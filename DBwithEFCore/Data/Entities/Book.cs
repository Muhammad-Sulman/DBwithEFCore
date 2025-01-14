namespace DBwithEFCore.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NoOfPages { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }


        public int LanguageId { get; set; } // FK
        public Language Language { get; set; }
    }
}
