using MessagingToolset.Outbox.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingToolset.Outbox.Infrastructure;

public class OutboxBuilder
{
    private readonly IServiceCollection _serviceCollection;

    public OutboxBuilder(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public OutboxBuilder SetStorage(Func<IServiceProvider, IStorageProvider> factory)
    {
        _serviceCollection.AddScoped(factory);

        return this;
    }

    public IMessageOutbox Build(IServiceProvider provider)
    {
        return new Outbox(
            provider.GetRequiredService<IStorageProvider>()
        );
    }
}