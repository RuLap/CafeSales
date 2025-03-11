using CafeSales.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CafeSales.UnitOfWork
{
    public class EfTransaction : ITransaction
    {
        private IDbContextTransaction _dbContextTransaction;

        public EfTransaction(IDbContextTransaction dbContextTransaction)
        {
            _dbContextTransaction = dbContextTransaction;
        }

        public Task CommitAsync() => _dbContextTransaction.CommitAsync();

        public Task RollbackAsync() => _dbContextTransaction.RollbackAsync();

        public void Dispose()
        {
            _dbContextTransaction?.Dispose();
            _dbContextTransaction = null!;
        }
    }
}
