using GameStoreAPI.Common.Interfaces;
using GameStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Repository;

public class UserRepository(ModelsContext db): IUserRepository
{
    public async Task<User?> GetUserByIdAsync(Guid id) => await db.Users.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User?> GetUserByUsernameAsync(string username) => await db.Users.FirstOrDefaultAsync(u => u.Username == username);

    public async Task CreateUserAsync(User user)
    {
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        db.Users.Update(user);
        await db.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        db.Users.Remove(user);
        await db.SaveChangesAsync();
    }
}