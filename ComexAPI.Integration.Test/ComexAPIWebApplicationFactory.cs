using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;

using ComexAPI.Data;
using Testcontainers.PostgreSql;

namespace ComexAPI.Integration.Test
{
    public class ComexAPIWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        public ProdutoContext Context { get; set; }

        private IServiceScope scope;

        private readonly PostgreSqlContainer _postgresqlContainer;

        public ComexAPIWebApplicationFactory()
        {
            _postgresqlContainer = new PostgreSqlBuilder()
                .WithDatabase("produtos_test")
                .WithUsername("postgres")
                .WithPassword("root")
                .WithImage("postgres:16-alpine")
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ProdutoContext>));

                var connectionString = _postgresqlContainer.GetConnectionString();

                services.AddDbContext<ProdutoContext>(options =>
                    options.UseLazyLoadingProxies()
                           .UseNpgsql(connectionString));

                var serviceProvider = services.BuildServiceProvider();

                //using (var scope = serviceProvider.CreateScope())
                //{
                //    var scopedServices = scope.ServiceProvider;
                //    Context = scopedServices.GetRequiredService<ProdutoContext>();

                //    Context.Database.Migrate();
                //}
            });

            base.ConfigureWebHost(builder);
        }

        public async Task InitializeAsync()
        {
            await _postgresqlContainer.StartAsync();
            this.scope = Services.CreateScope();
            Context = scope.ServiceProvider.GetRequiredService<ProdutoContext>();
            Context.Database.Migrate();

        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _postgresqlContainer.DisposeAsync();
        }
    }
}
