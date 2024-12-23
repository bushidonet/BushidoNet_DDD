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
        CreateMap<CreateClientDTO, Client>();

        CreateMap<Client, UpdateClientDTO>();
        CreateMap<UpdateClientDTO, Client>();

        CreateMap<Client, ClientDTO>();
        CreateMap<ClientDTO, Client>();
    }
}