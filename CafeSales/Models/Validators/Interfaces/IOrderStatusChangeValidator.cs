namespace CafeSales.Models.Validators.Interfaces;

public interface IOrderStatusChangeValidator
{
    void ValidateTransaction(OrderStatus current, OrderStatus newStatus);
}