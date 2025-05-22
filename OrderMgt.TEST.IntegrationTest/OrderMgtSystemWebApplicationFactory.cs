using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using OrderMgt.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgt.TEST.IntegrationTest
{
    internal class OrderMgtSystemWebApplicationFactory: WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<AppDbContext>));

                var connString = GetConnectionString();
                services.AddSqlServer<AppDbContext>(connString);

                var dbContext = CreateDbContext(services);
                dbContext.Database.Migrate();
                dbContext.Database.EnsureCreated();
            });
        }

        private static string? GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<OrderMgtSystemWebApplicationFactory>()
                .Build();

            var connString = "Server=.;Database=OrderMgtTest;Integrated Security=SSPI;persist security info=True;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";// configuration.GetConnectionString("ConnectionStrings:TestConnection");
            return connString;
        }

        private static AppDbContext CreateDbContext(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            return dbContext;
        }
    }
}
