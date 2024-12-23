
using AMochika.Core.Entities;

namespace AMochika.Infrastructure.Repositories
{
    public interface IClientRepository
    {
        Task<Client> GetByIdAsync(int id);
        Task<bool> ClientExistAsync(int id);
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> AddAsync(Client client);
        Task<int> UpdateAsync(Client client);
        Task<Client> DeleteAsync(Client client);
    }

}
