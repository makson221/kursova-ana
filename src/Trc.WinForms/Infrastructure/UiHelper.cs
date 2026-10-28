using System.ComponentModel;
using System.Reflection;
using Trc.Core.Common;

namespace Trc.WinForms.Infrastructure;

/// <summary>Спільні допоміжні методи для форм.</summary>
internal static class UiHelper
{
    public const string AppTitle = "ТРЦ «Галактика»";

    /// <summary>Однакове оформлення всіх таблиць.</summary>
    public static void SetupGrid(DataGridView grid)
    {
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.RowHeadersVisible = false;
        grid.BackgroundColor = SystemColors.Window;
        grid.BorderStyle = BorderStyle.None;
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 252);
        grid.ColumnHeadersDefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold);
        grid.EnableHeadersVisualStyles = true;
        grid.DataBindingComplete += (_, _) => FormatColumns(grid);
    }

    /// <summary>Прив'язує список до таблиці, зберігаючи виділений рядок.</summary>
    public static void Bind<T>(DataGridView grid, IEnumerable<T> rows, Func<T, int>? key = null)
    {
        var selectedKey = key is not null && grid.CurrentRow?.DataBoundItem is T current ? key(current) : (int?)null;
        grid.DataSource = new SortableBindingList<T>(rows.ToList());

        if (selectedKey is null || key is null)
            return;
        foreach (DataGridViewRow row in grid.Rows)
        {
            if (row.DataBoundItem is T item && key(item) == selectedKey)
            {
                grid.CurrentCell = row.Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible);
                break;
            }
        }
    }

    public static T? Selected<T>(DataGridView grid) where T : class => grid.CurrentRow?.DataBoundItem as T;

    /// <summary>Повертає виділений рядок або показує підказку, якщо нічого не вибрано.</summary>
    public static bool TryGetSelected<T>(DataGridView grid, out T row) where T : class
    {
        row = Selected<T>(grid)!;
        if (row is not null)
            return true;

        MessageBox.Show(grid.FindForm(), "Спочатку виберіть запис у таблиці.", AppTitle,
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        return false;
    }

    private static void FormatColumns(DataGridView grid)
    {
        var itemType = grid.DataSource?.GetType().GetGenericArguments().FirstOrDefault();
        foreach (DataGridViewColumn column in grid.Columns)
        {
            var property = itemType?.GetProperty(column.DataPropertyName);
            var custom = property?.GetCustomAttribute<GridFormatAttribute>();
            var type = Nullable.GetUnderlyingType(column.ValueType ?? typeof(object)) ?? column.ValueType;

            if (custom is not null)
                column.DefaultCellStyle.Format = custom.Format;
            else if (type == typeof(decimal))
                column.DefaultCellStyle.Format = "N2";
            else if (type == typeof(DateTime))
                column.DefaultCellStyle.Format = "dd.MM.yyyy";

            if (type == typeof(decimal) || type == typeof(int) || type == typeof(double))
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }

    /// <summary>
    /// Виконує дію і показує зрозуміле повідомлення у разі помилки.
    /// Повертає true, якщо дія виконана успішно.
    /// </summary>
    public static bool Run(IWin32Window? owner, Action action)
    {
        try
        {
            action();
            return true;
        }
        catch (BusinessException ex)
        {
            MessageBox.Show(owner, ex.Message, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            ShowError(owner, ex);
        }
        return false;
    }

    public static void ShowError(IWin32Window? owner, Exception ex, string? message = null)
    {
        var text = (message ?? "Під час виконання операції сталася помилка.") + Environment.NewLine + Environment.NewLine
                   + (ex.InnerException?.Message ?? ex.Message);
        MessageBox.Show(owner, text, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static bool Confirm(IWin32Window? owner, string question) =>
        MessageBox.Show(owner, question, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2) == DialogResult.Yes;

    public static void Info(IWin32Window? owner, string text) =>
        MessageBox.Show(owner, text, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

    /// <summary>Зберігає вміст таблиці у CSV-файл (для відкриття в Excel).</summary>
    public static void ExportGrid(DataGridView grid, string defaultFileName)
    {
        if (grid.DataSource is not IBindingList list || list.Count == 0)
        {
            Info(grid.FindForm(), "Немає даних для експорту.");
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Filter = "CSV (роздільник «;»)|*.csv",
            FileName = $"{defaultFileName}_{DateTime.Now:yyyy-MM-dd}.csv"
        };
        if (dialog.ShowDialog(grid.FindForm()) != DialogResult.OK)
            return;

        var itemType = list.GetType().GetGenericArguments()[0];
        Run(grid.FindForm(), () =>
        {
            CsvExporter.Export(list, itemType, dialog.FileName);
            Info(grid.FindForm(), $"Дані збережено у файл:{Environment.NewLine}{dialog.FileName}");
        });
    }

    /// <summary>Заповнює ComboBox значеннями переліку з українськими назвами.</summary>
    public static void FillEnum<T>(ComboBox combo, T selected) where T : struct, Enum
    {
        combo.Items.Clear();
        foreach (var option in EnumHelper.GetOptions<T>())
            combo.Items.Add(option);
        combo.SelectedItem = new EnumOption<T>(selected, selected.GetDisplayName());
    }

    public static T SelectedEnum<T>(ComboBox combo) where T : struct, Enum => ((EnumOption<T>)combo.SelectedItem!).Value;

    /// <summary>Заповнює ToolStripComboBox пунктом «Усі» та значеннями переліку.</summary>
    public static void FillEnumFilter<T>(ToolStripComboBox combo, string allText = "Усі") where T : struct, Enum
    {
        combo.Items.Clear();
        combo.Items.Add(allText);
        foreach (var option in EnumHelper.GetOptions<T>())
            combo.Items.Add(option);
        combo.SelectedIndex = 0;
    }

    public static T? SelectedFilter<T>(ToolStripComboBox combo) where T : struct, Enum =>
        combo.SelectedItem is EnumOption<T> option ? option.Value : null;
}
