using Microsoft.EntityFrameworkCore;
using Trc.Core.Common;
using Trc.Core.Data;
using Trc.Core.Models;

namespace Trc.Core.Services;

/// <summary>Автентифікація та керування обліковими записами користувачів.</summary>
public class AuthService : ServiceBase
{
    public const int MinPasswordLength = 6;

    public AuthService(Func<TrcDbContext> contextFactory, IClock clock) : base(contextFactory, clock) { }

    /// <summary>Перевіряє логін і пароль. Повертає користувача або null.</summary>
    public User? Authenticate(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrEmpty(password))
            return null;

        using var db = ContextFactory();
        var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Login == login.Trim());
        if (user is null || !user.IsActive)
            return null;

        return PasswordHasher.Verify(password, user.PasswordHash, user.PasswordSalt) ? user : null;
    }

    public List<User> GetUsers()
    {
        using var db = ContextFactory();
        return db.Users.AsNoTracking().OrderBy(u => u.Login).ToList();
    }

    public User Get(int id)
    {
        using var db = ContextFactory();
        return db.Users.AsNoTracking().FirstOrDefault(u => u.Id == id)
               ?? throw new BusinessException("Користувача не знайдено.");
    }

    public int AddUser(string login, string fullName, UserRole role, string password)
    {
        login = Guard.Required(login, "Логін");
        fullName = Guard.Required(fullName, "ПІБ");
        Guard.Matches(login, Patterns.Login(), "Логін може містити лише латинські літери, цифри, «_» та «.» (3–30 символів).");
        ValidatePassword(password);

        using var db = ContextFactory();
        if (db.Users.Any(u => u.Login == login))
            throw new BusinessException($"Користувач з логіном «{login}» вже існує.");

        var (hash, salt) = PasswordHasher.HashPassword(password);
        var user = new User { Login = login, FullName = fullName, Role = role, PasswordHash = hash, PasswordSalt = salt };
        db.Users.Add(user);
        db.SaveChanges();
        return user.Id;
    }

    public void UpdateUser(int id, string fullName, UserRole role, bool isActive)
    {
        fullName = Guard.Required(fullName, "ПІБ");

        using var db = ContextFactory();
        var user = db.Users.Find(id) ?? throw new BusinessException("Користувача не знайдено.");

        var losesAdminRights = user.Role == UserRole.Administrator && user.IsActive
                               && (role != UserRole.Administrator || !isActive);
        if (losesAdminRights && CountActiveAdmins(db) <= 1)
            throw new BusinessException("У системі має залишитися хоча б один активний адміністратор.");

        user.FullName = fullName;
        user.Role = role;
        user.IsActive = isActive;
        db.SaveChanges();
    }

    /// <summary>Встановлення нового пароля адміністратором.</summary>
    public void ResetPassword(int id, string newPassword)
    {
        ValidatePassword(newPassword);
        using var db = ContextFactory();
        var user = db.Users.Find(id) ?? throw new BusinessException("Користувача не знайдено.");
        (user.PasswordHash, user.PasswordSalt) = PasswordHasher.HashPassword(newPassword);
        db.SaveChanges();
    }

    /// <summary>Зміна власного пароля з перевіркою поточного.</summary>
    public void ChangePassword(int id, string currentPassword, string newPassword)
    {
        using var db = ContextFactory();
        var user = db.Users.Find(id) ?? throw new BusinessException("Користувача не знайдено.");
        if (!PasswordHasher.Verify(currentPassword, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException("Поточний пароль введено неправильно.");

        ValidatePassword(newPassword);
        (user.PasswordHash, user.PasswordSalt) = PasswordHasher.HashPassword(newPassword);
        db.SaveChanges();
    }

    public void DeleteUser(int id, int currentUserId)
    {
        if (id == currentUserId)
            throw new BusinessException("Не можна видалити власний обліковий запис.");

        using var db = ContextFactory();
        var user = db.Users.Find(id) ?? throw new BusinessException("Користувача не знайдено.");
        if (user.Role == UserRole.Administrator && user.IsActive && CountActiveAdmins(db) <= 1)
            throw new BusinessException("Не можна видалити останнього адміністратора.");

        db.Users.Remove(user);
        db.SaveChanges();
    }

    private static int CountActiveAdmins(TrcDbContext db) =>
        db.Users.Count(u => u.Role == UserRole.Administrator && u.IsActive);

    private static void ValidatePassword(string? password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < MinPasswordLength)
            throw new BusinessException($"Пароль має містити щонайменше {MinPasswordLength} символів.");
    }
}
