using Confluent.Kafka;

namespace MessagingToolset.Kafka.Serializers;

public class GuidKeySerializer : ISerializer<Guid>
{
    public byte[] Serialize(Guid data, SerializationContext context)
    {
        return data.ToByteArray();
    }
}