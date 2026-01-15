using GameStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Common;

public static class Seed
{
    public static async Task PopulateGenresAsync(ModelsContext dbContext)
    {
        await dbContext.Database.MigrateAsync();

        var genresToSeed = new[]
        {
            "RPG",
            "Action",
            "Adventure",
            "Simulation",
            "Strategy"
        };

        // If genres are not found, add it to the database table.
        var missingGenres = genresToSeed
            .Where(g => !dbContext.Genres.Any(db => db.Name == g))
            .Select(g => new Genre { Name = g })
            .ToList();

        if (missingGenres.Any())
        {
            await dbContext.AddRangeAsync(missingGenres);
            await dbContext.SaveChangesAsync();
        }
    }
}