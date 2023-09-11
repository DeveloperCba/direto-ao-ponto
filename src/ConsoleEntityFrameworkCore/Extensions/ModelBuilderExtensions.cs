using ConsoleEntityFrameworkCore.Enumerators;
using Microsoft.EntityFrameworkCore;

namespace ConsoleEntityFrameworkCore.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ConfigureDatabaseServer(this ModelBuilder modelBuilder, TypeDatabaseEnum typeSGDB)
        {
            switch (typeSGDB)
            {
                case TypeDatabaseEnum.SQLServer:
                    foreach (var property in modelBuilder.Model.GetEntityTypes()
                                 .SelectMany(e => e.GetProperties()
                                     .Where(p => p.ClrType == typeof(string))))
                        property.SetColumnType("varchar");
                    return modelBuilder;

                case TypeDatabaseEnum.Postgres:
                    foreach (var property in modelBuilder.Model.GetEntityTypes()
                                 .SelectMany(e => e.GetProperties()
                                     .Where(p => p.ClrType == typeof(string))))
                        property.SetColumnType("character varying");

                    return modelBuilder;

                case TypeDatabaseEnum.MySQL:
                    foreach (var property in modelBuilder.Model.GetEntityTypes()
                                 .SelectMany(e => e.GetProperties()
                                     .Where(p => p.ClrType == typeof(string))))
                        property.SetColumnType("varchar");
                    return modelBuilder;

                case TypeDatabaseEnum.Oracle:
                    foreach (var property in modelBuilder.Model.GetEntityTypes()
                                 .SelectMany(e => e.GetProperties()
                                     .Where(p => p.ClrType == typeof(string))))
                        property.SetColumnType("varchar2");
                    return modelBuilder;

                case TypeDatabaseEnum.SQLLite:
                    foreach (var property in modelBuilder.Model.GetEntityTypes()
                                 .SelectMany(e => e.GetProperties()
                                     .Where(p => p.ClrType == typeof(string))))
                        property.SetColumnType("TEXT");
                    return modelBuilder;

                default:
                    return modelBuilder;
            }
        }

        public static ModelConfigurationBuilder ConfigureColumnTypeConvention(this ModelConfigurationBuilder configurationBuilder,
            TypeDatabaseEnum typeSGDB)
        {
            configurationBuilder.Properties<string>()
                .AreUnicode(false);
            switch (typeSGDB)
            {
                case TypeDatabaseEnum.SQLServer:
                    configurationBuilder.Properties<string>().AreUnicode(false).HaveColumnType("varchar");
                    break;

                case TypeDatabaseEnum.Postgres:
                    configurationBuilder.Properties<string>().AreUnicode(false).HaveColumnType("character varying");
                    break;

                case TypeDatabaseEnum.MySQL:
                    configurationBuilder.Properties<string>().AreUnicode(false).HaveColumnType("varchar");
                    break;

                case TypeDatabaseEnum.Oracle:
                    configurationBuilder.Properties<string>().AreUnicode(false).HaveColumnType("varchar2");
                    break;

                case TypeDatabaseEnum.SQLLite:
                    configurationBuilder.Properties<string>().AreUnicode(false).HaveColumnType("text");
                    break;

                default:
                    break;
            }

            return configurationBuilder;
        }

        public static ModelBuilder ConfigureEntityRelationship(this ModelBuilder modelBuilder, DeleteBehavior behavior)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = behavior;

            return modelBuilder;
        }



    }
}