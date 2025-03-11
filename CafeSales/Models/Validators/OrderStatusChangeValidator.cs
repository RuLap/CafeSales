using CafeSales.Models.Validators.Interfaces;

namespace CafeSales.Models.Validators;

public class OrderStatusChangeValidator : IOrderStatusChangeValidator
{
    public void ValidateTransaction(OrderStatus current, OrderStatus newStatus)
    {
        if (current == OrderStatus.Completed && newStatus == OrderStatus.Canceled)
        {
            throw new InvalidOperationException("Попытка смены статуса заказа с завершенного на отмененный");
        }
        if (current == OrderStatus.Canceled && newStatus == OrderStatus.Completed)
        {
            throw new InvalidOperationException("Попытка смены статуса заказа с отмененного на завершенный");
        }
    }
}