using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeCine.DTOs;
using PrimeCine.Services;
using System.Security.Claims;

namespace PrimeCine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieSummaryDto>>> GetMovies()
        {
            var userId = GetCurrentUserId();
            var movies = await _movieService.GetAllMoviesAsync(userId);
            return Ok(movies);
        }

        [HttpGet("featured")]
        public async Task<ActionResult<IEnumerable<MovieSummaryDto>>> GetFeaturedMovies()
        {
            var userId = GetCurrentUserId();
            var movies = await _movieService.GetFeaturedMoviesAsync(userId);
            return Ok(movies);
        }

        [HttpGet("new")]
        public async Task<ActionResult<IEnumerable<MovieSummaryDto>>> GetNewMovies()
        {
            var userId = GetCurrentUserId();
            var movies = await _movieService.GetNewMoviesAsync(userId);
            return Ok(movies);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<MovieSummaryDto>>> SearchMovies([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return BadRequest("El parámetro de búsqueda es requerido");

            var userId = GetCurrentUserId();
            var movies = await _movieService.SearchMoviesAsync(q, userId);
            return Ok(movies);
        }

        [HttpGet("genre/{genre}")]
        public async Task<ActionResult<IEnumerable<MovieSummaryDto>>> GetMoviesByGenre(string genre)
        {
            var userId = GetCurrentUserId();
            var movies = await _movieService.GetMoviesByGenreAsync(genre, userId);
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var userId = GetCurrentUserId();
            var movie = await _movieService.GetMovieByIdAsync(id, userId);
            
            if (movie == null)
                return NotFound("Película no encontrada");

            return Ok(movie);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MovieDto>> CreateMovie(CreateMovieDto createMovieDto)
        {
            var movie = await _movieService.CreateMovieAsync(createMovieDto);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MovieDto>> UpdateMovie(int id, UpdateMovieDto updateMovieDto)
        {
            var movie = await _movieService.UpdateMovieAsync(id, updateMovieDto);
            
            if (movie == null)
                return NotFound("Película no encontrada");

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var success = await _movieService.DeleteMovieAsync(id);
            
            if (!success)
                return NotFound("Película no encontrada");

            return NoContent();
        }

        [HttpPost("{id}/favorites")]
        [Authorize]
        public async Task<ActionResult> AddToFavorites(int id)
        {
            var userId = GetCurrentUserId();
            var success = await _movieService.AddToFavoritesAsync(userId.Value, id);
            
            if (!success)
                return BadRequest("La película ya está en favoritos");

            return Ok(new { message = "Película agregada a favoritos" });
        }

        [HttpDelete("{id}/favorites")]
        [Authorize]
        public async Task<ActionResult> RemoveFromFavorites(int id)
        {
            var userId = GetCurrentUserId();
            var success = await _movieService.RemoveFromFavoritesAsync(userId.Value, id);
            
            if (!success)
                return BadRequest("La película no está en favoritos");

            return Ok(new { message = "Película removida de favoritos" });
        }

        [HttpGet("favorites")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MovieSummaryDto>>> GetFavorites()
        {
            var userId = GetCurrentUserId();
            var favorites = await _movieService.GetUserFavoritesAsync(userId.Value);
            return Ok(favorites);
        }

        [HttpPost("{id}/watch")]
        [Authorize]
        public async Task<ActionResult> AddToWatchHistory(int id, [FromBody] int watchDuration = 0)
        {
            var userId = GetCurrentUserId();
            var success = await _movieService.AddToWatchHistoryAsync(userId.Value, id, watchDuration);
            
            if (!success)
                return BadRequest("Error al agregar al historial");

            return Ok(new { message = "Agregado al historial de visualización" });
        }

        [HttpGet("history")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MovieSummaryDto>>> GetWatchHistory()
        {
            var userId = GetCurrentUserId();
            var history = await _movieService.GetUserWatchHistoryAsync(userId.Value);
            return Ok(history);
        }

        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : null;
        }
    }
} 