using Microsoft.Extensions.DependencyInjection;

namespace MessagingToolset.Outbox.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddKafkaOutbox(this IServiceCollection serviceCollection,
        Action<OutboxBuilder> configure)
    {
        var builder = new OutboxBuilder();

        configure(builder);

        serviceCollection.AddSingleton<IMessageOutbox>(provider => builder.Build(provider));
        
        serviceCollection.AddHostedService<OutboxService>();

        return serviceCollection;
    }
}