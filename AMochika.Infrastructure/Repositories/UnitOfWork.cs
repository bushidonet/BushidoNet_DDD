using AMochika.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore.Storage;

namespace AMochika.Infrastructure.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveAsync();
    Task BeginTransactionAsync();
    Task RollbackAsync();
    Task CommitAsync();
}

public class UnitOfWork: IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    private IDbContextTransaction _transaction;//I could control transaction 
            
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    // Init new transaction
    public async Task BeginTransactionAsync() => 
        _transaction = await _context.Database.BeginTransactionAsync();
    
    // Confirm transaction
    public async Task CommitAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
        }
    }
    // Revert transaction
    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }
    //Save Transaction
    public Task<int> SaveAsync() => _context.SaveChangesAsync();
    public void Dispose() => _transaction?.Dispose();
   
}