namespace Templ.Domain.Customers;
using Domain.Customers.ValueObjects;

public class Customer : IAggregateRoot
{
    public Customer() { }

    public Customer(Guid customerId, string name, Company company, string phone, string email) 
    {
        CustomerId = customerId;
        Name = name;
        Company = company;
        Email = email;
        Phone = phone ?? string.Empty;
    }
    public Guid CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public Company Company { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;
}

