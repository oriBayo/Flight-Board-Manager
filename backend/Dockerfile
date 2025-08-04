# Use the official ASP.NET Core runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file and csproj files for restore
COPY ["FlightBoardManager.sln", "./"]
COPY ["backend/FlightBoard.API/FlightBoard.API.csproj", "backend/FlightBoard.API/"]
COPY ["backend/FlightBoard.Application/FlightBoard.Application.csproj", "backend/FlightBoard.Application/"]
COPY ["backend/FlightBoard.Domain/FlightBoard.Domain.csproj", "backend/FlightBoard.Domain/"]
COPY ["backend/FlightBoard.Infrastructure/FlightBoard.Infrastructure.csproj", "backend/FlightBoard.Infrastructure/"]
COPY ["backend/FlightBoard.Tests/FlightBoard.Tests.csproj", "backend/FlightBoard.Tests/"]

RUN dotnet restore "backend/FlightBoard.API/FlightBoard.API.csproj"

# Copy the rest of the source code
COPY . .

# Build and publish the application
WORKDIR "/src/backend/FlightBoard.API"
RUN dotnet publish "FlightBoard.API.csproj" -c Release -o /app/publish

# Final stage - runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Create directory for SQLite database
RUN mkdir -p /app/data

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "FlightBoard.API.dll"]