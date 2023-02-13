using FluentMediator;
using Templ.Domain.Customers;
using Templ.Domain.Customers.Commands;
using Templ.Domain.Customers.ValueObjects;

namespace Templ.Application.Handlers;

public class CustomerCommandHandler
{
    private readonly ICustomerFactory _customerFactory;
    private readonly ICustomerRepository _customerRepository;

    public CustomerCommandHandler(ICustomerFactory customerFactory, ICustomerRepository customerRepository, IMediator mediator)
    {
        _customerRepository= customerRepository;
        _customerFactory= customerFactory;
    }

    public async Task<Customer> HandleNewCustomer(CreateNewCustomerCommand command)
    {
        var customer = await _customerFactory.CreateCustomerInstance(
            name: command.Name,
            companyName: new Company(command.CompanyName,command.CompanyAddress),
            phone: command.Phone,
            email: command.Email);

        return await _customerRepository.Add(customer);
    }

    public async Task<Customer?> HandleUpdateCustomer(UpdateCustomerCommand command)
    {
        var customer = await _customerRepository.FindById(command.Id);
        if( customer != null)
        {
            return await _customerRepository.Update(customer);
        }
        return null;
    }

    public async Task<int> HandleDeleteCustomer(DeleteCustomerCommand command)
    {
        var customer = await _customerRepository.FindById(command.Id);
        if (customer != null)
        {
            return await _customerRepository.Remove(customer);
        }
        return 0;
    }
}
