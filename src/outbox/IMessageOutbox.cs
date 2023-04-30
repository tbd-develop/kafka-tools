namespace MessagingToolset.Outbox;

public interface IMessageOutbox
{
    Task PostAsync<TKey, TMessage>(TKey key, TMessage? message, string topic, CancellationToken cancellationToken = default)
        where TKey : notnull;
}