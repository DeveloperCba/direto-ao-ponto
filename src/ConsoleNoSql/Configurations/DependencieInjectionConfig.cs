using ConsoleNoSql.Helpers;
using ConsoleNoSql.Infrastructures.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace ConsoleNoSql.Configurations;

public static class DependencieInjectionConfig
{
    public static ServiceProvider ConfigureService()
    {
        var configuration = AppSettingsExtensions.GetConfigurationAppSettings();

        var redisSettings = configuration.GetAppSettings<RedisSettings>(nameof(RedisSettings));
        var mongoDbSettings = configuration.GetAppSettings<MongoDbSettings>(nameof(MongoDbSettings));

        var serviceProvider = new ServiceCollection()

                .AddLogging(options =>
                {
                    options.AddConsole();
                    options.AddDebug();
                })
                .AddMongoDbConfiguration(configuration)
                .AddSingleton<IConfiguration>(provider => configuration)
                .Configure<RedisSettings>(configuration.GetSection(nameof(RedisSettings)))
                //.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)))
                .AddScoped<IRedisRepository, RedisRepository>()
                .AddScoped(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>))
                .AddRedisConfiguration(configuration)

            ;

        ServiceProvider provider = serviceProvider.BuildServiceProvider();
        return provider;
    }
}