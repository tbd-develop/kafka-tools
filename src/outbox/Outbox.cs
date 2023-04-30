using MessagingToolset.Outbox.Storage;

namespace MessagingToolset.Outbox;

public class Outbox : IMessageOutbox
{
    private readonly IStorageProvider _storageProvider;

    public Outbox(IStorageProvider storageProvider)
    {
        _storageProvider = storageProvider;
    }

    public async Task PostAsync<TKey, TMessage>(TKey key, TMessage? message, string topic,
        CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        await _storageProvider.QueueMessageAsync(
            QueuedMessage.Create(
                key,
                message,
                topic
            ),
            cancellationToken
        );
    }
}