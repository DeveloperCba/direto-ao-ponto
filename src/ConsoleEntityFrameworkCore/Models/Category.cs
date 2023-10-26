namespace ConsoleEntityFrameworkCore.Models;

public class Category : Entity
{
    public required string Description { get; set; }

    public required IEnumerable<Product> Products { get; set; }
}