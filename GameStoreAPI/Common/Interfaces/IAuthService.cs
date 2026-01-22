using GameStoreAPI.DTOs;

namespace GameStoreAPI.Common.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(CreateUserDto dto);
    Task<AuthResponse> RegisterAsync(CreateUserDto dto);
}