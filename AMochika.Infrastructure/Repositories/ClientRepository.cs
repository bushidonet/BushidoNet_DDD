using AMochika.Core.Interfaces;

using AMochika.Core.Entities;
using AMochika.Infrastructure.Configuration; // Contiene el DbContext
using Microsoft.EntityFrameworkCore;

namespace AMochika.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<int> AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            return client.Id;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<int> UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return client.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var client = await GetByIdAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
                return client.Id;
            }

            return 0;
        }
    }
}
