using AMochika.Core.Entities;

namespace AMochika.Core.Interfaces;

public interface IPurchaseRepository
{
    Task<Purchase> GetByIdAsync(int id);
    Task<IEnumerable<Purchase>> GetPurchaseByClientIdAsync(int clientId);
    Task AddAsync(Purchase purchase);
    Task UpdateAsync(Purchase purchase);
    Task DeleteSoftAsync(int id);
}