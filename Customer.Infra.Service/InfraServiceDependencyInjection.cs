using System.Text.Json;
using Customer.Application.Interfaces;
using Customer.Domain.Repositories;
using Customer.Infra.Service.Configurations;
using Customer.Infra.Service.Context;
using Customer.Infra.Service.Repositories;
using Customer.Infra.Service.Unit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Customer.Infra.Service;

public static class InfraServiceDependencyInjection
{
    public static IServiceCollection InfraServiceExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbConnection(configuration)
            .AddConfiguration(configuration)
            .AddRepositories()
            .AddGlobalConfiguration();

        return services;
    }
    
    private static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var scope = services.BuildServiceProvider().CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var customerDbConfig = serviceProvider.GetRequiredService<IOptionsSnapshot<CustomerDbConfig>>();

        if (customerDbConfig.Value.UseInMemoryDatabase)
        {
            services.AddDbContextPool<CustomerDBContext>(options =>
                options.UseInMemoryDatabase("TestingDB"));
        }
        else
        {
            services.AddDbContextPool<CustomerDBContext>(options => options.UseNpgsql(configuration.GetConnectionString("DB"))
                .EnableSensitiveDataLogging() 
                .LogTo(Console.WriteLine, LogLevel.Information));
            ;
        }

        return services;
    }
    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddTransient<ICustomerRepository, CustomerRepository>()
            .AddTransient<IUnitOfWork, UnitOfWorkCustomer>();

    private static IServiceCollection
        AddConfiguration(this IServiceCollection services, IConfiguration configuration) =>
        services
            .Configure<CustomerDBContext>(configuration.GetSection(nameof(CustomerDBContext)))
            .Configure<CustomerDbConfig>(configuration.GetSection(nameof(CustomerDbConfig)));
    

    private static IServiceCollection AddGlobalConfiguration(this IServiceCollection services)
        => services.AddSingleton(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        });
}