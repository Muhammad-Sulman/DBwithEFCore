using DBwithEFCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DBwithEFCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // Adding Master Data

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency() { Id = 1, Title = "PKR", Description = "Pakistan Rupee" },
                new Currency() { Id = 2, Title = "IND", Description = "Indian Rupee" },
                new Currency() { Id = 3, Title = "Euro", Description = "Italy Euro" },
                new Currency() { Id = 4, Title = "Dollar", Description = "USA Dollar" }
                );

            modelBuilder.Entity<Currency>().HasData(
               new Currency() { Id = 1, Title = "PKR", Description = "Pakistan Rupee" },
               new Currency() { Id = 2, Title = "IND", Description = "Indian Rupee" },
               new Currency() { Id = 3, Title = "Euro", Description = "Italy Euro" },
               new Currency() { Id = 4, Title = "Dollar", Description = "USA Dollar" }
               );

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookPrice> BookPrices { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
