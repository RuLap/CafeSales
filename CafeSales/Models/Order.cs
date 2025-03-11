using CafeSales.Models.Validators.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeSales.Models;

[Table("orders", Schema = "public")]
public class Order
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(30)]
    public string ClientName { get; set; } = string.Empty;
    
    public DateTime Time { get; set; }
    
    public Guid StatusId { get; private set; }
    public virtual OrderStatus Status { get; private set; }
    
    public Guid PaymentTypeId { get; set; }
    public virtual PaymentType PaymentType { get; set; }

    public virtual List<OrderProduct> Products { get; set; } = new();
    
    public void ChangeStatus(OrderStatus newStatus, IOrderStatusChangeValidator validator)
    {
        validator.ValidateTransaction(Status, newStatus);
        Status = newStatus;
        StatusId = newStatus.Id;
    }
}