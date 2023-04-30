using Confluent.Kafka;

namespace GameStack.Kafka.Deserializers;

public class GuidKeyDeserializer : IDeserializer<Guid>
{
    public Guid Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return new Guid(data);
    }
}