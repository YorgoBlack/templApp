using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace templ.WpfClient.Services;

public class CustomerService : IService
{
    private readonly Client _client;
    private readonly HttpClient HttpClient;

    public CustomerService(IConfiguration configuration)
    {
        var url = configuration.GetSection("Services")["Customers"];
        HttpClient = new HttpClient();  
        _client = new Client(url, HttpClient);
    }

    public event EventHandler<IEnumerable<CustomerDto>>? CustomersLoadedEvent;

    public async Task QueryCustomers()
    {
        var res = await _client.QueryAsync(new CustomerQueryParams());
        await Task.Delay(5000);
        CustomersLoadedEvent?.Invoke(null, res);
    }
}
