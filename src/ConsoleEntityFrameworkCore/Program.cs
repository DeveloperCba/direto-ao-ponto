using ConsoleEntityFrameworkCore.Datas;
using ConsoleEntityFrameworkCore.Enumerators;
using ConsoleEntityFrameworkCore.Models;
using System.Reflection;

namespace ConsoleEntityFrameworkCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var conn = "Data Source=LocalDatabase.db";

            using (var context = new ApplicationDbContext(TypeDatabaseEnum.SQLLite, conn))
            {
                var category = new Category
                {
                    Description = "Category 01"
                };
                context.Categories.Add(category);
                context.SaveChanges();

                var product = new Product
                {
                    Description = "Product 01",
                    CategoryId = category.Id,
                    Price = 150.45m
                };

                context.Products.Add(product);
                context.SaveChanges();


                var propriedades = product.GetType().GetProperties();
                foreach (PropertyInfo propriedade in propriedades)
                    Console.WriteLine(propriedade.Name + ": " + propriedade.GetValue(product));
            }


        }
    }
}