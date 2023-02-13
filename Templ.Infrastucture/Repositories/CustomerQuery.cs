namespace Templ.Infrastucture.Repositories;

using Application.Services;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Templ.Domain.Customers;
using Templ.Domain.Customers.ValueObjects;

class CustomerQuery : ICustomerQuery
{
    readonly string _connectionString;

    public CustomerQuery(IAppConfiguration configuration)
    {
        _connectionString = configuration.ConnectionString;
    }

    public IEnumerable<Customer> Query(CustomerQueryParams query)
    {
        using System.Data.IDbConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        return conn.Query<dynamic>($"Exec GetCustomers")
            .Select(item => new Customer()
            {
                CustomerId = item.CustomerId,
                Name = item.Name,
                Phone = item.Phone,
                Email = item.Email,
                Company = new Company(item.CompanyName, item.CompanyAddress)
            });
    }
}
