using Microsoft.EntityFrameworkCore.Storage;
using ShoppingApp.Data.Context;

namespace ShoppingApp.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShoppingAppDbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(ShoppingAppDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            // allows disposing of unmanaged resources
            _context.Dispose();
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
