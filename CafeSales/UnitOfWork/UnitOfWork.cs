using CafeSales.Data;
using CafeSales.UnitOfWork.Interfaces;

namespace CafeSales.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CafeDbContext _dataContext;

        public UnitOfWork(CafeDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
