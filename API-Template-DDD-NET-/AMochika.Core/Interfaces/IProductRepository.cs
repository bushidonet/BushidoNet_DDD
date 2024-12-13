using AMochika.Core.Entities;

namespace AMochika.Core.Interfaces;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteSoftAsync(int id);
    Task DeleteHardAsync(int id);
}