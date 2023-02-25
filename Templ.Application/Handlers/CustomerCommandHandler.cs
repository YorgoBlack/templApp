using AutoMapper;
using FluentMediator;
using System.ComponentModel.DataAnnotations;
using Templ.Domain.Customers;
using Templ.Domain.Customers.Commands;
using Templ.Domain.Customers.ValueObjects;

namespace Templ.Application.Handlers;

public class CustomerCommandHandler
{
    private readonly ICustomerFactory _customerFactory;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerCommandHandler(ICustomerFactory customerFactory, 
        ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository= customerRepository;
        _customerFactory= customerFactory;
        _mapper= mapper;
    }

    public async Task<Customer> HandleNewCustomer(CreateNewCustomerCommand command)
    {
        var customer = await _customerFactory.CreateCustomerInstance(
            name: command.Name,
            companyName: new Company(command.CompanyName,command.CompanyAddress),
            phone: command.Phone,
            email: command.Email);

        var entity = await _customerRepository.Add(customer);
        await _customerRepository.SaveAsync();
        return entity;
    }

    public async Task<Customer?> HandleUpdateCustomer(UpdateCustomerCommand command)
    {
        var customer = _mapper.Map<Customer>(command);
        return await _customerRepository.Update(customer);
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
