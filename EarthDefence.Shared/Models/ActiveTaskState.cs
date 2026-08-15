namespace EarthDefence.Shared.Models
{
    public class ActiveTaskState
    {
        public PlayerTask ActiveTask { get; set; } = new();
        public DateTime StartedAtUtc { get; set; }
        public DateTime TargetCompletionTimeUtc { get; set; }
        public bool IsRewardClaimed { get; set; } = false;
    }
}