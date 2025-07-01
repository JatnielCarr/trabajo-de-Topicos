using PrimeCine.DTOs;

namespace PrimeCine.Services
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        Task<bool> DeleteUserAsync(int id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
} 