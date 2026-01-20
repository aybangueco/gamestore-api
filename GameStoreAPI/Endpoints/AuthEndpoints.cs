using GameStoreAPI.Common.Helpers;
using GameStoreAPI.Common.Interfaces;
using GameStoreAPI.DTOs;

namespace GameStoreAPI.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var authGroup = app.MapGroup("/auth");

        authGroup.MapPost("/login", async (CreateUserDto dto, IUserService userService, ILogger logger, IConfiguration config) =>
        {
            var existingUser = await userService.GetUserByUsernameAsync(dto.Username);

            if (existingUser is null)
            {
                logger.LogInformation("User not found with username: {Username}", dto.Username);
                return Results.Unauthorized();
            }

            var isPasswordMatch = UserManager.IsPasswordMatch(existingUser.Password, dto.Password);
            if (!isPasswordMatch)
            {
                logger.LogInformation("Password does not match");
                return Results.Unauthorized();
            }

            var accessToken = Token.GenerateAccessToken(existingUser.Id, config);
            var refreshToken = Token.GenerateRefreshToken(existingUser.Id, config);

            logger.LogInformation("User logged in successfully");
            return Results.Ok(new { accessToken, refreshToken, message = "Logged in successfully" });
        });

        authGroup.MapPost("/register", async (CreateUserDto dto, IUserService userService, ILogger logger, IConfiguration config) =>
        {
            var existingUser = await userService.GetUserByUsernameAsync(dto.Username);
            if (existingUser is not null)
            {
                logger.LogInformation("User with username: {Username} already exists", dto.Username);
                return Results.BadRequest(new { message = "Username is already taken!" });
            }

            var hashedPassword = UserManager.HashPassword(dto.Password);

            var newUser = new CreateUserDto(dto.Username, hashedPassword);
            var newUserId = await userService.CreateUserAsync(newUser);
            
            var accessToken = Token.GenerateAccessToken(newUserId, config);
            var refreshToken = Token.GenerateRefreshToken(newUserId, config);
            
            logger.LogInformation("User registered successfully");
            return Results.Ok(new { accessToken, refreshToken, message = "Logged in successfully" });
        });
    }
}