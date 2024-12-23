using AMochika.Core.Interfaces;

using AMochika.Core.Entities;
using AMochika.Infrastructure.Configuration; // Contiene el DbContext
using Microsoft.EntityFrameworkCore;

namespace AMochika.Infrastructure.Repositories
{
    public class ClientRepository(AppDbContext context) : IClientRepository
    {
        public async Task<Client> GetByIdAsync(int id)
        {
            var result = await context.Clients.FindAsync(id);
            if (result == null) return null;
            return result;
        }

        public async Task<bool> ClientExistAsync(int id)
        {
            return await context.Clients.AnyAsync(x => x.Id == id);

        }

        public async Task<Client> AddAsync(Client client)
        {
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
            return client;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await context.Clients.ToListAsync();
        }

        public async Task<int> UpdateAsync(Client client)
        {
            context.Clients.Update(client);
            await context.SaveChangesAsync();
            return client.Id;
        }



        public async Task<Client> DeleteAsync(Client client)
        {
            context.Clients.Remove(client);
            await context.SaveChangesAsync();
            return client;
        }
    }
}