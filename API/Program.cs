using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.System.Commands;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetService<IConfiguration>();

                UpdateDatabase(configuration);

                var mediator = services.GetRequiredService<IMediator>();
                await mediator.Send(new SeedSampleDataCommand(), CancellationToken.None);
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    if (env.IsDevelopment())
                    {
                        var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                        if (appAssembly != null)
                        {
                            config.AddUserSecrets(appAssembly, optional: true);
                        }
                    }

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .UseStartup<Startup>();

        /// <summary>
        /// Updates the database (applies EF migrations)
        /// </summary>
        private static void UpdateDatabase(IConfiguration config)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("AppDatabase_SA"));

            // TODO: uncomment after setup database
            //using var appDbContext = new AppDbContext(optionsBuilder.Options);
            //appDbContext.Database.Migrate();
        }
    }
}
