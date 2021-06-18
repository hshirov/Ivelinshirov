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

        public virtual DbSet<Artwork> Artworks { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Biography> Biography { get; set; }
        public virtual DbSet<ContactInfo> ContactInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Seed initial categories
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Paintings" },
                new Category { Id = 2, Name = "Drawings" }
            );

            builder.Entity<Biography>().HasData(
                new Biography { Id = 1 , Text = "" }
            );

            builder.Entity<ContactInfo>().HasData(
                new ContactInfo { Id = 1, ReceiverEmail = "reciver@ivelinshirov.com" }
            );

            base.OnModelCreating(builder);
        }
    }
}
