
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Templ.WCustomers.Services;
using Models;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> QueryCustomersAsync(CustomerQueryParams queryParams);
    Task<bool> ValidateCustomerNameAsync(System.Guid? id, string customerName);
    Task<CustomerDto> CreateNewAsync(Customer body);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Guid id);

}
