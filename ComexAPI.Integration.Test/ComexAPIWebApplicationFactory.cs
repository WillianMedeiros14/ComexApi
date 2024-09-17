using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;

using ComexAPI.Data;

namespace ComexAPI.Integration.Test
{
    public class ComexAPIWebApplicationFactory : WebApplicationFactory<Program>
    {
        public ProdutoContext Context { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ProdutoContext>));

                var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL_TEST")
                     ?? "Server=localhost;Port=5433;Database=produtos_test;Username=postgres;Password=Bemol@99";

                services.AddDbContext<ProdutoContext>(options =>
                    options.UseLazyLoadingProxies()
                           .UseNpgsql(connectionString));

                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    Context = scopedServices.GetRequiredService<ProdutoContext>();

                    Context.Database.Migrate();
                }
            });

            base.ConfigureWebHost(builder);
        }
    }
}
