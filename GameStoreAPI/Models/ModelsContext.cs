using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Models;

public class ModelsContext: DbContext
{
    public ModelsContext(DbContextOptions<ModelsContext> options): base(options) {}
    public DbSet<Game> Games { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Genre>().HasIndex(g => g.Name).IsUnique();
    }
}