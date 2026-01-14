using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.DTOs;

public record CreateGameDto(
    [Required] string Title,
    [Required] string Description,
    [Required] string Genre,
    [Required] string Publisher,
    [Required] DateOnly ReleaseDate);