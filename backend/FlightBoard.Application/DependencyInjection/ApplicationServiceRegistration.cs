using System.Reflection;
using FlightBoard.Application.Interfaces;
using FlightBoard.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FlightBoard.Application.DTOs;
using FlightBoard.Application.Validators;

namespace FlightBoard.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IFlightService, FlightService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IValidator<CreateFlightDto>, CreateFlightValidator>();
        return services;
    }
}
