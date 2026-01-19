using GameStoreAPI.Common.Interfaces;
using GameStoreAPI.DTOs;
using GameStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Services;

public class GameService(ModelsContext db): IGameService
{
    public async Task<List<Game>> GetGames()
    {
        var games = await db.Games.ToListAsync();
        return games;
    }

    public async Task<Game?> GetGame(int id)
    {
        var game = await db.Games.FirstOrDefaultAsync(g => g.Id == id);
        return game ?? null;
    }

    public async Task<Game> AddGame(CreateGameDto dto)
    {
        var newGame = new Game
        {
            Title = dto.Title,
            Description = dto.Description,
            GenreId = dto.GenreId,
            Publisher = dto.Publisher,
            ReleaseDate = dto.ReleaseDate,
        };

        await db.Games.AddAsync(newGame);
        await db.SaveChangesAsync();
        
        return newGame;
    }

    public async Task<Game> UpdateGame(int id, UpdateGameDto dto)
    {
        var existingGame = await db.Games.FirstOrDefaultAsync(g => g.Id == id);

        if (existingGame == null)
        {
            throw new KeyNotFoundException("Game not found");
        }
        
        existingGame.Title = dto.Title;
        existingGame.Description = dto.Description;
        existingGame.GenreId = dto.GenreId;
        existingGame.Publisher = dto.Publisher;
        existingGame.ReleaseDate = dto.ReleaseDate;
        await db.SaveChangesAsync();

        return existingGame;
    }

    public async Task DeleteGame(int id)
    {
        var existingGame = await db.Games.FindAsync(id);

        if (existingGame is null)
        {
            throw new KeyNotFoundException("Game not found");
        }
        
        db.Games.Remove(existingGame);
        await db.SaveChangesAsync();
    }
}