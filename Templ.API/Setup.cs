using AutoMapper;
using FluentMediator;

namespace Templ.API;
using Application.Handlers;
using Application.Mappers;
using Application.Services;
using Domain.Customers;
using Domain.Customers.Commands;
using Infrastucture.Factories;
using Infrastucture.Repositories;
using Templ.Infrastucture;
using Templ.Infrastucture.Services;

static class Setup
{
    public static void InstallAppServices(IServiceCollection services, ConfigurationManager cm)
    {
        var conn = cm.GetSection("ConnectionStrings")["TemplDb"];
        var liteDb = cm.GetSection("LocalStorage")["LiteDb"];
        var secret = cm.GetSection("Jwt")["SecretKey"];
        var configuration = new AppConfiguration(conn!,liteDb!, secret!);

        services.AddRepository(configuration);

        services.AddSingleton<IAppConfiguration>(configuration);

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ICustomerRepository, CustomerRespository>();
        services.AddSingleton(new MapperConfiguration(c => c
                .AddProfile(new CustomerProfile()))
                .CreateMapper());
        services.AddTransient<ICustomerFactory, EntityFactory>();
        services.AddScoped<CustomerCommandHandler>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddFluentMediator(builder =>
        {
            builder.On<CreateNewCustomerCommand>().PipelineAsync().Return<Customer, CustomerCommandHandler>((handler, request) => handler.HandleNewCustomer(request));
            builder.On<UpdateCustomerCommand>().PipelineAsync().Return<Customer?, CustomerCommandHandler>((handler, request) => handler.HandleUpdateCustomer(request));
            builder.On<DeleteCustomerCommand>().PipelineAsync().Return<int, CustomerCommandHandler>((handler, request) => handler.HandleDeleteCustomer(request));
        });
    }
}
