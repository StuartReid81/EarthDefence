using System.Net.Http.Json;
using EarthDefence.Shared.Models;

namespace EarthDefence.Blazor.Services
{
    public class GameApiService
    {
        private readonly HttpClient _http;

        public GameApiService(HttpClient http)
        {
            _http = http;
        } 

        /// <summary>
        ///  Fetched the player state by ID
        /// </summary>
        /// <param name="playerId">Id of the current player</param>
        /// <returns></returns>
        public async Task<PlayerState?> GetPlayerStateAsync(string playerId)
        {
            try
            {
                return await _http.GetFromJsonAsync<PlayerState>($"api/player/{playerId}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[{nameof(GameApiService)}] - GetPlayerAsync({playerId}) Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Initiates a task for a player.
        /// </summary>
        /// <param name="playerId">Id of the current player</param>
        /// <param name="taskId">Id of the task we are starting</param>
        /// <returns></returns>
        public async Task<PlayerState?> StartTaskAsync(string playerId, string taskId)
        {
            try
            {
                var response = await _http.PostAsJsonAsync($"api/player/{playerId}/tasks/{taskId}/start", new { });
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PlayerState>();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[GameApiService] StartTaskAsync Error: {ex.Message}");
            }
            return null;
        }

    /// <summary>
    /// Claims rewards for a completed task.
    /// </summary>
    /// <param name="playerId">Id of the current player</param>
    /// <param name="taskId">Id of the completed task we are claiming the reward for</param>
    /// <returns></returns>
        public async Task<PlayerState?> ClaimRewardAsync(string playerId, string taskId)
        {
            try
            {
                var response = await _http.PostAsJsonAsync($"api/player/{playerId}/tasks/{taskId}/claim", new { });
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PlayerState>();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[GameApiService] ClaimRewardAsync Error: {ex.Message}");
            }
            return null;
        }
    }
}