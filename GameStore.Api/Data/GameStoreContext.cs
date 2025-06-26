using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

// Gateway to the database
// Add New Migrations with thi command
// dotnet ef migrations add MIGRATION_NAME --output-dir Data/Migrations
public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
  // Define a new Table for each entity
  public DbSet<Game> Games => Set<Game>();

  public DbSet<Genre> Genres => Set<Genre>();

  // Data Seeding 
  // Like Fixture From Other Django Project
  // ONLY FOR STATIC DATA
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Genre>().HasData(
      new { Id = 1, Name = "Fighting" },
      new { Id = 2, Name = "Roleplaying" },
      new { Id = 3, Name = "Sports" },
      new { Id = 4, Name = "Racing" },
      new { Id = 5, Name = "Kids and Family" }
    );
  }
}
