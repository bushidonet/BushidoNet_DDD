using AMochika.Core.Entities;
using AMochika.Core.Interfaces;
using AMochika.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AMochika.Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AppDbContext _context;

        public PurchaseRepository(AppDbContext context)
        {
            _context = context;
        }

        // Obtener una compra por su ID
        public async Task<Purchase> GetByIdAsync(int id)
        {
            return await _context.Purchases
                .Include(p => p.Client)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        // Obtener todas las compras de un cliente específico
        public async Task<IEnumerable<Purchase>> GetPurchaseByClientIdAsync(int clientId)
        {
            return await _context.Purchases
                .Where(p => p.ClientId == clientId && !p.IsDeleted) // Filtrando por cliente y asegurándose de no incluir eliminados
                .ToListAsync();
        }

        // Agregar una nueva compra
        public async Task AddAsync(Purchase purchase)
        {
            await _context.Purchases.AddAsync(purchase);
            await _context.SaveChangesAsync();
        }

        // Actualizar una compra existente
        public async Task UpdateAsync(Purchase purchase)
        {
            _context.Purchases.Update(purchase);
            await _context.SaveChangesAsync();
        }

        // Soft delete de una compra (marcarla como eliminada sin eliminarla físicamente)
        public async Task DeleteSoftAsync(int id)
        {
            var purchase = await _context.Purchases
                .FirstOrDefaultAsync(p => p.Id == id);

            if (purchase != null)
            {
                purchase.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}