using GameStoreAPI.DTOs;
using GameStoreAPI.Models;

namespace GameStoreAPI.Common.Interfaces;

public interface IGameRepository
{
    Task <List<Game>> GetGamesAsync();
    Task<Game?> GetGameByIdAsync(int id);
    Task<Game> CreateGameAsync(Game game);
    Task<Game> UpdateGameAsync(Game game);
    Task DeleteGameAsync(Game game);
}