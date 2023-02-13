namespace Templ.Domain.Customers.ValueObjects;

public class Company 
{
    public Company(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public string Name { get; set; }
    public string Address { get; set; }
}
    