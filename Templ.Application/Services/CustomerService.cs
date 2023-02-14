using AutoMapper;
using FluentMediator;
using System.Collections.Generic;
using System.Linq;

namespace Templ.Application.Services;
using Dtos;
using Domain;
using Domain.Customers;
using Domain.Customers.Commands;

public class CustomerService : ICustomerService
{
    readonly ICustomerRepository _customerRepository;
    readonly IMapper _mapper;
    readonly IMediator _mediator;
    readonly ICustomerQuery _customerQuery;

    public CustomerService(ICustomerRepository customerRepository, ICustomerQuery customerQuery, IMapper mapper, IMediator mediator)
    {
        _customerRepository = customerRepository;
        _customerQuery = customerQuery;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<CustomerDto> Create(CustomerDto customerViewModel)
    {
        var command = _mapper.Map<CreateNewCustomerCommand>(customerViewModel);
        var result = await _mediator.SendAsync<Customer>(command);
        return _mapper.Map<CustomerDto>(result);
    }

    public async Task<int> Delete(Guid id)
    {
        var command = _mapper.Map<DeleteCustomerCommand>(id);
        return await _mediator.SendAsync<int>(command);
    }

    public async Task<IEnumerable<CustomerDto>> GetAll()
    {
        var customers = await _customerRepository.FindAll();
        var models = customers.Select(i => _mapper.Map<CustomerDto>(i));
        return models;
    }

    public async Task<CustomerDto> GetById(Guid id)
    {
        var result = await _customerRepository.FindById(id);
        return _mapper.Map<CustomerDto>(result);
    }

    public async Task<bool> ValidateCustomerName(Guid? id, string customerName)
    {
        if (customerName == null) return false;

        var customer = await _customerRepository.FindByName(customerName);

        if (customer == null) return true;

        return customer.CustomerId == id;
    }

    public CustomerQueryResult Query(CustomerQueryParams queryParams)
    {
        queryParams.PageSize++;
        var customers = _customerQuery.Query(queryParams);
        var records = customers.Select(i => _mapper.Map<CustomerDto>(i));
        return new CustomerQueryResult
        {
            IsLastRecords = queryParams.PageSize != records?.Count(),
            Records = records?.Take(--queryParams.PageSize)
        };
    }

    public async Task<CustomerDto> Update(CustomerDto customerViewModel)
    {
        var command = _mapper.Map<Customer>(customerViewModel);
        var result = await _customerRepository.Update(command);
        return _mapper.Map<CustomerDto>(result);
    }
}