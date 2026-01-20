using GameStoreAPI.Common.Interfaces;
using GameStoreAPI.DTOs;
using GameStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Services;

public class UserService(IUserRepository userRepository, ILogger<UserService> logger): IUserService
{
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        return user ?? null;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var user = await userRepository.GetUserByUsernameAsync(username);
        return user ?? null;
    }

    public async Task<Guid> CreateUserAsync(CreateUserDto dto)
    {
        logger.LogInformation("Creating user with username {Username}", dto.Username);

        var existingUser = await userRepository.GetUserByUsernameAsync(dto.Username);
        if (existingUser is not null)
        {
            logger.LogWarning("User with username {Username} already exists", dto.Username);
            throw new BadHttpRequestException("Username already exists");
        }

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            Password = dto.Password,
        };
        
        await userRepository.CreateUserAsync(newUser);
        return newUser.Id;
    }

    public async Task UpdateUserAsync(Guid id, UpdateUserDto dto)
    {
        logger.LogInformation("Updating user with username {Username}", dto.Username);

        var existingUser = await userRepository.GetUserByIdAsync(id);
        if (existingUser is null)
        {
            logger.LogWarning("User with id {Id} does not exist", id);
            throw new BadHttpRequestException("User does not exists");
        }
        
        existingUser.Username = dto.Username;
        existingUser.Password = dto.Password;
        await userRepository.UpdateUserAsync(existingUser);
        
        logger.LogInformation("User with id {Id} updated successfully", id);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        logger.LogInformation("Updating user with id {Id}", id);
        
        var existingUser = await userRepository.GetUserByIdAsync(id);
        if (existingUser is null)
        {
            logger.LogWarning("User with id {Id} does not exist", id);
            throw new BadHttpRequestException("User does not exists");
        }
        
        await userRepository.DeleteUserAsync(existingUser);
        logger.LogInformation("User with id {Id} deleted successfully", id);
    }
}