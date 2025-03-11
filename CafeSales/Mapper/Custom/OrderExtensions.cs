using CafeSales.Models;
using CafeSales.Models.DTO;

namespace CafeSales.Mapper.Custom;

public static class OrderExtensions
{
    public static OrderDTO ToDTO(this Order order)
    {
        return new OrderDTO
        {
            Id = order.Id,
            ClientName = order.ClientName,
            Status = order.Status,
            PaymentType = order.PaymentType,
            Time = order.Time,
            Products = order.Products.Select(p => p.ToDTO()).ToList()
        };
    }

    public static Order ToEntity(this AddOrderDTO dto)
    {
        return new Order
        {
            ClientName = dto.ClientName,
            PaymentTypeId = dto.PaymentType,
            Products = new List<OrderProduct>()
        };
    }
}