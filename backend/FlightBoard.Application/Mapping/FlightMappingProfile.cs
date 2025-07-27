
using AutoMapper;
using FlightBoard.Application.DTOs;
using FlightBoard.Domain.Entities;

namespace FlightBoard.Application.Mappers;

public class FlightMappingProfile : Profile
{
    public FlightMappingProfile()
    {
        CreateMap<Flight, FlightDto>();
        CreateMap<CreateFlightDto, Flight>();
    }
}
