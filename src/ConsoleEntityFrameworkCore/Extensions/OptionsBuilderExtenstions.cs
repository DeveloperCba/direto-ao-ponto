using ConsoleEntityFrameworkCore.Enumerators;
using Microsoft.EntityFrameworkCore;

namespace ConsoleEntityFrameworkCore.Extensions
{
    public static class OptionsBuilderExtenstions
    {
        private 
        public static DbContextOptionsBuilder ConfigureConnection(this DbContextOptionsBuilder optionsBuilder,
            TypeDatabaseEnum typeSGDB,
            string connectionString,
            string migratioName ,
            Version version
            )
        {
            switch (typeSGDB)
            {
                case TypeDatabaseEnum.SQLServer:
                    return optionsBuilder
                        .UseSqlServer(connectionString, x => x.MigrationsHistoryTable(migratioName));
                case TypeDatabaseEnum.Postgres:
                    return optionsBuilder
                        .UseNpgsql(connectionString, x => x.MigrationsHistoryTable(migratioName).SetPostgresVersion(version));
                case TypeDatabaseEnum.MySQL:
                    return optionsBuilder
                         .UseMySql(connectionString, new MySqlServerVersion(version), x => x.MigrationsHistoryTable(migratioName));
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
}