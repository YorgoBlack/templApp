using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Templ.Infrastucture.Repositories;
using Domain.Customers;


public class CustomerRespository : ICustomerRepository
{
    protected readonly EfDbContext _context;
        
    public CustomerRespository(EfDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> Add(Customer entity)
    {
        await _context.Set<Customer>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Customer> Update(Customer entity)
    {
        _context.Set<Customer>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<Customer>> FindAll()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> FindById(Guid id)
    {
        return await _context.Set<Customer>().FindAsync(id);
    }

    public async Task<Customer?> FindByName(string name)
    {
        return await _context.Set<Customer>().SingleOrDefaultAsync(c=>c.Name.Equals(name));
    }

    public async Task<int> Remove(Customer entity)
    {
        _context.Set<Customer>().Remove(entity);
        return await _context.SaveChangesAsync();
    }
}