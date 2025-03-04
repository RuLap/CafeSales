using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeSales.Models;

[Table("order_products", Schema = "public")]
public class OrderProduct
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    
    public int Quantity { get; set; }
}