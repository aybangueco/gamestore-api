using GameStoreAPI.Common.Interfaces;
using GameStoreAPI.DTOs;

namespace GameStoreAPI.Endpoints;

public static class GenresEndpoints
{
    public static void MapGenresEndpoint(this WebApplication app)
    {
        var genresGroup = app.MapGroup("/genres").RequireAuthorization();

        genresGroup.MapGet("/", async (IGenreService genreService) =>
        {
            var genres = await genreService.GetGenres();
            return Results.Ok(genres);
        });

        genresGroup.MapGet("/{id:int}", async (int id, IGenreService genreService) =>
        {
            var genre = await genreService.GetGenre(id);
            return genre is null ? Results.NotFound() : Results.Ok(genre);
        });

        genresGroup.MapPost("/", async (CreateGenreDto dto, IGenreService genreService) =>
        {
            var newGenre = await genreService.AddGenre(dto);
            return Results.Created($"/genres", newGenre);
        });

        genresGroup.MapPut("/{id:int}", async (int id, UpdateGenreDto dto, IGenreService genreService) =>
        {
            var updatedGenre = await genreService.UpdateGenre(id, dto);
            return Results.Ok(updatedGenre);
        });

        genresGroup.MapDelete("/{id:int}", async (int id, IGenreService genreService) =>
        {
            await genreService.DeleteGenre(id);
            return Results.NoContent();
        });
    }
}