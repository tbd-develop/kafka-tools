using System.Text.Json;
using Confluent.Kafka;

namespace MessagingToolset.Outbox.Infrastructure.Serialization;

public class EntityValueSerializer<TEntity> : ISerializer<TEntity>
    where TEntity : class
{
    public byte[]? Serialize(TEntity? data, SerializationContext context)
    {
        if (data is null)
        {
            return null;
        }
        
        return JsonSerializer.SerializeToUtf8Bytes(data, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}