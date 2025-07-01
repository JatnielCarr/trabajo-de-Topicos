using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.DTOs;
using MovieAPI.Models;

namespace MovieAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _context;
        private readonly IMapper _mapper;

        public MovieService(MovieDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieSummaryDto>> GetAllMoviesAsync(int? userId = null)
        {
            var query = _context.Movies.AsQueryable();

            if (userId.HasValue)
            {
                query = query.Select(m => new Movie
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterUrl,
                    Rating = m.Rating,
                    Year = m.Year,
                    Genre = m.Genre,
                    IsFeatured = m.IsFeatured,
                    IsNew = m.IsNew,
                    UserFavorites = m.UserFavorites.Where(uf => uf.UserId == userId).ToList()
                });
            }

            var movies = await query.ToListAsync();
            var movieDtos = _mapper.Map<IEnumerable<MovieSummaryDto>>(movies);

            if (userId.HasValue)
            {
                foreach (var movieDto in movieDtos)
                {
                    movieDto.IsFavorite = movies.First(m => m.Id == movieDto.Id).UserFavorites.Any();
                }
            }

            return movieDtos;
        }

        public async Task<MovieDto?> GetMovieByIdAsync(int id, int? userId = null)
        {
            var query = _context.Movies.AsQueryable();

            if (userId.HasValue)
            {
                query = query.Select(m => new Movie
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    Year = m.Year,
                    Genre = m.Genre,
                    Director = m.Director,
                    Cast = m.Cast,
                    Rating = m.Rating,
                    PosterUrl = m.PosterUrl,
                    TrailerUrl = m.TrailerUrl,
                    VideoUrl = m.VideoUrl,
                    Duration = m.Duration,
                    Language = m.Language,
                    IsFeatured = m.IsFeatured,
                    IsNew = m.IsNew,
                    CreatedAt = m.CreatedAt,
                    UserFavorites = m.UserFavorites.Where(uf => uf.UserId == userId).ToList()
                });
            }

            var movie = await query.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return null;

            var movieDto = _mapper.Map<MovieDto>(movie);
            
            if (userId.HasValue)
            {
                movieDto.IsFavorite = movie.UserFavorites.Any();
            }

            return movieDto;
        }

        public async Task<IEnumerable<MovieSummaryDto>> GetFeaturedMoviesAsync(int? userId = null)
        {
            var query = _context.Movies.Where(m => m.IsFeatured).AsQueryable();

            if (userId.HasValue)
            {
                query = query.Select(m => new Movie
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterUrl,
                    Rating = m.Rating,
                    Year = m.Year,
                    Genre = m.Genre,
                    IsFeatured = m.IsFeatured,
                    IsNew = m.IsNew,
                    UserFavorites = m.UserFavorites.Where(uf => uf.UserId == userId).ToList()
                });
            }

            var movies = await query.ToListAsync();
            var movieDtos = _mapper.Map<IEnumerable<MovieSummaryDto>>(movies);

            if (userId.HasValue)
            {
                foreach (var movieDto in movieDtos)
                {
                    movieDto.IsFavorite = movies.First(m => m.Id == movieDto.Id).UserFavorites.Any();
                }
            }

            return movieDtos;
        }

        public async Task<IEnumerable<MovieSummaryDto>> GetNewMoviesAsync(int? userId = null)
        {
            var query = _context.Movies.Where(m => m.IsNew).AsQueryable();

            if (userId.HasValue)
            {
                query = query.Select(m => new Movie
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterUrl,
                    Rating = m.Rating,
                    Year = m.Year,
                    Genre = m.Genre,
                    IsFeatured = m.IsFeatured,
                    IsNew = m.IsNew,
                    UserFavorites = m.UserFavorites.Where(uf => uf.UserId == userId).ToList()
                });
            }

            var movies = await query.ToListAsync();
            var movieDtos = _mapper.Map<IEnumerable<MovieSummaryDto>>(movies);

            if (userId.HasValue)
            {
                foreach (var movieDto in movieDtos)
                {
                    movieDto.IsFavorite = movies.First(m => m.Id == movieDto.Id).UserFavorites.Any();
                }
            }

            return movieDtos;
        }

        public async Task<IEnumerable<MovieSummaryDto>> SearchMoviesAsync(string query, int? userId = null)
        {
            var searchQuery = _context.Movies
                .Where(m => m.Title.Contains(query) || 
                           m.Description.Contains(query) || 
                           m.Director.Contains(query) || 
                           m.Cast.Contains(query) ||
                           m.Genre.Contains(query))
                .AsQueryable();

            if (userId.HasValue)
            {
                searchQuery = searchQuery.Select(m => new Movie
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterUrl,
                    Rating = m.Rating,
                    Year = m.Year,
                    Genre = m.Genre,
                    IsFeatured = m.IsFeatured,
                    IsNew = m.IsNew,
                    UserFavorites = m.UserFavorites.Where(uf => uf.UserId == userId).ToList()
                });
            }

            var movies = await searchQuery.ToListAsync();
            var movieDtos = _mapper.Map<IEnumerable<MovieSummaryDto>>(movies);

            if (userId.HasValue)
            {
                foreach (var movieDto in movieDtos)
                {
                    movieDto.IsFavorite = movies.First(m => m.Id == movieDto.Id).UserFavorites.Any();
                }
            }

            return movieDtos;
        }

        public async Task<IEnumerable<MovieSummaryDto>> GetMoviesByGenreAsync(string genre, int? userId = null)
        {
            var query = _context.Movies.Where(m => m.Genre.ToLower() == genre.ToLower()).AsQueryable();

            if (userId.HasValue)
            {
                query = query.Select(m => new Movie
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterUrl,
                    Rating = m.Rating,
                    Year = m.Year,
                    Genre = m.Genre,
                    IsFeatured = m.IsFeatured,
                    IsNew = m.IsNew,
                    UserFavorites = m.UserFavorites.Where(uf => uf.UserId == userId).ToList()
                });
            }

            var movies = await query.ToListAsync();
            var movieDtos = _mapper.Map<IEnumerable<MovieSummaryDto>>(movies);

            if (userId.HasValue)
            {
                foreach (var movieDto in movieDtos)
                {
                    movieDto.IsFavorite = movies.First(m => m.Id == movieDto.Id).UserFavorites.Any();
                }
            }

            return movieDtos;
        }

        public async Task<MovieDto> CreateMovieAsync(CreateMovieDto createMovieDto)
        {
            var movie = _mapper.Map<Movie>(createMovieDto);
            movie.CreatedAt = DateTime.UtcNow;
            movie.UpdatedAt = DateTime.UtcNow;

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto?> UpdateMovieAsync(int id, UpdateMovieDto updateMovieDto)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return null;

            // Actualizar solo las propiedades que no son null
            if (updateMovieDto.Title != null) movie.Title = updateMovieDto.Title;
            if (updateMovieDto.Description != null) movie.Description = updateMovieDto.Description;
            if (updateMovieDto.Year.HasValue) movie.Year = updateMovieDto.Year.Value;
            if (updateMovieDto.Genre != null) movie.Genre = updateMovieDto.Genre;
            if (updateMovieDto.Director != null) movie.Director = updateMovieDto.Director;
            if (updateMovieDto.Cast != null) movie.Cast = updateMovieDto.Cast;
            if (updateMovieDto.Rating.HasValue) movie.Rating = updateMovieDto.Rating.Value;
            if (updateMovieDto.PosterUrl != null) movie.PosterUrl = updateMovieDto.PosterUrl;
            if (updateMovieDto.TrailerUrl != null) movie.TrailerUrl = updateMovieDto.TrailerUrl;
            if (updateMovieDto.VideoUrl != null) movie.VideoUrl = updateMovieDto.VideoUrl;
            if (updateMovieDto.Duration.HasValue) movie.Duration = updateMovieDto.Duration.Value;
            if (updateMovieDto.Language != null) movie.Language = updateMovieDto.Language;
            if (updateMovieDto.IsFeatured.HasValue) movie.IsFeatured = updateMovieDto.IsFeatured.Value;
            if (updateMovieDto.IsNew.HasValue) movie.IsNew = updateMovieDto.IsNew.Value;

            movie.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return false;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddToFavoritesAsync(int userId, int movieId)
        {
            var existingFavorite = await _context.UserFavorites
                .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.MovieId == movieId);

            if (existingFavorite != null) return false;

            var favorite = new UserFavorite
            {
                UserId = userId,
                MovieId = movieId,
                AddedAt = DateTime.UtcNow
            };

            _context.UserFavorites.Add(favorite);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromFavoritesAsync(int userId, int movieId)
        {
            var favorite = await _context.UserFavorites
                .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.MovieId == movieId);

            if (favorite == null) return false;

            _context.UserFavorites.Remove(favorite);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MovieSummaryDto>> GetUserFavoritesAsync(int userId)
        {
            var favorites = await _context.UserFavorites
                .Where(uf => uf.UserId == userId)
                .Include(uf => uf.Movie)
                .Select(uf => uf.Movie)
                .ToListAsync();

            var movieDtos = _mapper.Map<IEnumerable<MovieSummaryDto>>(favorites);
            
            foreach (var movieDto in movieDtos)
            {
                movieDto.IsFavorite = true;
            }

            return movieDtos;
        }

        public async Task<bool> AddToWatchHistoryAsync(int userId, int movieId, int watchDuration = 0)
        {
            var watchHistory = new WatchHistory
            {
                UserId = userId,
                MovieId = movieId,
                WatchedAt = DateTime.UtcNow,
                WatchDuration = watchDuration,
                Completed = watchDuration > 0
            };

            _context.WatchHistories.Add(watchHistory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MovieSummaryDto>> GetUserWatchHistoryAsync(int userId)
        {
            var watchHistory = await _context.WatchHistories
                .Where(wh => wh.UserId == userId)
                .Include(wh => wh.Movie)
                .OrderByDescending(wh => wh.WatchedAt)
                .Select(wh => wh.Movie)
                .ToListAsync();

            var movieDtos = _mapper.Map<IEnumerable<MovieSummaryDto>>(watchHistory);
            
            foreach (var movieDto in movieDtos)
            {
                movieDto.IsFavorite = await _context.UserFavorites
                    .AnyAsync(uf => uf.UserId == userId && uf.MovieId == movieDto.Id);
            }

            return movieDtos;
        }
    }
} 