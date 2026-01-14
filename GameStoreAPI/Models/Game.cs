using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Models;

public class Game
{
    [Key]
    public int Id { get; set; }
    
    [Required, StringLength(50)]
    public required string Title { get; set; }
    
    [Required, StringLength(150)]
    public required string Description { get; set; }
    
    [Required, StringLength(50)]
    public required string Genre { get; set; }
    
    [Required, StringLength(50)]
    public required string Publisher { get; set; }
    
    public DateOnly ReleaseDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}