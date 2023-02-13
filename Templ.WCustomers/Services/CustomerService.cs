using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Templ.WCustomers.Models;

namespace Templ.WCustomers.Services;

public class CustomerService : ICustomerService
{
    private readonly Client _client;
    private readonly HttpClient HttpClient;

    public CustomerService(IConfiguration configuration)
    {
        var url = configuration.GetSection("Services")["Customers"];
        HttpClient = new HttpClient();  
        _client = new Client(url, HttpClient);
    }

    public async Task<IEnumerable<CustomerDto>> QueryCustomersAsync(CustomerQueryParams queryParams)
    {
        return await _client.QueryAsync(queryParams);
    }

    public async Task<CustomerDto> CreateNewAsync(Customer customer)
    {
        return await _client.PostAsync(new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            CompanyName = customer.CompanyName,
            CompanyAddress = customer.CompanyAddress,
            Email = customer.Email,
            Phone = customer.Phone,
        });
    }

    public async Task DeleteAsync(Guid id)
    {
        await _client.DeleteAsync(id);
    }

    public async Task<bool> ValidateCustomerNameAsync(Guid? id, string customerName)
    {
        return await _client.ValidateCustomerNameAsync(id, customerName); 
    }

    public async Task UpdateAsync(Customer customer)
    {
        await _client.PutAsync(new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            CompanyName = customer.CompanyName,
            CompanyAddress = customer.CompanyAddress,
            Email = customer.Email,
            Phone = customer.Phone,
        });
    }
}
