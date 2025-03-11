using System.Data;

namespace CafeSales.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<ITransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
