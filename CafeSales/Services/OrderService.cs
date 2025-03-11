using CafeSales.Data;
using CafeSales.Mapper.Custom;
using CafeSales.Middleware.ExceptionHandling.Errors;
using CafeSales.Models;
using CafeSales.Models.DTO;
using CafeSales.Models.Validators.Interfaces;
using CafeSales.Repository;
using CafeSales.Services.Interfaces;
using CafeSales.UnitOfWork;
using CafeSales.UnitOfWork.Interfaces;
using Serilog;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<OrderProduct> _orderProductRepository;
    private readonly IRepository<OrderStatus> _orderStatusRepository;
    private readonly IRepository<PaymentType> _paymentTypeRepository;
    private readonly IOrderStatusChangeValidator _orderStatusChangeValidator;

    public OrderService(
        IUnitOfWork unitOfWork,
        IRepository<Order> orderRepository, 
        IRepository<Product> productRepository, 
        IRepository<OrderProduct> orderProductRepository, 
        IRepository<OrderStatus> orderStatusRepository,
        IRepository<PaymentType> paymentTypeRepository,
        IOrderStatusChangeValidator orderStatusChangeValidator)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _orderProductRepository = orderProductRepository;
        _orderStatusRepository = orderStatusRepository;
        _paymentTypeRepository = paymentTypeRepository;
        _orderStatusChangeValidator = orderStatusChangeValidator;
    }

    public async Task<IEnumerable<OrderDTO>> GetOrders()
    {
        try
        {
            var orders = await _orderRepository.GetAllAsync();
            
            var dtos = orders.Select(p => p.ToDTO()).ToList();
            
            return dtos;
        }
        catch (Exception ex)
        {
            throw new InternalException(ex.Message, ex);
        }
    }

    public async Task<OrderDTO> GetOrder(Guid id)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(id);
            Log.Information($"order: {order.PaymentType}");
            Log.Information($"order: {order.PaymentType.Name}");
            var dto = order.ToDTO();
            
            return dto;
        }
        catch (Exception ex)
        {
            throw new InternalException(ex.Message, ex);
        }
    }

    public async Task<OrderDTO> AddOrder(AddOrderDTO dto)
    {
        var transaction = await _unitOfWork.BeginTransactionAsync();
        
        try
        {
            var order = dto.ToEntity();
            order.Id = Guid.NewGuid();
            order.Time = DateTime.UtcNow;
            
            var paymentType = await _paymentTypeRepository.GetByIdAsync(dto.PaymentType);
            if (paymentType == null)
            {
                throw new NotFoundException("Тип оплаты не найден", $"Тип оплаты не найден (id: {dto.PaymentType})");
            }
            order.PaymentType = paymentType;
            
            var inProgressStatus = await _orderStatusRepository.GetByIdAsync(OrderStatus.InProgress.Id);
            if (inProgressStatus == null)
            {
                throw new NotFoundException("Статус не найден", $"Статус не найден (id: {OrderStatus.InProgress.Id})");
            }
            order.ChangeStatus(inProgressStatus, _orderStatusChangeValidator);
            
            var productIds = dto.Products.ToList();
            var products = (await _productRepository.GetAllAsync()).Where(p => productIds.Contains(p.Id)).ToList();
            var productDict = products.ToDictionary(p => p.Id);
            
            var missingIds = productIds.Except(products.Select(p => p.Id)).ToList();
            if (missingIds.Any())
            {
                throw new NotFoundException("Товары не найдены", "Товары не найдены");
            }

            foreach (var product in productDict)
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = product.Key,
                    Quantity = 1
                };
                await _orderProductRepository.AddAsync(orderProduct);
            }
            
            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            
            await transaction.CommitAsync();

            var resultDto = order.ToDTO();
            
            return resultDto;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new InternalException(ex.Message, ex);
        }
    }

    public async Task<OrderDTO> ChangeStatus(Guid id, Guid statusId)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new NotFoundException("Заказ не найден", $"Заказ не найден (id: {id})");
            }

            var status = await _orderStatusRepository.GetByIdAsync(statusId);
            if (status == null)
            {
                throw new NotFoundException("Статус не найден", $"Статус не найден (id: {order.StatusId})");
            }
            
            order.ChangeStatus(status, _orderStatusChangeValidator);
            _orderRepository.Update(order);
            
            await _orderRepository.SaveChangesAsync();
            
            var dto = order.ToDTO();
            
            return dto;
        }
        catch (Exception ex)
        {
            throw new InternalException(ex.Message, ex);
        }
    }

    public async Task<OrderDTO> AddProduct(Guid orderId, Guid productId)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new NotFoundException("Заказ не найден", $"Заказ не найден (id: {orderId})");
            }

            if (order.Status == OrderStatus.Completed || order.Status == OrderStatus.Canceled)
            {
                throw new NotFoundException("Попытка добавить товар в заказ, который не в работе", $"Попытка добавить товар в заказ (id: {order.Id}) со статусом: {order.Status}");
            }
            
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("Товар не найден", $"Товар не найден (id: {productId})");
            }

            var orderProduct = order.Products.FirstOrDefault(p => p.ProductId == productId);
            if (orderProduct == null)
            {
                orderProduct = new OrderProduct
                {
                    ProductId = productId,
                    OrderId = orderId,
                    Quantity = 1,
                };
                
                await _orderProductRepository.AddAsync(orderProduct);
                await _orderProductRepository.SaveChangesAsync();
                
                order.Products.Add(orderProduct);
            }
            else
            {
                orderProduct.Quantity++;
                await _orderProductRepository.SaveChangesAsync();
            }
            
            var dto = order.ToDTO();
            
            return dto;
        }
        catch (Exception ex)
        {
            throw new InternalException(ex.Message, ex);
        }
    }
}