namespace Templ.Domain.Customers.Commands;

public class CreateNewCustomerCommand
{
    public string Name { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string CompanyAddress { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;
}
