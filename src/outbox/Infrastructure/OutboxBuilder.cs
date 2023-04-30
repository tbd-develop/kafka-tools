using MessagingToolset.Outbox.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingToolset.Outbox.Infrastructure;

public class OutboxBuilder
{
    private Type? StorageType { get; set; }

    public OutboxBuilder SetStorage<TStorage>()
        where TStorage : IStorageProvider
    {
        StorageType = typeof(TStorage);
        
        return this;
    }
    public IMessageOutbox Build(IServiceProvider provider)
    {
        if (StorageType is null)
            throw new ArgumentNullException("Storage Type is required for Outbox");

        return new Outbox(
            (IStorageProvider)provider.GetRequiredService(StorageType!)
        );
    }
}