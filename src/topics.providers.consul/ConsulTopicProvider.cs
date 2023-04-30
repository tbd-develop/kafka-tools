using System.Text.Json;
using MessagingToolset.Topics.Providers.Consul.Infrastructure;

namespace MessagingToolset.Topics.Providers.Consul;

public class ConsulTopicProvider : IKafkaTopicProvider
{
    private readonly ConsulTopicProviderConfiguration _configuration;
    private readonly IHttpClientFactory _factory;
    private IDictionary<string, string>? _topics;

    public ConsulTopicProvider(
        IHttpClientFactory factory,
        ConsulTopicProviderConfiguration configuration)
    {
        _configuration = configuration;
        _factory = factory;
    }

    public async Task<string> GetTopicAsync(string name, CancellationToken cancellationToken = default)
    {
        if (_topics is null)
        {
            await FetchTopics(cancellationToken);
        }

        return _topics!.TryGetValue(name, out string? value) ? value : name;
    }

    private async Task FetchTopics(CancellationToken cancellationToken = default)
    {
        using var client = _factory.CreateClient();

        client.BaseAddress = _configuration.Host;

        var request = await client.GetAsync($"/v1/kv/{_configuration.ConfigurationKey}", cancellationToken);

        if (request.IsSuccessStatusCode)
        {
            var response = JsonSerializer.Deserialize<ConsulKeyResult[]>(await request.Content.ReadAsStreamAsync(cancellationToken));

            if (response is null)
                throw new NullReferenceException("No topics found in Consul");

            var configuration = (JsonSerializer.Deserialize<KafkaTopicConfiguration>(
                Convert.FromBase64String(response.First().Value),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                }));

            _topics = configuration!.Topics;
        }
    }

    private class ConsulKeyResult
    {
        public int LockIndex { get; set; }
        public string Key { get; set; } = null!;
        public int Flags { get; set; }
        public string Value { get; set; } = null!;
        public int CreateIndex { get; set; }
        public int ModifyIndex { get; set; }
    }
}