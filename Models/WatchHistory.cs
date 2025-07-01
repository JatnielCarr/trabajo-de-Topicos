namespace MovieAPI.Models
{
    public class WatchHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime WatchedAt { get; set; } = DateTime.UtcNow;
        public int WatchDuration { get; set; } // en segundos
        public bool Completed { get; set; } = false;
        
        // Navegación
        public User User { get; set; } = null!;
        public Movie Movie { get; set; } = null!;
    }
} 