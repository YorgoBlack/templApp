namespace Templ.Application.Services;
using Domain.Customers;

public interface ICustomerQuery
{
    IEnumerable<Customer> Query(CustomerQueryParams query);
}
