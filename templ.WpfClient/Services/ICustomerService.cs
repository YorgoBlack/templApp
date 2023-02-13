using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace templ.WpfClient.Services
{
    public interface IService
    {
        public event EventHandler<IEnumerable<CustomerDto>> CustomersLoadedEvent;
        public Task QueryCustomers();
    }
}
