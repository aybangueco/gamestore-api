using GameStoreAPI.DTOs;
using GameStoreAPI.Models;

namespace GameStoreAPI.Common.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User> CreateUserAsync(CreateUserDto dto);
    Task<User> UpdateUserAsync(int id, UpdateUserDto dto);
    Task DeleteUserAsync(int id);
}