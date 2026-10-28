using Trc.Core.Models;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Dialogs;

/// <summary>Додавання / редагування орендаря.</summary>
public partial class TenantEditForm : Form
{
    private readonly AppServices _services = null!;
    private readonly int _id;

    public TenantEditForm()
    {
        InitializeComponent();
    }

    public TenantEditForm(AppServices services, Tenant? tenant) : this()
    {
        _services = services;
        UiHelper.FillEnum(cmbCategory, tenant?.Category ?? TenantCategory.Clothing);

        if (tenant is null)
        {
            Text = "Новий орендар";
            return;
        }

        _id = tenant.Id;
        Text = $"Орендар: {tenant.CompanyName}";
        txtName.Text = tenant.CompanyName;
        txtTaxCode.Text = tenant.TaxCode;
        txtContact.Text = tenant.ContactPerson;
        txtPhone.Text = tenant.Phone;
        txtEmail.Text = tenant.Email;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        var tenant = new Tenant
        {
            Id = _id,
            CompanyName = txtName.Text,
            TaxCode = txtTaxCode.Text,
            Category = UiHelper.SelectedEnum<TenantCategory>(cmbCategory),
            ContactPerson = txtContact.Text,
            Phone = txtPhone.Text,
            Email = txtEmail.Text
        };

        var saved = UiHelper.Run(this, () =>
        {
            if (_id == 0) _services.Tenants.Add(tenant);
            else _services.Tenants.Update(tenant);
        });
        if (saved)
            DialogResult = DialogResult.OK;
    }
}
