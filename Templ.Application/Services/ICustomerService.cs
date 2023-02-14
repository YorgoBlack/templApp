using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templ.Application.Dtos;

namespace Templ.Application.Services
{
    public interface ICustomerService
    {
        Task<bool> ValidateCustomerName(Guid? id, string customerName);
        Task<IEnumerable<CustomerDto>> GetAll();
        Task<CustomerDto> GetById(Guid id);
        Task<CustomerDto> Create(CustomerDto dto);
        Task<CustomerDto> Update(CustomerDto dto);
        Task<int> Delete(Guid id);
        CustomerQueryResult Query(CustomerQueryParams queryParams);
    }
}
