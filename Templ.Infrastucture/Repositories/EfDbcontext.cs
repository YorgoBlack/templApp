using Microsoft.EntityFrameworkCore;

namespace Templ.Infrastucture.Repositories;
using Domain.Customers;

public class EfDbContext : DbContext
{
    public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }

    public virtual DbSet<Customer> Customers { get; set; }
}

