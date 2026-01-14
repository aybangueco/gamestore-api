using GameStoreAPI.DTOs;
using GameStoreAPI.Models;

namespace GameStoreAPI.Common.Interfaces;

public interface IGenreService
{
    Task<List<Genre>> GetGenres();
    Task<Genre?> GetGenre(int id);
    Task<Genre>AddGenre(CreateGenreDto dto);
    Task<Genre> UpdateGenre(int id, UpdateGenreDto dto);
    Task DeleteGenre(int id);
}