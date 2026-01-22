using GameStoreAPI.Common.Helpers;
using GameStoreAPI.Common.Interfaces;
using GameStoreAPI.DTOs;

namespace GameStoreAPI.Services;

public class AuthService(ILogger<AuthService> logger, IConfiguration config, IUserService userService): IAuthService
{
    public async Task<AuthResponse> LoginAsync(CreateUserDto dto)
    {
        var existingUser = await userService.GetUserByUsernameAsync(dto.Username);

        if (existingUser is null)
        {
            logger.LogInformation("User not found with username: {Username}", dto.Username);
            throw new BadHttpRequestException("Invalid username or password");
        }

        var isPasswordMatch = UserManager.IsPasswordMatch(existingUser.Password, dto.Password);
        if (!isPasswordMatch)
        {
            logger.LogInformation("Password does not match");
            throw new BadHttpRequestException("Invalid username or password");
        }

        var accessToken = Token.GenerateAccessToken(existingUser.Id, config);
        var refreshToken = Token.GenerateRefreshToken(existingUser.Id, config);

        logger.LogInformation("User logged in successfully");
        return new AuthResponse(accessToken, refreshToken, "Logged in successfully");
    }

    public async Task<AuthResponse> RegisterAsync(CreateUserDto dto)
    {
        var existingUser = await userService.GetUserByUsernameAsync(dto.Username);
        if (existingUser is not null)
        {
            logger.LogInformation("User with username: {Username} already exists", dto.Username);
            throw new BadHttpRequestException("Username already taken");
        }

        var hashedPassword = UserManager.HashPassword(dto.Password);

        var newUser = new CreateUserDto(dto.Username, hashedPassword);
        var newUserId = await userService.CreateUserAsync(newUser);
            
        var accessToken = Token.GenerateAccessToken(newUserId, config);
        var refreshToken = Token.GenerateRefreshToken(newUserId, config);
            
        logger.LogInformation("User registered successfully");
        return new AuthResponse(accessToken, refreshToken, "Registered successfully");
    }
}