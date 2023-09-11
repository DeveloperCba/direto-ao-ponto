﻿using ConsoleNoSql.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ConsoleNoSql.Configurations
{
    public static class MongoDbConfiguration
    {
        public static IServiceCollection AddMongoDbConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Value;

            var mongoDbSettingsSection = configuration.GetSection(nameof(MongoDbSettings));
            services.Configure<MongoDbSettings>(mongoDbSettingsSection);
            var cacheSettings = mongoDbSettingsSection.Get<MongoDbSettings>();
            services.AddSingleton<IMongoClient, MongoClient>(option =>
            {
                return new MongoClient(cacheSettings.ConnectionStrings);
            });
            return services;
        }
    }
}
