using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Seed initial categories
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Paintings" },
                new Category { Id = 2, Name = "Drawings" }
            );

            base.OnModelCreating(builder);
        }
    }
}
