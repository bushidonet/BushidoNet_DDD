using AMochika.Application.DTOs;
using AMochika.Core.Entities;

namespace AMochika.Application.Interfaces;

public interface IClientService
{
    Task<ClientDTO> GetClientByIdAsync(int id);
    Task<int> AddClientAsync(CreateClientDTO clientDto);
    Task<IEnumerable<Client>> GetAllClientAsync();
    Task<UpdateClientDTO> UpdateAsync(UpdateClientDTO clientDto);
    Task<int> DeleteAsync(int idClient);
}