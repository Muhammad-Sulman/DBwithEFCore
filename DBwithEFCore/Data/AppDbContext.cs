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

            modelBuilder.Entity<Language>().HasData(
               new Language() { Id = 1, Title = "Urdu", Description = "Pakistan Urdu" },
               new Language() { Id = 2, Title = "Hindi", Description = "Indian Hindi" },
               new Language() { Id = 3, Title = "English", Description = "USA English" },
               new Language() { Id = 4, Title = "Arabic", Description = "Saudi Arab Arabic" }
               );

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookPrice> BookPrices { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}

/*
 Seeding data, also referred to as adding master data, is the process of populating a database with initial or default data when it is first created or during application setup. This data is typically required for the application to function correctly or to provide foundational options for users.

Understanding Master Data and Seeding
Master Data:

These are foundational records that do not frequently change.
Examples include:
Default user roles (e.g., Admin, User, Moderator).
Categories in an e-commerce app (e.g., Electronics, Furniture, Clothing).
Supported languages in a multi-language app (e.g., English, Spanish, French).
Seeding:

Involves adding this data to the database during the initial setup or migrations.
Ensures that essential records are present in the database after deployment.

Why Seeding is Useful
Consistency Across Environments:
Ensures that the development, staging, and production databases have the same foundational data.
Reduced Manual Effort:
Automates the process of populating essential data, reducing errors and saving time.
Better Application Behavior:
Ensures the application starts with necessary data, preventing runtime issues (e.g., missing roles or categories).

 */