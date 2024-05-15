using GameStore.API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data
{
    public class GameStoreContext : IdentityDbContext<ApplicationUser>
    {
        public GameStoreContext(DbContextOptions<GameStoreContext> options) 
            : base(options)
        {
        }

        public DbSet<Game> Games => Set<Game>();
        public DbSet<Genre> Genres => Set<Genre>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure the base configurations for Identity are applied

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Fighting" },
                new Genre { Id = 2, Name = "Action" },
                new Genre { Id = 3, Name = "Adventure" },
                new Genre { Id = 4, Name = "RPG" },
                new Genre { Id = 5, Name = "Strategy" },
                new Genre { Id = 6, Name = "Simulation" },
                new Genre { Id = 7, Name = "Sports" },
                new Genre { Id = 8, Name = "Puzzle" },
                new Genre { Id = 9, Name = "Platformer" },
                new Genre { Id = 10, Name = "Action-Adventure" },
                new Genre { Id = 11, Name = "Kids and Family" }
            );
        }
    }
}
