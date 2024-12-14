using WebCatalogOfFootballers.Models;
using Microsoft.EntityFrameworkCore;

namespace WebCatalogOfFootballers.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().HasData(
                    new Gender {Id = 1, GenderName = "Мужчина" },
                    new Gender { Id = 2, GenderName = "Женщина" }
            );

            modelBuilder.Entity<Country>().HasData(
                 new Country { Id = 1, CountryName = "Россия" },
                 new Country { Id = 2, CountryName = "США" },
                 new Country { Id = 3, CountryName = "Италия" }
            );
        }

        public DbSet<Footballer> Footballers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<TeamName> TeamNames { get; set; }
    }
}
