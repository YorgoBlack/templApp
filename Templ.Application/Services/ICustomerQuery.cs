namespace Templ.Application.Services;
using Domain.Customers;
using Templ.Application.Dtos;

public interface ICustomerQuery
{
    IEnumerable<Customer> Query(CustomerQueryParams query);
}
