using AMochika.Application.Interfaces;
using AMochika.Core.Entities;
using AMochika.Core.Interfaces;

namespace AMochika.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    //GET by ID
    public async Task<Client> GetClientByIdAsync(int id)
    {
        return await _clientRepository.GetByIdAsync(id);
    }
    //GET ALL CLIENT 
    public async Task<IEnumerable<Client>> GetAllClientAsync()
    {
        return await _clientRepository.GetAllAsync();
    }
    //ADD CLIENT 
    public async Task<int> AddClientAsync(Client client)
    {
       var result = await _clientRepository.AddAsync(client);
       return result;
    }
    //UPDATE CLIENT
    public async Task<int> UpdateAsync(Client client)
    {
        await _clientRepository.UpdateAsync(client);
        return client.Id;
    }
    //DELETE CLIENT
    public async Task<int> DeleteAsync(int id)
    {
        await _clientRepository.DeleteAsync(id);
        return id;
    }
    
}