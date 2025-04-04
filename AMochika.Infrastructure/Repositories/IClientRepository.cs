
using AMochika.Core.Entities;

namespace AMochika.Infrastructure.Repositories
{
    public interface IClientRepository
    {
        Task<Client> GetByIdAsync(int id);
        Task<bool> ClientExistAsync(int id);
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> AddAsync(Client client);
        int Update(Client client);
        Client Delete(Client client);
    }

}
