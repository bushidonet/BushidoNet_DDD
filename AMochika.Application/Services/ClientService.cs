
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
    private readonly IUnitOfWork _unitOfWork;
   

    public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
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
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var client = _mapper.Map<AMochika.Core.Entities.Client>(clientDto);
            var result = await _clientRepository.AddAsync(client);
            await _unitOfWork.SaveAsync();

            await _unitOfWork.CommitAsync();
            return result.Id;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            return 0;
        }
        
        // var client = _mapper.Map<AMochika.Core.Entities.Client>(clientDto);
        // var result = await _clientRepository.AddAsync(client);
        // return result.Id;
    }

    //UPDATE CLIENT
    public async Task<UpdateClientDTO> UpdateAsync(UpdateClientDTO clientDto)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var clientExist = await _clientRepository.ClientExistAsync(clientDto.Id);
            if (clientExist == false) return null;

            var client = _mapper.Map<Client>(clientDto);


            _clientRepository.Update(client);
            await _unitOfWork.SaveAsync();
            
            var updatedClient = _mapper.Map<UpdateClientDTO>(client);
            await _unitOfWork.CommitAsync();
            return updatedClient;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

       
    }

    //DELETE CLIENT
    public async Task<int> DeleteAsync(int clientId)
    {
        
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var clientExist = await _clientRepository.GetByIdAsync(clientId);
            
            if (clientExist is null) return -1;
            var result = _clientRepository.Delete(clientExist);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();
            return result.Id;
            }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            return 0;
        }
    }
}