using System.Text.Json.Serialization;

namespace EarthDefence.Shared.Models
{
    public class PlayerState
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("playerId")]
        public string PlayerId { get; set; } = string.Empty;

        public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;

        public Currencies Currencies { get; set; } = new();
        public ActiveTaskState? ActiveTask { get; set; }
        public List<PlayerTask> RecentTasks { get; set; } = [];

        public void AddToRecentTasks(PlayerTask task, int maxHistory = 10)
        {
            RecentTasks.Insert(0, task);

            if (RecentTasks.Count > maxHistory)
            {
                RecentTasks = [.. RecentTasks.Take(maxHistory)];
            }
        }
    }
}