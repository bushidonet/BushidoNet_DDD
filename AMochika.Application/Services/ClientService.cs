
using AMochika.Application.DTOs;
using AMochika.Application.Interfaces;
using AMochika.Core.Entities;
using AMochika.Infrastructure.Repositories;
using AutoMapper;

namespace AMochika.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private IClientService _clientImplementation;

    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    //GET by ID
    public async Task<ClientDTO> GetClientByIdAsync(int id)
    {

        var result = await _clientRepository.GetByIdAsync(id);
        var resultDto = _mapper.Map<ClientDTO>(result);
        return resultDto;
    }


    //GET ALL CLIENT 
    public async Task<IEnumerable<Client>> GetAllClientAsync()
    {
        return await _clientRepository.GetAllAsync();
    }

    //ADD CLIENT 
    public async Task<int> AddClientAsync(CreateClientDTO clientDto)
    {
        var client = _mapper.Map<AMochika.Core.Entities.Client>(clientDto);
        var result = await _clientRepository.AddAsync(client);
        return result.Id;
    }

    //UPDATE CLIENT
    public async Task<UpdateClientDTO> UpdateAsync(UpdateClientDTO clientDto)
    {

        var clientExist = await _clientRepository.ClientExistAsync(clientDto.Id);
        if (clientExist == false) return null;

        var client = _mapper.Map<Client>(clientDto);


        await _clientRepository.UpdateAsync(client);
        var updatedClient = _mapper.Map<UpdateClientDTO>(client);
        return updatedClient;
    }

    //DELETE CLIENT
    public async Task<int> DeleteAsync(int clientId)
    {
        var clientExist = await _clientRepository.GetByIdAsync(clientId);
        if (clientExist is null) return -1;
        var result = await _clientRepository.DeleteAsync(clientExist);
        return result.Id;
    }
}