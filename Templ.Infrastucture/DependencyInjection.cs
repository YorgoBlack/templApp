using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Templ.Infrastucture;
using Domain;
using Domain.Customers;
using Infrastucture.Repositories;
using Application.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddRepository(this IServiceCollection services, IAppConfiguration configuration)
    {
        services.AddTransient<ICustomerRepository, CustomerRespository>();
        services.AddTransient<ICustomerQuery, CustomerQuery>();

        services.AddDbContext<EfDbContext>(opt => opt
            .UseSqlServer(configuration.ConnectionString));

        return services;
    }
}

