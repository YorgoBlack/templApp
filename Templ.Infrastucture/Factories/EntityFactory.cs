namespace Templ.Infrastucture.Factories;
using Domain.Customers;
using Domain.Customers.ValueObjects;
using System.ComponentModel.DataAnnotations;
using Templ.Application.Services;

public class EntityFactory : ICustomerFactory
{
    private readonly ICustomerService _customerService;
    public EntityFactory(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<Customer> CreateCustomerInstance(string name, Company companyName, string phone, string email)
    {
        var res = await _customerService.ValidateCustomerName(null, name);
        if (!res) throw new ValidationException("Customer Name");
        return new CustomerFactory(name, companyName, phone, email);
    }
}
