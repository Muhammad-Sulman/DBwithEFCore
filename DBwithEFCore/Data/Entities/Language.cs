using DBwithEFCore.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DBwithEFCore.Data.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // This establishes the one-to-many relationship between Language and Book.
        public virtual ICollection<Book> Books { get; set; } // A navigation property representing a collection of Book entities associated with this language. This establishes the one-to-many relationship between Language and Book.
    }
}


/* 
  Database Relationship
The classes represent a one-to-many relationship between Language and Book.

One-to-Many Relationship Explanation:

One language(Language) can have many books (Book), but each book belongs to exactly one language.
Entity Framework Representation:

Language Table:
Primary Key: Id
Columns: Title, Description
Book Table:
Primary Key: Id
Foreign Key: LanguageId
Columns: Title, Description, NoOfPages, IsActive, CreatedOn
How EF Core Maps the Relationship:

Navigation Property:
The Language class has a collection(ICollection<Book> Books) representing all books in that language.
The Book class has a property(Language Language) to access its associated language.
Foreign Key:
Book.LanguageId acts as the foreign key linking to Language.Id.
Behavior:

When querying a Book entity, you can include its Language property to fetch the associated language.
When querying a Language entity, you can include its Books property to fetch all associated books.


*/