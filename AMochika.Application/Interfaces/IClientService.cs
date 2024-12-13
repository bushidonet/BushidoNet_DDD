using AMochika.Core.Entities;

namespace AMochika.Application.Interfaces;

public interface IClientService
{
    Task<Client> GetClientByIdAsync(int id);
    Task<int> AddClientAsync(Client client);
}