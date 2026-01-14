using GameStoreAPI.Common.Interfaces;
using GameStoreAPI.DTOs;
using GameStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Services;

public class GameService: IGameService
{
    private readonly ModelsContext _db;

    public GameService(ModelsContext db)
    {
        _db = db;
    }

    public async Task<List<Game>> GetGames()
    {
        var games = await _db.Games.ToListAsync();
        return games;
    }

    public async Task<Game?> GetGame(int id)
    {
        var game = await _db.Games.FirstOrDefaultAsync(g => g.Id == id);
        return game ?? null;
    }

    public async Task<Game> AddGame(CreateGameDto dto)
    {
        var newGame = new Game
        {
            Title = dto.Title,
            Description = dto.Description,
            Genre = dto.Genre,
            Publisher = dto.Publisher,
            ReleaseDate = dto.ReleaseDate,
        };

        await _db.Games.AddAsync(newGame);
        await _db.SaveChangesAsync();
        
        return newGame;
    }

    public async Task<Game> UpdateGame(int id, UpdateGameDto dto)
    {
        var existingGame = await _db.Games.FirstOrDefaultAsync(g => g.Id == id);

        if (existingGame == null)
        {
            throw new KeyNotFoundException("Game not found");
        }
        
        existingGame.Title = dto.Title;
        existingGame.Description = dto.Description;
        existingGame.Genre = dto.Genre;
        existingGame.Publisher = dto.Publisher;
        existingGame.ReleaseDate = dto.ReleaseDate;
        await _db.SaveChangesAsync();

        return existingGame;
    }

    public async Task DeleteGame(int id)
    {
        var existingGame = await _db.Games.FindAsync(id);

        if (existingGame is null)
        {
            throw new KeyNotFoundException("Game not found");
        }
        
        _db.Games.Remove(existingGame);
        await _db.SaveChangesAsync();
    }
}