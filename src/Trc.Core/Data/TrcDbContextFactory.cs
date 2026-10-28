using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Trc.Core.Data;

/// <summary>Створення контекстів бази даних.</summary>
public static class TrcDbContextFactory
{
    /// <summary>Фабрика контекстів для файлу бази даних SQLite.</summary>
    public static Func<TrcDbContext> ForFile(string databasePath)
    {
        var connectionString = new SqliteConnectionStringBuilder { DataSource = databasePath }.ToString();
        var options = new DbContextOptionsBuilder<TrcDbContext>()
            .UseSqlite(connectionString)
            .Options;
        return () => new TrcDbContext(options);
    }

    /// <summary>
    /// Фабрика контекстів для бази в оперативній пам'яті (використовується в тестах).
    /// База існує, поки відкрите повернуте з'єднання.
    /// </summary>
    public static (Func<TrcDbContext> Factory, SqliteConnection Connection) InMemory()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<TrcDbContext>()
            .UseSqlite(connection)
            .Options;
        return (() => new TrcDbContext(options), connection);
    }
}
