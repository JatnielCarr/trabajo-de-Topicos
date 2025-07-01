namespace MovieAPI.Models
{
    public class UserFavorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        
        // Navegación
        public User User { get; set; } = null!;
        public Movie Movie { get; set; } = null!;
    }
} 