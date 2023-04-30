namespace MessagingToolset.Kafka;

public class KafkaConfiguration
{
    public IDictionary<string, string> Producers { get; set; } = null!;
    public ConsumerConfiguration Consumers { get; set; } = null!;
    public IDictionary<string, string> Topics { get; set; } = null!;
}