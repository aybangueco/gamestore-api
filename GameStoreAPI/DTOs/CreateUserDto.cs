using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.DTOs;

public record CreateUserDto(
    [Required]
    [StringLength(25, MinimumLength = 5)]
    string Username,
    
    [Required]
    [StringLength(50, MinimumLength = 10)]
    string Password);