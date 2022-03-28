using Npgsql;

var connString = "Host=localhost;Username=postgres;Password=postgres;Database=kvidrer_db;";

await using var conn = new NpgsqlConnection(connString);
await conn.OpenAsync();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseForwardedHeaders();
app.UseHttpLogging();
app.UseCors();

app.MapGet("/", async () =>
{
    // Retrieve all rows
    await using var cmd = new NpgsqlCommand("SELECT * FROM data ORDER BY Id DESC", conn);
    await using var reader = await cmd.ExecuteReaderAsync();

    List<Message> messages = new List<Message>();
    while (await reader.ReadAsync())
    {
        var message = new Message();
        message.Timestamp = reader.GetInt64(1);
        message.Name = reader.GetString(2);
        message.Content = reader.GetString(3);
        messages.Add(message);
    }

    return messages;
});
app.MapPost("/", async (Message message) =>
{
    if (string.IsNullOrEmpty(message.Name) && string.IsNullOrEmpty(message.Content))
    {
        return Results.BadRequest("Must provide name and content.");
    }

    await using (var cmd = new NpgsqlCommand("INSERT INTO data (Timestamp, Name, Content) VALUES (@t, @n, @c)", conn))
    {
        message.Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        cmd.Parameters.AddWithValue("t", message.Timestamp);
        cmd.Parameters.AddWithValue("n", message.Name!);
        cmd.Parameters.AddWithValue("c", message.Content!);
        await cmd.ExecuteNonQueryAsync();
    }
    return Results.Ok();
});

app.Run();

class Message
{
    public long Timestamp { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
}