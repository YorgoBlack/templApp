namespace Templ.Domain.Customers;
using Customers.ValueObjects;

public interface ICustomerFactory
{
    Task<Customer> CreateCustomerInstance(string name, Company companyName, string phone, string email);
}
