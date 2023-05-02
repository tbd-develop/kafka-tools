using MessagingToolset.Kafka.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingToolset.Topics.Providers.Consul.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKafkaConsul(this IServiceCollection services,
        Action<ConsulTopicProviderConfiguration> configure)
    {
        services.AddGameStackKafka();

        services.AddHttpClient();

        services.AddTransient<ConsulTopicProviderConfiguration>(provider =>
        {
            var configuration = new ConsulTopicProviderConfiguration();

            configure(configuration);

            return configuration;
        });

        services.AddTransient<IKafkaTopicProvider, ConsulTopicProvider>();

        return services;
    }
}