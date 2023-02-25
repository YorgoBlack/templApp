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
        return entity;
    }

    public async Task<Customer> Update(Customer entity)
    {
        var tracking  = _context.Set<Customer>()
            .Local
            .FirstOrDefault(e => e.CustomerId.Equals(entity.CustomerId));

        if( tracking != null ) 
        {
            _context.Entry(tracking).State = EntityState.Detached;
        }
        _context.Entry(entity).State= EntityState.Modified;
            
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

    public Task SaveAsync() => _context.SaveChangesAsync();
}