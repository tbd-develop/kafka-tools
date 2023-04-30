using System.Text.Json;

namespace MessagingToolset.Outbox.Storage;

public class QueuedMessage
{
    public int Id { get; private set; }
    public string Key { get; private set; } = null!;
    public Type KeyType { get; set; } = null!;
    public string? Message { get; private set; }
    public Type MessageType { get; private set; } = null!;
    public string Topic { get; private set; } = null!;


    private QueuedMessage()
    {
    }

    public QueuedMessage(int id, string key, Type keyType, string message, Type messageType, string topic)
    {
        Id = id;
        Key = key;
        KeyType = keyType;
        Message = message;
        MessageType = messageType;
        Topic = topic;
    }

    public static QueuedMessage Create<TKey, TMessage>(TKey key, TMessage? message, string topic)
    {
        string serializedKey = key switch
        {
            Guid guid => guid.ToString(),
            _ => key?.ToString() ?? ""
        };

        return new()
        {
            Key = serializedKey,
            KeyType = typeof(TKey),
            Message = message is not null ? JsonSerializer.Serialize(message) : null,
            MessageType = typeof(TMessage),
            Topic = topic
        };
    }
}