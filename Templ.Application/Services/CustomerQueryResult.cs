

namespace Templ.Application.Services;
using Domain.Customers;
using Dtos;

public class CustomerQueryResult
{
    public IEnumerable<CustomerDto>? Records { get; set; }

    public bool IsLastRecords { get; set; }
}
