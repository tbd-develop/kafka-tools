using Confluent.Kafka;

namespace MessagingToolset.Outbox.Infrastructure.Serialization;

public class GuidKeySerializer : ISerializer<Guid>
{
    public byte[] Serialize(Guid data, SerializationContext context)
    {
        return data.ToByteArray();
    }
}