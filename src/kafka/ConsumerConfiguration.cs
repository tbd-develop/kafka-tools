namespace MessagingToolset.Kafka;

public class ConsumerConfiguration
{
    public IDictionary<string, string> Defaults { get; set; } = null!;
    public IDictionary<string, IDictionary<string, string>> Configurations { get; set; } = null!;
}