using EarthDefence.Shared.Models;

namespace EarthDefence.Core.Interfaces
{
    public interface IPlayerRepo
    {
        Task<PlayerState?> GetPlayerAsync(string playerId);
        Task SavePlayerAsync(PlayerState player);
    }
}