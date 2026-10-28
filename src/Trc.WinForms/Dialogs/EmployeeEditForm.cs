using Trc.Core.Models;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Dialogs;

/// <summary>Додавання / редагування працівника.</summary>
public partial class EmployeeEditForm : Form
{
    private readonly AppServices _services = null!;
    private readonly int _id;

    public EmployeeEditForm()
    {
        InitializeComponent();
    }

    public EmployeeEditForm(AppServices services, Employee? employee) : this()
    {
        _services = services;
        UiHelper.FillEnum(cmbDepartment, employee?.Department ?? Department.Administration);
        dtHire.MaxDate = services.Clock.Today;

        if (employee is null)
        {
            Text = "Новий працівник";
            numSalary.Value = 15000;
            dtHire.Value = services.Clock.Today;
            chkActive.Checked = true;
            chkActive.Enabled = false;
            return;
        }

        _id = employee.Id;
        Text = $"Працівник: {employee.FullName}";
        txtName.Text = employee.FullName;
        txtPosition.Text = employee.Position;
        numSalary.Value = employee.Salary;
        txtPhone.Text = employee.Phone;
        dtHire.Value = employee.HireDate;
        chkActive.Checked = employee.IsActive;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        var employee = new Employee
        {
            Id = _id,
            FullName = txtName.Text,
            Position = txtPosition.Text,
            Department = UiHelper.SelectedEnum<Department>(cmbDepartment),
            Salary = numSalary.Value,
            Phone = txtPhone.Text,
            HireDate = dtHire.Value.Date,
            IsActive = chkActive.Checked
        };

        var saved = UiHelper.Run(this, () =>
        {
            if (_id == 0) _services.Employees.Add(employee);
            else _services.Employees.Update(employee);
        });
        if (saved)
            DialogResult = DialogResult.OK;
    }
}
