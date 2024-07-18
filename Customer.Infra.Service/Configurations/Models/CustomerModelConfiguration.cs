using Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Infra.Service.Configurations.Models;

public class CustomerModelConfiguration: IEntityTypeConfiguration<CustomerEntity>
{
    public void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder.HasKey(cm => cm.Id);
        builder.Property(cm => cm.Name).IsRequired();
        builder.Property(cm => cm.Email).IsRequired();
        builder.Property(cm => cm.Document).IsRequired();
        builder.Property(cm => cm.Phone).IsRequired();
        builder.Property(cm => cm.ProfileTypeId).IsRequired();
        builder.Property(medals => medals.UpdatedAt).HasDefaultValueSql("now()");
        builder.Property(medals => medals.CreatedAt).HasDefaultValueSql("now()");
    }
}