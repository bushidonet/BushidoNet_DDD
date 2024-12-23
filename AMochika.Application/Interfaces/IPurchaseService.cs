
using AMochika.Core.Entities;
namespace AMochika.Application.Interfaces
{

    public interface IPurchaseService
    {
        Task<Purchase> GetByIdAsync(int id);
        Task<IEnumerable<Purchase>> GetPurchaseByClientIdAsync(int clientId);
        Task AddAsync(Purchase purchase);
        Task UpdateAsync(Purchase purchase);
        Task DeleteSoftAsync(int id);
    }
}