using Confluent.Kafka;

namespace MessagingToolset.Outbox.Infrastructure.Serialization;

public class IntSerializer : ISerializer<int>
{
    public byte[] Serialize(int data, SerializationContext context)
    {
        return BitConverter.GetBytes(data);
    }
}