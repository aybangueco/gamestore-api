using GameStoreAPI.Common.Interfaces;
using GameStoreAPI.DTOs;

namespace GameStoreAPI.Endpoints;

public static class GamesEndpoints
{
    public static void MapGamesEndpoint(this WebApplication app)
    {
        var gamesGroup = app.MapGroup("/games").RequireAuthorization();
        
        gamesGroup.MapGet("/", async (IGameService gameService) =>
        {
            var games = await gameService.GetGames();
            return Results.Ok(new { games });
        });

        gamesGroup.MapGet("/{id:int}", async (int id, IGameService gameService) =>
        {
            var game = await gameService.GetGame(id);
            return game is not null ? Results.Ok(new { game }) : Results.NotFound(new { statusCode = 404, message = "Game not found" });
        });

        gamesGroup.MapPost("/", async (CreateGameDto dto, IGameService gameService) =>
        {
            var game = await gameService.AddGame(dto);
            return Results.Created($"/games/{game.Id}", game);
        });

        gamesGroup.MapPut("/{id:int}", async (int id, UpdateGameDto dto, IGameService gameService) =>
        {
            var game = await gameService.UpdateGame(id, dto);
            return Results.Ok(game);
        });

        gamesGroup.MapDelete("/{id:int}", async (int id, IGameService gameService) =>
        {
            await gameService.DeleteGame(id);
            return Results.NoContent();
        });
    }
}