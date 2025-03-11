using System.Data;
using CafeSales.Data;
using CafeSales.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CafeSales.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dataContext;
        private readonly Dictionary<Type, object> _repositories = new();
        private ITransaction _currentTransaction;
        private bool _disposed;

        public UnitOfWork(DbContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<ITransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        
        {
            if (_currentTransaction != null)
                throw new InvalidOperationException("Transaction already in progress");

            var dbTransaction = await _dataContext.Database.BeginTransactionAsync(isolationLevel);
            _currentTransaction = new EfTransaction(dbTransaction);
            return _currentTransaction;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dataContext.SaveChangesAsync(cancellationToken);
        }
        
        public void Dispose()
        {
            _dataContext.Dispose();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _currentTransaction?.Dispose();
                    _dataContext.Dispose();
                    foreach (var repo in _repositories.Values.OfType<IDisposable>())
                    {
                        repo.Dispose();
                    }
                    _repositories.Clear();
                }
                _disposed = true;
            }
        }
    }
}
