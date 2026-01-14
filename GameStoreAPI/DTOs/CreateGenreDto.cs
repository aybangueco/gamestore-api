using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.DTOs;

public record CreateGenreDto(
    [Required] string Name);