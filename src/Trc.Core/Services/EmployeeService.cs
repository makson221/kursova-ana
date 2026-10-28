using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Trc.Core.Common;
using Trc.Core.Data;
using Trc.Core.Models;

namespace Trc.Core.Services;

/// <summary>Фонд оплати праці відділу.</summary>
public sealed record DepartmentPayroll(
    [property: Browsable(false)] Department Department,
    [property: DisplayName("Відділ")] string DepartmentName,
    [property: DisplayName("Працівників")] int EmployeeCount,
    [property: DisplayName("Фонд оплати праці, грн")] decimal TotalSalary,
    [property: DisplayName("Середній оклад, грн")] decimal AverageSalary);

/// <summary>Облік персоналу ТРЦ.</summary>
public class EmployeeService : ServiceBase
{
    public EmployeeService(Func<TrcDbContext> contextFactory, IClock clock) : base(contextFactory, clock) { }

    public List<Employee> GetAll(string? search = null, Department? department = null, bool includeDismissed = false)
    {
        using var db = ContextFactory();
        var query = db.Employees.AsNoTracking();
        if (!includeDismissed) query = query.Where(e => e.IsActive);
        if (department.HasValue) query = query.Where(e => e.Department == department.Value);

        var list = query.OrderBy(e => e.FullName).ToList();
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            list = list.Where(e => ContainsText(e.FullName, search) || ContainsText(e.Position, search)
                                   || ContainsText(e.Phone, search)).ToList();
        }
        return list;
    }

    public Employee Get(int id)
    {
        using var db = ContextFactory();
        return db.Employees.AsNoTracking().FirstOrDefault(e => e.Id == id)
               ?? throw new BusinessException("Працівника не знайдено.");
    }

    public int Add(Employee employee)
    {
        Validate(employee);
        using var db = ContextFactory();
        var entity = new Employee();
        CopyFields(employee, entity);
        db.Employees.Add(entity);
        db.SaveChanges();
        return entity.Id;
    }

    public void Update(Employee employee)
    {
        Validate(employee);
        using var db = ContextFactory();
        var entity = db.Employees.Find(employee.Id) ?? throw new BusinessException("Працівника не знайдено.");
        CopyFields(employee, entity);
        db.SaveChanges();
    }

    /// <summary>Звільнення працівника (запис зберігається для історії).</summary>
    public void Dismiss(int id)
    {
        using var db = ContextFactory();
        var entity = db.Employees.Find(id) ?? throw new BusinessException("Працівника не знайдено.");
        if (!entity.IsActive)
            throw new BusinessException("Працівника вже звільнено.");
        entity.IsActive = false;
        db.SaveChanges();
    }

    public void Delete(int id)
    {
        using var db = ContextFactory();
        var entity = db.Employees.Find(id) ?? throw new BusinessException("Працівника не знайдено.");
        db.Employees.Remove(entity);
        db.SaveChanges();
    }

    /// <summary>Фонд оплати праці в розрізі відділів (лише працюючі співробітники).</summary>
    public List<DepartmentPayroll> GetPayroll()
    {
        using var db = ContextFactory();
        return db.Employees.AsNoTracking().Where(e => e.IsActive).ToList()
            .GroupBy(e => e.Department)
            .Select(g => new DepartmentPayroll(g.Key, g.Key.GetDisplayName(), g.Count(), g.Sum(e => e.Salary),
                                               Math.Round(g.Average(e => e.Salary), 2)))
            .OrderBy(p => p.Department)
            .ToList();
    }

    private void Validate(Employee e)
    {
        Guard.Required(e.FullName, "ПІБ");
        Guard.Required(e.Position, "Посада");
        Guard.Positive(e.Salary, "Оклад");
        var phone = Guard.Required(e.Phone, "Телефон");
        Guard.Matches(phone, Patterns.Phone(), "Невірний формат телефону. Приклад: +380671234567.");
        if (e.HireDate.Date > Clock.Today)
            throw new BusinessException("Дата прийому на роботу не може бути в майбутньому.");
    }

    private static void CopyFields(Employee source, Employee target)
    {
        target.FullName = source.FullName.Trim();
        target.Position = source.Position.Trim();
        target.Department = source.Department;
        target.Salary = source.Salary;
        target.Phone = source.Phone.Trim();
        target.HireDate = source.HireDate.Date;
        target.IsActive = source.IsActive;
    }
}
