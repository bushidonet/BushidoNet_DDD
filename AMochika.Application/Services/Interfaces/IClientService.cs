using AMochika.Application.DTOs;
using AMochika.Core.Entities;

namespace AMochika.Application.Interfaces;

public interface IClientService
{
    Task<ClientDTO> GetClientByIdAsync(int id);
    Task<int> AddClientAsync(CreateClientDTO clientDto);
}