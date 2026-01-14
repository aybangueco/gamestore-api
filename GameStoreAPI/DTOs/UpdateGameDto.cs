using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.DTOs;

public record UpdateGameDto(
    [Required] string Title,
    [Required] string Description,
    [Required] int GenreId,
    [Required] string Publisher,
    [Required] DateOnly ReleaseDate);