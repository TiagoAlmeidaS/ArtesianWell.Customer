using Customer.Domain.Entities;
using Customer.Infra.Service.Configurations.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infra.Service.Context;

public class CustomerDBContext: DbContext
{
    public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();

    public CustomerDBContext(DbContextOptions<CustomerDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CustomerModelConfiguration());
    }
}