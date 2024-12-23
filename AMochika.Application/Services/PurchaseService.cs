using AMochika.Application.Interfaces;
using AMochika.Core.Entities;
using AMochika.Core.Interfaces;
using AMochika.Infrastructure.Repositories;

namespace AMochika.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<Purchase> GetByIdAsync(int id)
        {
            return await _purchaseRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Purchase>> GetPurchaseByClientIdAsync(int clientId)
        {
            return await _purchaseRepository.GetPurchaseByClientIdAsync(clientId);
        }

        public async Task AddAsync(Purchase purchase)
        {
            await _purchaseRepository.AddAsync(purchase);
        }

        public async Task UpdateAsync(Purchase purchase)
        {
            await _purchaseRepository.UpdateAsync(purchase);
        }

        public async Task DeleteSoftAsync(int id)
        {
            await _purchaseRepository.DeleteSoftAsync(id);
        }
    }
}