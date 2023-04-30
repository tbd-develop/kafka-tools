using System.Text.Json;
using Confluent.Kafka;

namespace GameStack.Kafka.Deserializers;

public class EntityValueDeserializer<TEntity> : IDeserializer<TEntity>
    where TEntity : class
{
    public TEntity Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return
            JsonSerializer.Deserialize<TEntity>(data,
                new JsonSerializerOptions
                    { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true })!;
    }
}