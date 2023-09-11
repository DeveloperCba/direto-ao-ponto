using ConsoleNoSql.Configurations;
using ConsoleNoSql.Infrastructures.Repositories;
using ConsoleNoSql.Models;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;

namespace ConsoleNoSql
{

    internal class Program
    {
        static async Task Main(string[] args)
        {
            //DI
            var serviceProvider = DependencieInjectionConfig.ConfigureService() as ServiceProvider;
            var peopleRepository = serviceProvider.GetService<IMongoDbRepository<Person>>();
            var redisRepository = serviceProvider.GetService<IRedisRepository>();

            var person = new Person()
            {
                Id = new ObjectId(),
                FirstName = "John Test",
                LastName = "Doe"
            };
            //MongoDb
            var people = peopleRepository.FilterBy(
                   filter => filter.FirstName != "test",
                   projection => projection
                ).FirstOrDefault();

            if (people != null)
            {
                people.BirthDate = DateTime.Now;
                people.LastName = "Doe teste";
                peopleRepository.ReplaceOne(people);
                Console.WriteLine($"My name is: {people.FirstName} {people.LastName}");
            }
            else
            {
                await peopleRepository.InsertOneAsync(person);
                Console.WriteLine($"My name is: {person.FirstName} {person.LastName}");
            }

            //Redis
            var key = $"person:{person.FirstName}";
            var peopleRedis = redisRepository.GetCache<Person>(key);
            if (people != null)
            {
                //people.BirthDate = DateTime.Now;
                await redisRepository.RemoveCache(key);
                await redisRepository.SetCache(key,people);
                Console.WriteLine($"My name is: {person.FirstName} {person.LastName}");
            }
            else
            {
                await redisRepository.SetCache(key, person);
                Console.WriteLine($"My name is: {person.FirstName} {person.LastName}");
            }

        }
    }
}