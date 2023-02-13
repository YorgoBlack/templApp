namespace Templ.Domain.Customers.Commands;

public class UpdateCustomerCommand : CreateNewCustomerCommand
{
    public Guid Id { get; set; }
}
