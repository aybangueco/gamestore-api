using GameStoreAPI.Common.Interfaces;
using GameStoreAPI.DTOs;
using GameStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Services;

public class GameService(IGameRepository gameRepository, ILogger logger): IGameService
{
    public async Task<List<Game>> GetGamesAsync()
    {
        var games = await gameRepository.GetGamesAsync();
        return games;
    }

    public async Task<Game?> GetGameByIdAsync(int id)
    {
        var game = await gameRepository.GetGameByIdAsync(id);
        return game ?? throw new KeyNotFoundException();
    }

    public async Task<Game> CreateGameAsync(CreateGameDto dto)
    {
        logger.LogInformation("Creating game");
        
        var newGame = new Game
        {
            Title = dto.Title,
            Description = dto.Description,
            GenreId = dto.GenreId,
            Publisher = dto.Publisher,
            ReleaseDate = dto.ReleaseDate,
        };

        await gameRepository.CreateGameAsync(newGame);
        logger.LogInformation("Game created");
        return newGame;
    }

    public async Task<Game> UpdateGameAsync(int id, UpdateGameDto dto)
    {
        logger.LogInformation("Updating game");

        var existingGame = await gameRepository.GetGameByIdAsync(id);
        if (existingGame is null)
        {
            logger.LogWarning("Game not found with id {Id}", id);
            throw new KeyNotFoundException();
        }

        var updatedGame = new Game
        {
            Id = existingGame.Id,
            Title = dto.Title,
            Description = dto.Description,
            GenreId = dto.GenreId,
            Publisher = dto.Publisher,
            ReleaseDate = dto.ReleaseDate,
            UpdatedAt = DateTime.UtcNow
        };
        
        await gameRepository.UpdateGameAsync(updatedGame);
        logger.LogInformation("Game updated");
        return updatedGame;
    }

    public async Task DeleteGameAsync(int id)
    {
        logger.LogInformation("Deleting game");

        var existingGame = await gameRepository.GetGameByIdAsync(id);
        if (existingGame is null)
        {
            logger.LogWarning("Game not found with id {Id}", id);
            throw new KeyNotFoundException();
        }        
        
        await gameRepository.DeleteGameAsync(existingGame);
        logger.LogInformation("Game deleted");
    }
}