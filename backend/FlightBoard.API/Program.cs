using Microsoft.OpenApi.Models;
using FlightBoard.Infrastructure.DependencyInjection;
using FlightBoard.Application.DependencyInjection;
using FlightBoard.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
                           ?? new[] { "http://localhost:3000" };

        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
});
builder.Services.AddScoped<IFlightNotifierService, FlightNotifierService>();
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddHostedService<FlightStatusBackgroundService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlightBoard API", Version = "v1" });
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration.GetConnectionString("DefaultConnection")!);

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

app.MapHub<FlightBoardHub>("/flightBoardHub");

app.UseCors("AllowFrontend");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
    DbInitializer.Seed(db);
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();

}

app.UseAuthorization();
app.MapControllers();

app.Run();

