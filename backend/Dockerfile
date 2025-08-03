# Backend Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FlightBoard.API/FlightBoard.API.csproj", "FlightBoard.API/"]
COPY ["FlightBoard.Application/FlightBoard.Application.csproj", "FlightBoard.Application/"]
COPY ["FlightBoard.Domain/FlightBoard.Domain.csproj", "FlightBoard.Domain/"]
COPY ["FlightBoard.Infrastructure/FlightBoard.Infrastructure.csproj", "FlightBoard.Infrastructure/"]

RUN dotnet restore "FlightBoard.API/FlightBoard.API.csproj"
COPY . .
WORKDIR "/src/FlightBoard.API"
RUN dotnet build "FlightBoard.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FlightBoard.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlightBoard.API.dll"]