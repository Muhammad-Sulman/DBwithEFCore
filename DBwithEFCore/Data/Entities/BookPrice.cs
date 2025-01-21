namespace DBwithEFCore.Data.Entities
{
    public class BookPrice
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public int currencyId { get; set; }
        public virtual Currency currency { get; set; }
    }
}
