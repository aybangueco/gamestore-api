using GameStoreAPI.Common.Helpers;
using GameStoreAPI.Common.Interfaces;
using GameStoreAPI.DTOs;

namespace GameStoreAPI.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var authGroup = app.MapGroup("/auth");

        authGroup.MapPost("/login", async (CreateUserDto dto, IAuthService authService) =>
        {
            var loginResponse = await authService.LoginAsync(dto);
            return Results.Ok(loginResponse);
        });

        authGroup.MapPost("/register", async (CreateUserDto dto, IAuthService authService) =>
        {
            var registerResponse = await authService.RegisterAsync(dto);
            return Results.Ok(registerResponse);
        });
    }
}