namespace Templ.Infrastucture.Factories;
using Domain.Customers.ValueObjects;

public class CustomerFactory : Domain.Customers.Customer
{
    public CustomerFactory(string name, Company companyName, string phone, string email) : 
        base(Guid.NewGuid(), name, companyName, phone, email)
    {
    }
}
