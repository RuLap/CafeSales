using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeSales.Models;

[Table("order_statuses", Schema = "public")]
public class OrderStatus
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; }

    public string Name { get; } = string.Empty;
    
    
    public OrderStatus() { }
    
    public OrderStatus(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static readonly OrderStatus InProgress = new OrderStatus(Guid.Parse("76dfd036-cb2d-4a8a-ac50-1ed8a5908ba7"), "В процессе");
    public static readonly OrderStatus Completed  = new OrderStatus(Guid.Parse("51ed485a-d259-4b1e-9418-dd63147451ee"), "Завершен");
    public static readonly OrderStatus Canceled   = new OrderStatus(Guid.Parse("1f429036-0a5e-4888-a74f-cef8275f5df8"), "Отменен");
}