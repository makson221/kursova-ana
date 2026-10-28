using Trc.Core.Common;
using Trc.Core.Models;

namespace Trc.Tests;

public class ValidationAndReportTests
{
    [Theory]
    [InlineData("1234567")]     // 7 цифр
    [InlineData("12345678901")] // 11 цифр
    [InlineData("1234567A")]
    public void Tenant_InvalidTaxCode_IsRejected(string code)
    {
        using var t = new TestDatabase();
        Assert.Throws<BusinessException>(() => t.AddTenant(code: code));
    }

    [Fact]
    public void Tenant_DuplicateTaxCode_IsRejected()
    {
        using var t = new TestDatabase();
        t.AddTenant("Перший", "12345678");
        Assert.Throws<BusinessException>(() => t.AddTenant("Другий", "12345678"));
    }

    [Fact]
    public void Premises_DuplicateNumber_IsRejected()
    {
        using var t = new TestDatabase();
        t.AddPremises("2-010");
        Assert.Throws<BusinessException>(() => t.AddPremises("2-010"));
    }

    [Fact]
    public void Employee_PayrollGroupedByDepartment()
    {
        using var t = new TestDatabase();
        Employee E(string name, Department dep, decimal salary) => new()
        {
            FullName = name, Position = "Посада", Department = dep, Salary = salary,
            Phone = "+380501112233", HireDate = new DateTime(2025, 1, 1)
        };
        t.Employees.Add(E("А", Department.Security, 18000));
        t.Employees.Add(E("Б", Department.Security, 20000));
        var dismissed = t.Employees.Add(E("В", Department.Cleaning, 14000));
        t.Employees.Dismiss(dismissed);

        var payroll = Assert.Single(t.Employees.GetPayroll());
        Assert.Equal(Department.Security, payroll.Department);
        Assert.Equal(38000m, payroll.TotalSalary);
        Assert.Equal(19000m, payroll.AverageSalary);
    }

    [Fact]
    public void Dashboard_CalculatesOccupancyByArea()
    {
        using var t = new TestDatabase();
        var occupied = t.AddPremises("1-001", area: 300);
        t.AddPremises("1-002", area: 100);
        t.Leases.Create(t.AddTenant(), occupied, new DateTime(2026, 3, 1), new DateTime(2027, 2, 28));

        var stats = t.Reports.GetDashboard();
        Assert.Equal(2, stats.PremisesTotal);
        Assert.Equal(1, stats.PremisesOccupied);
        Assert.Equal(75.0, stats.OccupancyPercent);
    }

    [Fact]
    public void DemoData_LoadsAndReportsWork()
    {
        using var t = new TestDatabase(seedDemoData: true);
        var stats = t.Reports.GetDashboard();
        Assert.True(stats.PremisesTotal > 10);
        Assert.True(stats.ActiveLeases > 5);
        Assert.True(stats.TotalDebt > 0);
        Assert.Equal(12, t.Reports.GetRevenueByMonth(2026).Count);
        Assert.NotEmpty(t.Reports.GetOccupancyByFloor());
        Assert.NotEmpty(t.Reports.GetTenantsByCategory());
        Assert.NotEmpty(t.Payments.GetDebtors());
    }

    [Fact]
    public void CsvExport_WritesHeadersFromDisplayName()
    {
        using var t = new TestDatabase(seedDemoData: true);
        var path = Path.Combine(Path.GetTempPath(), $"trc-test-{Guid.NewGuid():N}.csv");
        try
        {
            CsvExporter.Export(t.Reports.GetOccupancyByFloor(), path);
            var header = File.ReadLines(path).First();
            Assert.StartsWith("Поверх;Приміщень", header);
        }
        finally
        {
            File.Delete(path);
        }
    }
}
