using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrimeCine.Data;
using PrimeCine.DTOs;
using PrimeCine.Models;

namespace PrimeCine.Services
{
    public class UserService : IUserService
    {
        private readonly MovieDbContext _context;
        private readonly IMapper _mapper;

        public UserService(MovieDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            if (updateUserDto.FirstName != null) user.FirstName = updateUserDto.FirstName;
            if (updateUserDto.LastName != null) user.LastName = updateUserDto.LastName;
            if (updateUserDto.DateOfBirth.HasValue) user.DateOfBirth = updateUserDto.DateOfBirth.Value;

            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.Where(u => u.IsActive).ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
} 