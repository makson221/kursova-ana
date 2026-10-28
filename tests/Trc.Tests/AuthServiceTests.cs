using Trc.Core.Common;
using Trc.Core.Data;
using Trc.Core.Models;

namespace Trc.Tests;

public class AuthServiceTests
{
    [Fact]
    public void DefaultAdmin_CanLogIn()
    {
        using var t = new TestDatabase();
        var user = t.Auth.Authenticate(DbInitializer.DefaultAdminLogin, DbInitializer.DefaultAdminPassword);
        Assert.NotNull(user);
        Assert.Equal(UserRole.Administrator, user!.Role);
    }

    [Fact]
    public void WrongPassword_ReturnsNull()
    {
        using var t = new TestDatabase();
        Assert.Null(t.Auth.Authenticate("admin", "wrong-password"));
    }

    [Fact]
    public void PasswordsAreNotStoredInPlainText()
    {
        using var t = new TestDatabase();
        var admin = t.Auth.GetUsers().Single(u => u.Login == "admin");
        Assert.DoesNotContain("admin123", admin.PasswordHash);
        Assert.True(PasswordHasher.Verify("admin123", admin.PasswordHash, admin.PasswordSalt));
    }

    [Fact]
    public void InactiveUser_CannotLogIn()
    {
        using var t = new TestDatabase();
        var id = t.Auth.AddUser("operator", "Оператор", UserRole.Cashier, "secret1");
        t.Auth.UpdateUser(id, "Оператор", UserRole.Cashier, isActive: false);
        Assert.Null(t.Auth.Authenticate("operator", "secret1"));
    }

    [Fact]
    public void CannotRemoveLastAdministrator()
    {
        using var t = new TestDatabase();
        var admin = t.Auth.GetUsers().Single(u => u.Login == "admin");
        Assert.Throws<BusinessException>(() => t.Auth.UpdateUser(admin.Id, admin.FullName, UserRole.Manager, true));
        Assert.Throws<BusinessException>(() => t.Auth.DeleteUser(admin.Id, currentUserId: -1));
    }

    [Fact]
    public void ChangePassword_RequiresCurrentPassword()
    {
        using var t = new TestDatabase();
        var admin = t.Auth.GetUsers().Single(u => u.Login == "admin");
        Assert.Throws<BusinessException>(() => t.Auth.ChangePassword(admin.Id, "bad", "newpass1"));

        t.Auth.ChangePassword(admin.Id, "admin123", "newpass1");
        Assert.NotNull(t.Auth.Authenticate("admin", "newpass1"));
    }

    [Theory]
    [InlineData("ab")]
    [InlineData("логін")]
    [InlineData("user name")]
    public void InvalidLogin_IsRejected(string login)
    {
        using var t = new TestDatabase();
        Assert.Throws<BusinessException>(() => t.Auth.AddUser(login, "Тест", UserRole.Cashier, "secret1"));
    }
}
