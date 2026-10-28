namespace Trc.WinForms.Infrastructure;

/// <summary>Розділ головного вікна (вкладка).</summary>
public interface ISectionControl
{
    /// <summary>Передає розділу сервіси. Викликається один раз після створення головної форми.</summary>
    void Initialize(AppServices services);

    /// <summary>Перечитує дані з бази.</summary>
    void RefreshData();
}
