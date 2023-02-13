
using System;
using System.Collections;
using System.ComponentModel;
using System.DirectoryServices;

namespace Templ.WCustomers.Models;

public class Customer
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string CompanyName { get; set; }
    
    public string CompanyAddress { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public Customer(string id, string name, string companyName, string companyAddress, string phone, string email) 
    { 
        Id = id;
        Name = name;
        CompanyName = companyName;
        CompanyAddress = companyAddress;
        Phone = phone;
        Email = email;
    }

    public static Customer Create(Customer customer)
    {
        return new Customer(
            customer.Id,
            customer.Name,
            customer.CompanyName,
            customer.CompanyAddress,
            customer.Phone,
            customer.Email);
    }
}
