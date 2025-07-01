using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.DTOs;
using MovieAPI.Services;
using System.Security.Claims;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var currentUserId = GetCurrentUserId();
            
            // Los usuarios solo pueden ver su propio perfil, excepto los admins
            if (currentUserId != id && !User.IsInRole("Admin"))
                return Forbid();

            var user = await _userService.GetUserByIdAsync(id);
            
            if (user == null)
                return NotFound("Usuario no encontrado");

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var currentUserId = GetCurrentUserId();
            
            // Los usuarios solo pueden actualizar su propio perfil, excepto los admins
            if (currentUserId != id && !User.IsInRole("Admin"))
                return Forbid();

            var user = await _userService.UpdateUserAsync(id, updateUserDto);
            
            if (user == null)
                return NotFound("Usuario no encontrado");

            return Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            
            if (!success)
                return NotFound("Usuario no encontrado");

            return NoContent();
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim?.Value ?? "0");
        }
    }
} 