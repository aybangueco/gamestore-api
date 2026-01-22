namespace GameStoreAPI.DTOs;

public record AuthResponse(
    string AccessToken,
    string RefreshToken,
    string Message);