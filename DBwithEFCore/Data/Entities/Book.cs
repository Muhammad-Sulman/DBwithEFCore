﻿namespace DBwithEFCore.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NoOfPages { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }


        public int LanguageId { get; set; } // Acts as a foreign key (FK) linking to the Language entity. This defines which language the book is written in.
        public Language Language { get; set; } // A navigation property that enables accessing the related Language entity for the book. It represents the relationship between the book and its language.
    }
}
