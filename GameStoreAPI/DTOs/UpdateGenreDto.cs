using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.DTOs;

public record UpdateGenreDto(
    [Required] string Name);