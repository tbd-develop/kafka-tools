using Microsoft.Data.Sqlite;

namespace MessagingToolset.Outbox.Storage.Sqlite;

public class SqliteStorage : IStorageProvider
{
    private readonly string _connectionString;

    public SqliteStorage(string connectionString)
    {
        _connectionString = connectionString;

        ConfigureDb();
    }

    public async Task QueueMessageAsync(QueuedMessage message, CancellationToken cancellationToken = new())
    {
        await using var connection = new SqliteConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();

        command.CommandText =
            "INSERT INTO Messages (Key, KeyType, Message, MessageType, Topic) VALUES (@key, @keyType, @message, @messageType, @topic)";

        command.Parameters.AddWithValue("@key", message.Key);
        command.Parameters.AddWithValue("@keyType", message.KeyType.FullName);
        command.Parameters.AddWithValue("@message", message.Message);
        command.Parameters.AddWithValue("@messageType", message.MessageType.FullName);
        command.Parameters.AddWithValue("@topic", message.Topic);

        await command.ExecuteNonQueryAsync(cancellationToken);

        await connection.CloseAsync();
    }

    public async Task<QueuedMessage?> DequeueMessageAsync(CancellationToken cancellationToken = new())
    {
        await using var connection = new SqliteConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();

        command.CommandText =
            "SELECT Id, Key, KeyType, Message, MessageType, Topic FROM Messages ORDER BY CreatedAt ASC LIMIT 1";

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken))
            return default!;
        
        var result = new QueuedMessage(
            reader.GetInt32(0),
            reader.GetString(1),
            Type.GetType(reader.GetString(2)),
            reader.GetString(3),
            Type.GetType(reader.GetString(4)),
            reader.GetString(5)
        );

        await reader.CloseAsync();

        return result;
    }
    
    public async Task<bool> Commit(QueuedMessage message, CancellationToken cancellationToken)
    {
        await using var connection = new SqliteConnection(_connectionString);
		
        await connection.OpenAsync(cancellationToken);
		
        var command = connection.CreateCommand();
		
        command.CommandText = "DELETE FROM Messages WHERE Id = @id";
		
        command.Parameters.AddWithValue("@id", message.Id);
		
        return await command.ExecuteNonQueryAsync(cancellationToken) > 0;
    }

    private void ConfigureDb()
    {
        using var connection = new SqliteConnection(_connectionString);

        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Messages (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Key TEXT NOT NULL,               
                    KeyType TEXT NOT NULL,         
					Message TEXT NOT NULL,
					MessageType TEXT NOT NULL,
					Topic TEXT NOT NULL,
                    CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP
                );
            ";

        command.ExecuteNonQuery();
    }
}