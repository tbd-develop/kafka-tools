using Confluent.Kafka;
using MessagingToolset.Outbox.Infrastructure.Serialization;

namespace MessagingToolset.Outbox.Infrastructure.Extensions;

public static class SerializerExtensions
{
    public static ISerializer<TKey>? GetKeySerializer<TKey>(this Type type)
    {
        return type switch {
            not null when type == typeof(Guid) => new GuidKeySerializer() as ISerializer<TKey>,
            not null when type == typeof(int) => new IntSerializer() as ISerializer<TKey>,
            not null when type == typeof(string) => new StringKeySerializer() as ISerializer<TKey>,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}