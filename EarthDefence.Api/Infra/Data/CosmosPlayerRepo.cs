using Microsoft.Azure.Cosmos;
using EarthDefence.Shared.Models;
using EarthDefence.Core.Interfaces;

namespace EarthDefence.Infra.Data;

public class CosmosPlayerRepo : IPlayerRepo
{
    private readonly Container _container;

    public CosmosPlayerRepo(CosmosClient cosmosClient, IConfiguration config)
    {
        var dbName = config["CosmosDb:DatabaseName"] ?? "EarthDefenceDb";
        var containerName = config["CosmosDb:ContainerName"] ?? "PlayerStates";
        _container = cosmosClient.GetContainer(dbName, containerName);
    }

    public async Task<PlayerState?> GetPlayerAsync(string playerId)
    {
        try
        {
            ItemResponse<PlayerState> response = await _container.ReadItemAsync<PlayerState>(
                id: playerId,
                partitionKey: new PartitionKey(playerId)
            );
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task SavePlayerAsync(PlayerState player)
    {
        player.UpdatedAtUtc = DateTime.UtcNow;
        
        await _container.UpsertItemAsync(
            item: player,
            partitionKey: new PartitionKey(player.PlayerId)
        );
    }
}