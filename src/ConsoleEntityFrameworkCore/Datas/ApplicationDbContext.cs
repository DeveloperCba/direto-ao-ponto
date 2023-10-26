using System.Reflection;
using ConsoleEntityFrameworkCore.Enumerators;
using ConsoleEntityFrameworkCore.Extensions;
using ConsoleEntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleEntityFrameworkCore.Datas;

public  class ApplicationDbContext: DbContext
{
    private readonly TypeDatabaseEnum _serverDB;
    private readonly string _conn;

    public ApplicationDbContext(
        TypeDatabaseEnum serverDb,
        string conn)
    {
        _serverDB = serverDb;
        _conn = conn;
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        //EntityFrameworkCore 5 above
        configurationBuilder.ConfigureColumnTypeConvention(_serverDB);
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureConnection(_serverDB, _conn,"__table_migration");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ConfigureEntityRelationship(DeleteBehavior.Cascade);
        modelBuilder.ToSnakeCaseNames();
    }
}