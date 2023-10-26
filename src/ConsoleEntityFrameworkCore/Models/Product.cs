namespace ConsoleEntityFrameworkCore.Models;

public class Product : Entity
{
    public required string Description { get; set; }
    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
}