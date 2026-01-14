using GameStoreAPI.Common.Interfaces;
using GameStoreAPI.DTOs;
using GameStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Services;

public class GenreService: IGenreService
{
    private readonly ModelsContext _db;

    public GenreService(ModelsContext db)
    {
        _db = db;
    }

    public async Task<List<Genre>> GetGenres()
    {
        var genres = await _db.Genres.ToListAsync();
        return genres;
    }

    public async Task<Genre?> GetGenre(int id)
    {
        var genre = await _db.Genres.FirstOrDefaultAsync(g => g.Id == id);
        return genre ?? null;
    }

    public async Task<Genre> AddGenre(CreateGenreDto dto)
    {
        var newGenre = new Genre
        {
            Name = dto.Name
        };

        await _db.AddAsync(newGenre);
        await _db.SaveChangesAsync();
        return newGenre;
    }

    public async Task<Genre> UpdateGenre(int id, UpdateGenreDto dto)
    {
        var existingGenre = await _db.Genres.FirstOrDefaultAsync(g => g.Id == id);

        if (existingGenre == null)
        {
            throw new KeyNotFoundException($"Genre with id {id} not found");
        }

        existingGenre.Name = dto.Name;
        await _db.SaveChangesAsync();
        return existingGenre;
    }

    public async Task DeleteGenre(int id)
    {
        var existingGenre = await _db.Genres.FirstOrDefaultAsync(g => g.Id == id);
        if (existingGenre == null)
        {
            throw new KeyNotFoundException($"Genre with id {id} not found");
        }
        
        _db.Genres.Remove(existingGenre);
        await _db.SaveChangesAsync();
    }
}