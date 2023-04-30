namespace MessagingToolset.Topics.Providers;

public interface IKafkaTopicProvider
{
    Task<string> GetTopicAsync(string name, CancellationToken cancellationToken = default);
}