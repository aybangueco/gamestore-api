using GameStoreAPI.DTOs;
using GameStoreAPI.Models;

namespace GameStoreAPI.Common.Interfaces;

public interface IGameService
{
    Task<List<Game>> GetGamesAsync();
    Task<Game?> GetGameByIdAsync(int id);
    Task<Game> CreateGameAsync(CreateGameDto dto);
    Task<Game> UpdateGameAsync(int id, UpdateGameDto dto);
    Task DeleteGameAsync(int id);
}