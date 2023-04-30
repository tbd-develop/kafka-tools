using MessagingToolset.Outbox.Infrastructure;

namespace MessagingToolset.Outbox.Storage.Sqlite.Infrastructure.Extensions;

public static class OutboxConfigurationBuilderExtensions
{
    public static OutboxBuilder UseSqlite(this OutboxBuilder builder)
    {
        builder.SetStorage<SqliteStorage>();

        return builder;
    }
}