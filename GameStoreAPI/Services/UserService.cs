using GameStoreAPI.Common.Interfaces;
using GameStoreAPI.DTOs;
using GameStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Services;

public class UserService(ModelsContext db): IUserService
{
    public async Task<User?> GetUserByIdAsync(int id)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

        return user ?? null;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username );

        return user ?? null;
    }

    public async Task<User> CreateUserAsync(CreateUserDto dto)
    {
        var newUser = new User
        {
            Username = dto.Username,
            Password = dto.Password,
        };
        
        await db.Users.AddAsync(newUser);
        await db.SaveChangesAsync();
        
        return newUser;
    }

    public async Task<User> UpdateUserAsync(int id, UpdateUserDto dto)
    {
        var existingUser = await GetUserByIdAsync(id) ?? throw new KeyNotFoundException();
        
        existingUser.Username = dto.Username;
        existingUser.Password = dto.Password;
        existingUser.UpdatedAt = DateTime.UtcNow;

        db.Users.Update(existingUser);
        await db.SaveChangesAsync();

        return existingUser;
    }

    public async Task DeleteUserAsync(int id)
    {
        var existingUser = await db.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new KeyNotFoundException();
        db.Users.Remove(existingUser);
        await db.SaveChangesAsync();
    }
}