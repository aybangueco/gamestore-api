using GameStoreAPI.Common.Interfaces;
using GameStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Repository;

public class GameRepository(ModelsContext db): IGameRepository
{
    public async Task<List<Game>> GetGamesAsync() => await db.Games.ToListAsync();

    public async Task<Game?> GetGameByIdAsync(int id) => await db.Games.FirstOrDefaultAsync();

    public async Task<Game> CreateGameAsync(Game game)
    {
        await db.Games.AddAsync(game);
        await db.SaveChangesAsync();
        return game;
    }

    public async Task<Game> UpdateGameAsync(Game game)
    {
        db.Games.Update(game);
        await db.SaveChangesAsync();

        return game;
    }

    public async Task DeleteGameAsync(Game game)
    {
        db.Games.Remove(game);
        await db.SaveChangesAsync();
    }
}