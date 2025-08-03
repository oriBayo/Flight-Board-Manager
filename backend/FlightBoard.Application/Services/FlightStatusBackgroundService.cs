using AutoMapper;
using FlightBoard.Application.DTOs;
using FlightBoard.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class FlightStatusBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public FlightStatusBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IFlightRepository>();
            var notifier = scope.ServiceProvider.GetRequiredService<IFlightNotifierService>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

            var flights = await repository.GetAllFlightsAsync();

            foreach (var flight in flights)
            {
                var previousStatus = flight.Status;
                flight.CalculateFlightStatus(DateTime.Now);

                if (previousStatus != flight.Status)
                {
                    await repository.UpdateFlightAsync(flight);
                    var dto = mapper.Map<FlightDto>(flight);
                    await notifier.NotifyFlightStatusUpdatedAsync(dto);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
        }
    }
}