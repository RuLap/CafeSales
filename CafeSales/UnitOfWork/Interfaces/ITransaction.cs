namespace CafeSales.UnitOfWork.Interfaces
{
    public interface ITransaction : IDisposable
    {
        Task CommitAsync();

        Task RollbackAsync();
    }
}
