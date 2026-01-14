using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Models;

public class Genre
{
    [Key]
    public int Id { get; set; }
    
    [Required, StringLength(20)]
    public required string Name { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Game> Games { get; set; } = new List<Game>();
}