namespace DBwithEFCore.Data.Entities
{
    public class BookPrice
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int currencyId { get; set; }
        public Currency currency { get; set; }
    }
}
