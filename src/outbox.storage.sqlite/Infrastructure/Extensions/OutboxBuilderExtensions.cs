using MessagingToolset.Outbox.Infrastructure;

namespace MessagingToolset.Outbox.Storage.Sqlite.Infrastructure.Extensions;

public static class OutboxConfigurationBuilderExtensions
{
    public static OutboxBuilder UseSqlite(this OutboxBuilder builder, string connectionString)
    {
        builder.SetStorage((_) => new SqliteStorage(connectionString));

        return builder;
    }
}