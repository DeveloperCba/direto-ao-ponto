using ConsoleEntityFrameworkCore.Extensions;
using ConsoleEntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleEntityFrameworkCore.Mapping
{
    public class ProductMapping : EntityMapping<Product>
    {
        private static string _nameTable = nameof(Product).ToSnakeCase();
        public ProductMapping() : base(_nameTable) { }
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Description).HasMaxLength(80); 
            builder.Property(x => x.Price).HasPrecision(decimalInit,decimalEnd); 
            builder.Property(x => x.CategoryId); 
        }
    }
}