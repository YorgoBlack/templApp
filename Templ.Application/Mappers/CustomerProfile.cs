using AutoMapper;
using Templ.Application.Dtos;
using Templ.Domain.Customers;
using Templ.Domain.Customers.Commands;
using Templ.Domain.Customers.ValueObjects;

namespace Templ.Application.Mappers;

public class CustomerProfile : Profile
{
    public CustomerProfile() 
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.CustomerId))
            .ForMember(d => d.CompanyName, o => o.MapFrom(s => s.Company.Name))
            .ForMember(d => d.CompanyAddress, o => o.MapFrom(s => s.Company.Address));

        CreateMap<CustomerDto, Customer>()
            .ForMember(d => d.CustomerId, o => o.MapFrom(s => Guid.Parse(s.Id)))
            .ForMember(d => d.Company, o => o.MapFrom(s => new Company(s.CompanyName, s.CompanyAddress)));

        CreateMap<CustomerDto, CreateNewCustomerCommand>();

        CreateMap<CustomerDto, UpdateCustomerCommand>()
            .ForMember(d => d.Id, o => o.MapFrom(s => Guid.Parse(s.Id)));

        CreateMap<UpdateCustomerCommand, Customer>()
            .ForMember(d => d.CustomerId, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Company, o => o.MapFrom(s => new Company(s.CompanyName, s.CompanyAddress)));


        CreateMap<Guid, DeleteCustomerCommand>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s));
    }
}
