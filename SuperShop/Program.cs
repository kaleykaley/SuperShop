using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop
{

    // ASP.NET_MVC_15 10:40

    public class Program
    {
        // criar uma aplicacao, arrancar a app com configuracoes injectadas
        // no startup
        public static void Main(string[] args)
        {
            // first just construct the host mas nao arranca ainda
            // host: codigo para poder funcionar em qualquer sistema operativo
            var host = CreateHostBuilder(args).Build();

            //then use it to correr o seeding
            RunSeeding(host); // se nao existir a base de dados, cria a base de dados e popula com os produtos
            host.Run();


            // host- deixa correr a applicação em qualquer sistema operativo
            //CreateHostBuilder(args).Build().Run();
        }

        private static void RunSeeding(IHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using(var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<SeedDb>();
                seeder.SeedAsync().Wait();
            };
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
