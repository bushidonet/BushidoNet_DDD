using AMochika.Application.DTOs;
using AMochika.Core.Entities;
using AutoMapper;

namespace AMochika.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeo de Client a CreateClientDTO
        CreateMap<Client, CreateClientDTO>();
    }
}