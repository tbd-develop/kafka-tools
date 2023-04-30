using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingToolset.Kafka.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGameStackKafka(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();

            var kafkaConfiguration = new KafkaConfiguration();

            configuration.GetSection("Kafka").Bind(kafkaConfiguration);

            return kafkaConfiguration;
        });
        
        return serviceCollection;
    }
}