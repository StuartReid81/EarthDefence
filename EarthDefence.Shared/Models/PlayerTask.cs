namespace EarthDefence.Shared.Models
{
    public class PlayerTask
    {
        public Guid TaskId { get; set; }
        public string TaskType { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
    }
}