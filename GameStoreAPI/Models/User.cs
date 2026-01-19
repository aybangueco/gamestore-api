using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required, StringLength(25, MinimumLength = 5)]
    public required string Username { get; set; }
    
    [Required, StringLength(128, MinimumLength = 10)]
    public required string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}