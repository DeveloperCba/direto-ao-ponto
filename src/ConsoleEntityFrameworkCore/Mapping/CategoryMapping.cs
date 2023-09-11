using ConsoleEntityFrameworkCore.Extensions;
using ConsoleEntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleEntityFrameworkCore.Mapping
{
    public class CategoryMapping : EntityMapping<Category>
    {
        private static string _nameTable = nameof(Category).ToSnakeCase();
        public CategoryMapping() : base(_nameTable) { }
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Description).HasMaxLength(80);
;
            builder
                .HasMany(x => x.Products)
                .WithOne(op => op.Category)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
