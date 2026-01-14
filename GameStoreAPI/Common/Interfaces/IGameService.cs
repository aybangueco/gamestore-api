using GameStoreAPI.DTOs;
using GameStoreAPI.Models;

namespace GameStoreAPI.Common.Interfaces;

public interface IGameService
{
    Task<List<Game>> GetGames();
    Task<Game?> GetGame(int id);
    Task<Game> AddGame(CreateGameDto dto);
    Task<Game> UpdateGame(int id, UpdateGameDto dto);
    Task DeleteGame(int id);
}