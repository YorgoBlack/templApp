using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Templ.Infrastucture.Repositories;
using Domain.Customers;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(nameof(Customer));
        builder.HasKey(x => x.CustomerId);
        builder.OwnsOne(d => d.Company, a => 
        { 
            a.Property(p => p.Name).HasColumnName("CompanyName");
            a.Property(p => p.Address).HasColumnName("CompanyAddress"); 
         });
    }
}

