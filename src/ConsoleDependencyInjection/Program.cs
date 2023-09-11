using ConsoleDependencyInjection.Configurations;
using ConsoleDependencyInjection.Helpers;
using ConsoleDependencyInjection.Models;
using ConsoleDependencyInjection.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ConsoleDependencyInjection
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //DI
            var serviceProvider = DependencieInjectionConfig.ConfigureService() as ServiceProvider;
            var cepService = serviceProvider.GetService<ICepService>();

            Console.Write($"{new string('*',10)} CONSOLE DEPENDENCIE INJECTION {new string('*', 10)}");
            Console.WriteLine();

            Console.Write("Digite o Cep..: ");
            var cep = Console.ReadLine().ReturnNumberOnly();

            if (cep.Length == 8)
            {
                var cepRequest = new CepRequest { Cep = cep };
                var cepResponse = await cepService.GetCepAsync(cepRequest);

                if (cepResponse is not null)
                {
                    var propriedades = cepResponse.GetType().GetProperties();
                    foreach (PropertyInfo propriedade in propriedades)
                        Console.WriteLine(propriedade.Name + ": " + propriedade.GetValue(cepResponse));
                }
                else
                {
                    Console.WriteLine("Cep não encontrado!");
                }
            }
            else
            {
                Console.WriteLine("Cep informado não é válido!!!");
            }


            Console.ReadKey();
        }
    }
}