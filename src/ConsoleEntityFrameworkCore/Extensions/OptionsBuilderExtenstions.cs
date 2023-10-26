using ConsoleEntityFrameworkCore.Enumerators;
using Microsoft.EntityFrameworkCore;

namespace ConsoleEntityFrameworkCore.Extensions;

public static class OptionsBuilderExtenstions
{
        public static DbContextOptionsBuilder ConfigureConnection(this DbContextOptionsBuilder optionsBuilder,
            TypeDatabaseEnum typeSGDB,
            string connectionString ,
            string migratioName = "__table_migration"  
        )
    {
        switch (typeSGDB)
        {
            case TypeDatabaseEnum.SQLServer:
                return optionsBuilder
                    .UseSqlServer(connectionString, x => x.MigrationsHistoryTable(migratioName));
            case TypeDatabaseEnum.Postgres:
                return optionsBuilder
                    .UseNpgsql(connectionString, x => x.MigrationsHistoryTable(migratioName).SetPostgresVersion(new Version(14,0)));
            case TypeDatabaseEnum.MySQL:
                return optionsBuilder
                    .UseMySql(connectionString, new MySqlServerVersion(new Version(5,7)), x => x.MigrationsHistoryTable(migratioName));
            case TypeDatabaseEnum.Oracle:
                return optionsBuilder
                    .UseOracle(connectionString,  x => x.MigrationsHistoryTable(migratioName));
            case TypeDatabaseEnum.SQLLite:
                return optionsBuilder
                    .UseSqlite(connectionString, x => x.MigrationsHistoryTable(migratioName));

            default:
                break;
        }
        return optionsBuilder;
    }
}