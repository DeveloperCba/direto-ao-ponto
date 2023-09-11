namespace ConsoleEntityFrameworkCore.Models
{
    public class Product : Entity
    {
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}