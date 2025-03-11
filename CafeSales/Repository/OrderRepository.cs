using CafeSales.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeSales.Repository;

public class OrderRepository : IRepository<Order>
{
    private readonly DbContext _context;
    private readonly DbSet<Order> _dbSet;

    public OrderRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<Order>();
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _dbSet
            .Include(o => o.Status)
            .Include(o => o.PaymentType)
            .Include(o => o.Products)
            .ThenInclude(op => op.Product)
            .ToListAsync();
    }

    public async Task<Order> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(o => o.Status)
            .Include(o => o.PaymentType)
            .Include(o => o.Products)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task AddAsync(Order entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(Order entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        _dbSet.Remove(entity);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}