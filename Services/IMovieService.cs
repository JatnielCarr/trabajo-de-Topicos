using MovieAPI.DTOs;

namespace MovieAPI.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieSummaryDto>> GetAllMoviesAsync(int? userId = null);
        Task<MovieDto?> GetMovieByIdAsync(int id, int? userId = null);
        Task<IEnumerable<MovieSummaryDto>> GetFeaturedMoviesAsync(int? userId = null);
        Task<IEnumerable<MovieSummaryDto>> GetNewMoviesAsync(int? userId = null);
        Task<IEnumerable<MovieSummaryDto>> SearchMoviesAsync(string query, int? userId = null);
        Task<IEnumerable<MovieSummaryDto>> GetMoviesByGenreAsync(string genre, int? userId = null);
        Task<MovieDto> CreateMovieAsync(CreateMovieDto createMovieDto);
        Task<MovieDto?> UpdateMovieAsync(int id, UpdateMovieDto updateMovieDto);
        Task<bool> DeleteMovieAsync(int id);
        Task<bool> AddToFavoritesAsync(int userId, int movieId);
        Task<bool> RemoveFromFavoritesAsync(int userId, int movieId);
        Task<IEnumerable<MovieSummaryDto>> GetUserFavoritesAsync(int userId);
        Task<bool> AddToWatchHistoryAsync(int userId, int movieId, int watchDuration = 0);
        Task<IEnumerable<MovieSummaryDto>> GetUserWatchHistoryAsync(int userId);
    }
} 