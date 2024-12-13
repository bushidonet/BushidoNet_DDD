using AMochika.Core.Entities;

namespace AMochika.Core.Interfaces;

public interface IClientRepository
{
    Task<Client> GetByIdAsync(int id);
    Task<IEnumerable<Client>> GetAllAsync();
    Task<int> AddAsync(Client client);
    Task<int> UpdateAsync(Client client);
    Task<int> DeleteAsync(int id);
}