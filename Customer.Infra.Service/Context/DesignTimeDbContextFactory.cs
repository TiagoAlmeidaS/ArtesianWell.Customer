using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Customer.Infra.Service.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CustomerDBContext>
{
    public CustomerDBContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<CustomerDBContext>();
        var connectionString = configuration.GetConnectionString("DB");

        builder.UseNpgsql(connectionString);

        return new CustomerDBContext(builder.Options);
    }
}