namespace ConsoleEntityFrameworkCore.Models
{
    public class Category : Entity
    {
        public string Description { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
