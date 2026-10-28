using Trc.Core.Models;
using Trc.WinForms.Infrastructure;

namespace Trc.WinForms.Dialogs;

/// <summary>Додавання / редагування приміщення.</summary>
public partial class PremisesEditForm : Form
{
    private readonly AppServices _services = null!;
    private readonly int _id;

    public PremisesEditForm()
    {
        InitializeComponent();
    }

    public PremisesEditForm(AppServices services, Premises? premises) : this()
    {
        _services = services;
        UiHelper.FillEnum(cmbType, premises?.Type ?? PremisesType.Shop);
        UiHelper.FillEnum(cmbStatus, premises?.Status ?? PremisesStatus.Free);

        if (premises is null)
        {
            Text = "Нове приміщення";
            numFloor.Value = 1;
            numArea.Value = 50;
            numRate.Value = 500;
        }
        else
        {
            _id = premises.Id;
            Text = $"Приміщення {premises.Number}";
            txtNumber.Text = premises.Number;
            numFloor.Value = premises.Floor;
            numArea.Value = premises.Area;
            numRate.Value = premises.RatePerSqm;
            txtDescription.Text = premises.Description;
            // Статус орендованого приміщення змінюється лише через договори.
            cmbStatus.Enabled = premises.Status != PremisesStatus.Occupied;
        }
        UpdateRent();
    }

    private void numArea_ValueChanged(object sender, EventArgs e) => UpdateRent();

    private void UpdateRent() =>
        lblRent.Text = $"Базова орендна плата: {numArea.Value * numRate.Value:N2} грн/міс.";

    private void btnOk_Click(object sender, EventArgs e)
    {
        var premises = new Premises
        {
            Id = _id,
            Number = txtNumber.Text,
            Floor = (int)numFloor.Value,
            Type = UiHelper.SelectedEnum<PremisesType>(cmbType),
            Area = numArea.Value,
            RatePerSqm = numRate.Value,
            Status = UiHelper.SelectedEnum<PremisesStatus>(cmbStatus),
            Description = txtDescription.Text
        };

        var saved = UiHelper.Run(this, () =>
        {
            if (_id == 0) _services.Premises.Add(premises);
            else _services.Premises.Update(premises);
        });
        if (saved)
            DialogResult = DialogResult.OK;
    }
}
