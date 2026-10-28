using Trc.Core.Common;
using Trc.Core.Data;

namespace Trc.Core.Services;

/// <summary>
/// Базовий клас сервісів. Кожна операція відкриває власний короткоживучий контекст БД,
/// тому дані на формах завжди актуальні, а помилка однієї операції не впливає на інші.
/// </summary>
public abstract class ServiceBase
{
    protected ServiceBase(Func<TrcDbContext> contextFactory, IClock clock)
    {
        ContextFactory = contextFactory;
        Clock = clock;
    }

    protected Func<TrcDbContext> ContextFactory { get; }
    protected IClock Clock { get; }

    protected static bool ContainsText(string? source, string search) =>
        source is not null && source.Contains(search, StringComparison.CurrentCultureIgnoreCase);
}
