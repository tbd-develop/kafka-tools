using System.Text.Json;
using Confluent.Kafka;

namespace MessagingToolset.Kafka.Serializers;

public class EntityValueSerializer<TEntity> : ISerializer<TEntity>
    where TEntity : class
{
    public byte[] Serialize(TEntity data, SerializationContext context)
    {
        return JsonSerializer.SerializeToUtf8Bytes(data, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}