using CafeSales.Models.Validators.Interfaces;

namespace CafeSales.Models.Validators;

public class OrderStatusChangeValifdator : IOrderStatusChangeValidator
{
    public void ValidateTransaction(OrderStatus current, OrderStatus newStatus)
    {
        if (current == OrderStatus.Completed && newStatus == OrderStatus.Canceled)
        {
            throw new InvalidOperationException("Попытка отмены выполненного заказа");
        }
        if (current == OrderStatus.Canceled && newStatus == OrderStatus.Completed)
        {
            throw new InvalidOperationException("Попытка завершить отмененный заказ");
        }
    }
}