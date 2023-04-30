namespace MessagingToolset.Topics.Providers.Consul.Infrastructure;

public class ConsulTopicProviderConfiguration
{
    public Uri Host { get; set; } = null!;
    public string ConfigurationKey { get; set; } = null!;
}