using FlightBoard.Domain.Interfaces;
using FlightBoard.Infrastructure.Data;
using FlightBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FlightBoard.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));

        services.AddScoped<IFlightRepository, FlightRepository>();

        return services;
    }
}
