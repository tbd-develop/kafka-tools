namespace MessagingToolset.Outbox.Storage;

public interface IStorageProvider
{
    Task QueueMessageAsync(QueuedMessage message,
        CancellationToken cancellationToken = new());

    Task<QueuedMessage?> DequeueMessageAsync(
        CancellationToken cancellationToken = new());

    Task<bool> Commit(QueuedMessage message,
        CancellationToken cancellationToken = new());
}