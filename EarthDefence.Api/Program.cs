using Scalar.AspNetCore;
using Microsoft.Azure.Cosmos;
using EarthDefence.Shared.Models;
using EarthDefence.Core.Interfaces;
using EarthDefence.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5000",
                "https://localhost:5001",
                "http://localhost:5200",
                "https://localhost:7200"
              )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSingleton(sp =>
{
    var connectionString = builder.Configuration["CosmosDb:ConnectionString"];
    return new CosmosClient(connectionString, new CosmosClientOptions
    {
        SerializerOptions = new CosmosSerializationOptions
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        }
    });
});

builder.Services.AddScoped<IPlayerRepo, CosmosPlayerRepo>();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowBlazorApp");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// Endpoint: Fetch Snapshot
app.MapGet("/player/{playerId}", async (IPlayerRepo repo, string playerId) =>
{
    var player = await repo.GetPlayerAsync(playerId);
    if (player == null) return Results.NotFound(new { Message = "Player not found." });
    
    return Results.Ok(player);
});

// Endpoint: Start Task & Persist
app.MapPost("/player/{playerId}/start-task", async (IPlayerRepo repo, string playerId, int durationMinutes) =>
{
    var player = await repo.GetPlayerAsync(playerId) ?? new PlayerState { Id = playerId, PlayerId = playerId };

    var now = DateTime.UtcNow;
    player.ActiveTask = new ActiveTaskState
    {
        StartedAtUtc = now,
        TargetCompletionTimeUtc = now.AddMinutes(durationMinutes),
        ActiveTask = new PlayerTask()
        {
            DurationMinutes = durationMinutes,
            TaskId = new Guid(),
            TaskType = "Test"
        }
    };

    await repo.SavePlayerAsync(player);
    return Results.Ok(player);
});

app.Run();
