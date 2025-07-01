namespace MovieAPI.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public string Cast { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string Language { get; set; } = string.Empty;
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsFavorite { get; set; } = false;
    }

    public class MovieSummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; } = string.Empty;
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public bool IsFavorite { get; set; } = false;
    }

    public class CreateMovieDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public string Cast { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string Language { get; set; } = "Español";
        public bool IsFeatured { get; set; } = false;
        public bool IsNew { get; set; } = false;
    }

    public class UpdateMovieDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Year { get; set; }
        public string? Genre { get; set; }
        public string? Director { get; set; }
        public string? Cast { get; set; }
        public decimal? Rating { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public string? VideoUrl { get; set; }
        public int? Duration { get; set; }
        public string? Language { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? IsNew { get; set; }
    }
} 