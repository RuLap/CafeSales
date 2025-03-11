using CafeSales.Models.DTO;

namespace CafeSales.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDTO>> GetOrders();
    
    Task<OrderDTO> GetOrder(Guid id);
    
    Task<OrderDTO> AddOrder(AddOrderDTO order);
    
    Task<OrderDTO> ChangeStatus(Guid id, Guid statusId);
    
    Task<OrderDTO> AddProduct(Guid orderId, Guid productId);
}