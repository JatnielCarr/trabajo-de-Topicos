using System.ComponentModel.DataAnnotations;

namespace PrimeCine.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public int Year { get; set; }
        
        [MaxLength(50)]
        public string Genre { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string Director { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string Cast { get; set; } = string.Empty;
        
        [Range(0, 10)]
        public decimal Rating { get; set; }
        
        [MaxLength(500)]
        public string PosterUrl { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string TrailerUrl { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string VideoUrl { get; set; } = string.Empty;
        
        public int Duration { get; set; } // en minutos
        
        [MaxLength(50)]
        public string Language { get; set; } = "Español";
        
        public bool IsFeatured { get; set; } = false;
        
        public bool IsNew { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Relaciones
        public ICollection<UserFavorite> UserFavorites { get; set; } = new List<UserFavorite>();
        public ICollection<WatchHistory> WatchHistories { get; set; } = new List<WatchHistory>();
    }
} 