using System.Drawing.Drawing2D;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Controls;

/// <summary>Розділ «Звіти»: таблиця і стовпчикова діаграма.</summary>
public partial class ReportsControl : UserControl, ISectionControl
{
    private static readonly string[] ReportNames =
    {
        "Надходження коштів за місяцями",
        "Заповненість приміщень за поверхами",
        "Орендарі за категоріями діяльності",
        "Фонд оплати праці за відділами",
        "Боржники з орендної плати"
    };

    private AppServices _services = null!;
    private List<(string Label, double Value)> _chartData = new();
    private string _chartTitle = string.Empty;
    private string _chartUnit = string.Empty;

    public ReportsControl()
    {
        InitializeComponent();
        UiHelper.SetupGrid(grid);
    }

    public void Initialize(AppServices services)
    {
        numYear.Value = services.Clock.Today.Year;
        cmbReport.Items.AddRange(ReportNames);
        cmbReport.SelectedIndex = 0;
        _services = services;
    }

    public void RefreshData() => BuildReport();

    private void BuildReport()
    {
        if (_services is null || cmbReport.SelectedIndex < 0)
            return;

        var reports = _services.Reports;
        switch (cmbReport.SelectedIndex)
        {
            case 0:
                var year = (int)numYear.Value;
                var revenue = reports.GetRevenueByMonth(year);
                UiHelper.Bind(grid, revenue);
                SetChart($"Надходження за {year} рік, грн", "грн",
                    revenue.Select(r => (r.MonthName[..3], (double)r.Total)));
                lblSummary.Text = $"Разом за {year} рік: оренда {revenue.Sum(r => r.Rent):N2} грн, " +
                                  $"квитки {revenue.Sum(r => r.Tickets):N2} грн, паркінг {revenue.Sum(r => r.Parking):N2} грн. " +
                                  $"Усього: {revenue.Sum(r => r.Total):N2} грн";
                break;

            case 1:
                var floors = reports.GetOccupancyByFloor();
                UiHelper.Bind(grid, floors);
                SetChart("Заповненість за поверхами, %", "%",
                    floors.Select(f => (f.Floor < 0 ? $"Цоколь ({f.Floor})" : $"Поверх {f.Floor}", f.OccupancyPercent)));
                var stats = reports.GetDashboard();
                lblSummary.Text = $"Загальна заповненість ТРЦ за площею: {stats.OccupancyPercent:0.0}% " +
                                  $"({stats.OccupiedArea:N2} з {stats.TotalArea:N2} м²)";
                break;

            case 2:
                var categories = reports.GetTenantsByCategory();
                UiHelper.Bind(grid, categories);
                SetChart("Щомісячна орендна плата за категоріями, грн", "грн",
                    categories.Select(c => (c.Category, (double)c.MonthlyRent)));
                lblSummary.Text = $"Орендарів: {categories.Sum(c => c.Tenants)}, " +
                                  $"оренда на місяць: {categories.Sum(c => c.MonthlyRent):N2} грн";
                break;

            case 3:
                var payroll = _services.Employees.GetPayroll();
                UiHelper.Bind(grid, payroll);
                SetChart("Фонд оплати праці за відділами, грн", "грн",
                    payroll.Select(p => (p.DepartmentName, (double)p.TotalSalary)));
                lblSummary.Text = $"Працівників: {payroll.Sum(p => p.EmployeeCount)}, " +
                                  $"загальний ФОП: {payroll.Sum(p => p.TotalSalary):N2} грн/міс.";
                break;

            case 4:
                var debtors = _services.Payments.GetDebtors().Select(DebtorRow.From).ToList();
                UiHelper.Bind(grid, debtors);
                SetChart("Заборгованість орендарів, грн", "грн", debtors.Select(d => (d.Tenant, (double)d.Debt)));
                lblSummary.Text = $"Боржників: {debtors.Count}, загальна сума боргу: {debtors.Sum(d => d.Debt):N2} грн";
                break;
        }
    }

    private void SetChart(string title, string unit, IEnumerable<(string, double)> data)
    {
        _chartTitle = title;
        _chartUnit = unit;
        _chartData = data.ToList();
        chart.Invalidate();
    }

    /// <summary>Малювання простої стовпчикової діаграми засобами GDI+.</summary>
    private void chart_Paint(object sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Color.White);

        using var titleFont = new Font(Font.FontFamily, 10f, FontStyle.Bold);
        using var textFont = new Font(Font.FontFamily, 8f);
        g.DrawString(_chartTitle, titleFont, Brushes.Black, 12, 8);

        if (_chartData.Count == 0)
        {
            g.DrawString("Немає даних для відображення", textFont, Brushes.Gray, 12, 36);
            return;
        }

        var area = new Rectangle(50, 40, chart.Width - 70, chart.Height - 90);
        if (area.Width < 50 || area.Height < 30)
            return;

        var max = Math.Max(_chartData.Max(d => d.Value), 1);
        using var axisPen = new Pen(Color.Silver);
        g.DrawLine(axisPen, area.Left, area.Bottom, area.Right, area.Bottom);

        // Горизонтальні допоміжні лінії
        for (var i = 1; i <= 4; i++)
        {
            var y = area.Bottom - area.Height * i / 4f;
            g.DrawLine(axisPen, area.Left, y, area.Right, y);
            g.DrawString(FormatValue(max * i / 4), textFont, Brushes.Gray, 2, y - 7);
        }

        var slot = area.Width / (float)_chartData.Count;
        var barWidth = Math.Max(4, slot * 0.6f);
        using var barBrush = new SolidBrush(Color.SteelBlue);
        using var centered = new StringFormat { Alignment = StringAlignment.Center };

        for (var i = 0; i < _chartData.Count; i++)
        {
            var (label, value) = _chartData[i];
            var height = (float)(value / max * area.Height);
            var x = area.Left + slot * i + (slot - barWidth) / 2;
            g.FillRectangle(barBrush, x, area.Bottom - height, barWidth, height);

            var center = area.Left + slot * i + slot / 2;
            if (value > 0)
                g.DrawString(FormatValue(value), textFont, Brushes.Black,
                    new RectangleF(center - slot / 2, area.Bottom - height - 15, slot, 15), centered);
            g.DrawString(label, textFont, Brushes.Black,
                new RectangleF(center - slot / 2, area.Bottom + 4, slot, 32), centered);
        }
    }

    private string FormatValue(double value) =>
        _chartUnit == "%" ? $"{value:0.#}%" : value >= 1000 ? $"{value / 1000:0.#} тис." : $"{value:0}";

    private void chart_Resize(object sender, EventArgs e) => chart.Invalidate();

    private void cmbReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        numYear.Enabled = cmbReport.SelectedIndex == 0;
        UiHelper.Run(this, BuildReport);
    }

    private void numYear_ValueChanged(object sender, EventArgs e) => UiHelper.Run(this, BuildReport);

    private void btnBuild_Click(object sender, EventArgs e) => UiHelper.Run(this, BuildReport);

    private void btnExport_Click(object sender, EventArgs e) =>
        UiHelper.ExportGrid(grid, cmbReport.SelectedItem?.ToString() ?? "Звіт");
}
