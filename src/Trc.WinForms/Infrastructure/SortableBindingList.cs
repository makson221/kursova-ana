using System.ComponentModel;

namespace Trc.WinForms.Infrastructure;

/// <summary>
/// Список для прив'язки до DataGridView з підтримкою сортування кліком по заголовку стовпця.
/// </summary>
public class SortableBindingList<T> : BindingList<T>
{
    private bool _isSorted;
    private ListSortDirection _direction;
    private PropertyDescriptor? _property;

    public SortableBindingList(IList<T> items) : base(items) { }

    protected override bool SupportsSortingCore => true;
    protected override bool IsSortedCore => _isSorted;
    protected override ListSortDirection SortDirectionCore => _direction;
    protected override PropertyDescriptor? SortPropertyCore => _property;

    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        if (Items is not List<T> list)
            return;

        var comparer = Comparer<object?>.Default;
        list.Sort((a, b) =>
        {
            var result = comparer.Compare(prop.GetValue(a), prop.GetValue(b));
            return direction == ListSortDirection.Ascending ? result : -result;
        });

        _property = prop;
        _direction = direction;
        _isSorted = true;
        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    protected override void RemoveSortCore()
    {
        _isSorted = false;
        _property = null;
    }
}
