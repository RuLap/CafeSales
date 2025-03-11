using CafeSales.Models;
using CafeSales.Models.DTO;

namespace CafeSales.Mapper.Custom;

public static class OrderProductExtensions
{
    public static OrderProductDTO ToDTO(this OrderProduct orderProduct)
    {
        return new OrderProductDTO
        {
            Product = orderProduct.Product.ToDTO(),
            Quantity = orderProduct.Quantity,
        };
    }
}