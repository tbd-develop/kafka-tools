using System.Text;
using Confluent.Kafka;

namespace GameStack.Kafka.Deserializers;

public class StringKeyDeserializer : IDeserializer<string>
{
    public string Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return Encoding.UTF8.GetString(data);
    }
}