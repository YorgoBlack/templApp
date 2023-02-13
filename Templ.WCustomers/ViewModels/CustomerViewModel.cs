using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Templ.WCustomers.Models;
using Templ.WCustomers.Services;
using Templ.WCustomers.Shared;

namespace Templ.WCustomers.ViewModels;

public class CustomerViewModel : BaseDomainViewModel
{
    private readonly ICustomerService _customerService;

    public string Id { get; set; }

    private string _name = string.Empty;
    public string Name {
        get => _name;
        set
        {
            _name = value;
            ValidateName();
            RaisePropertyChanged();
        }
    }

    private string _companyName = string.Empty;
    public string CompanyName { 
        get => _companyName; 
        set
        {
            _companyName = value;
            ValidateCompany();
            RaisePropertyChanged();
        }
    }

    public string CompanyAddress { get; set; }

    public string Phone { get; set; }

    private string _email = string.Empty;
    public string Email { 
        get => _email; 
        set {
            _email = value;
            ValidateEmail();
            RaisePropertyChanged();
        }
    }

    public CustomerViewModel(ICustomerService customerService) 
        : this(customerService, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
    {
    }

    public CustomerViewModel(ICustomerService customerService, string id, string name, string companyName, string companyAddress, string phone, string email)
    {
        _customerService = customerService;
        Id = id;
        Name = name;
        CompanyName = companyName;
        CompanyAddress = companyAddress;
        Phone = phone;
        Email = email;
        ValidateCompany();
    }

    public CustomerViewModel Set(Customer c)
    {
        Id = c.Id;
        Name = c.Name;
        CompanyName = c.CompanyName;
        CompanyAddress = c.CompanyAddress;
        Phone = c.Phone;
        Email = c.Email;

        return this;
    }

    public Customer AsCustomer()
    {
        return new Customer(Id,Name,CompanyName,CompanyAddress,Phone,Email); 
    }

    private async void ValidateName()
    {
        ClearErrors(nameof(Name));
        if( string.IsNullOrWhiteSpace(_name) )
        {
            AddError(nameof(Name), "Name must be not empty");
        }
        else
        {
            await Task.Run(async () =>
            {
                Guid? guid = string.IsNullOrEmpty(Id) ? null : Guid.Parse(Id);
                var res = await _customerService.ValidateCustomerNameAsync(guid, Name);
                if( !res )
                {
                    AddError(nameof(Name), "Customer with this Name already exists");
                }
                
            })
            .ConfigureAwait(false);
        }
    }

    private void ValidateCompany()
    {
        ClearErrors(nameof(CompanyName));
        if (_companyName == null)
        {
            AddError(nameof(CompanyName), "Company Name must be 10 letters at least");
        }
        else if (_companyName.Length < 10)
        {
            AddError(nameof(CompanyName), "Company Name must be 10 letters at least");
        }
        else if (_companyName.Length > 20)
        {
            AddError(nameof(CompanyName), "Company Name must less then 20 letters");
        }
    }

    private void ValidateEmail()
    {
        ClearErrors(nameof(Email));
        if (_email == null || !regex.IsMatch(_email))
        {
            AddError(nameof(Email), "Invalid Email");
        }
    }

    private static readonly Regex regex = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
}
