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

app.UseHttpLogging();
app.UseCors();

app.MapGet("/", () => "Hello");
app.MapPost("/", async (Message message) =>
{
    if (string.IsNullOrEmpty(message.Name) && string.IsNullOrEmpty(message.Content))
    {
        return Results.BadRequest();
    }
    
    await using (var cmd = new NpgsqlCommand("INSERT INTO data VALUES (@n, @c)", conn))
    {
        cmd.Parameters.AddWithValue("n", message.Name!);
        cmd.Parameters.AddWithValue("c", message.Content!);
        await cmd.ExecuteNonQueryAsync();
    }
    return Results.Ok();
});

app.Run();

class Message
{
    public string? Name { get; set; }
    public string? Content { get; set; }
}
