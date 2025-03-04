using CafeSales.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeSales.Data;

public class CafeDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }

    public CafeDbContext(DbContextOptions<CafeDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();
        
        modelBuilder.Entity<OrderProduct>().HasKey(op => new { op.OrderId, op.ProductId });
        
        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(o => o.Products)
            .HasForeignKey(op => op.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany()
            .HasForeignKey(op => op.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(os => os.Id);
            entity.Property(os => os.Id).ValueGeneratedNever();
        });
        
        modelBuilder.Entity<OrderStatus>().HasData(
            OrderStatus.InProgress,
            OrderStatus.Completed,
            OrderStatus.Canceled
        );

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(pt => pt.Id);
            entity.Property(pt => pt.Id).ValueGeneratedNever();
        });
        
        modelBuilder.Entity<PaymentType>().HasData(
            PaymentType.Card,
            PaymentType.Cash
        );
    }
}