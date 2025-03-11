using CafeSales.Models.DTO;
using CafeSales.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CafeSales.Controllers;

[Route("orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _orderService.GetOrders();

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(string id)
    {
        if (Guid.TryParse(id, out var guid))
        {
            var result = await _orderService.GetOrder(guid);

            return Ok(result);
        }

        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody] AddOrderDTO order)
    {
        var result = await _orderService.AddOrder(order);

        return Created(result.Id.ToString(), result);
    }
    
    [HttpPost("{id}/status")]
    public async Task<IActionResult> ChangeStatus(string id, [FromBody] ChangeOrderStatusDTO orderStatusDto)
    {
        if (Guid.TryParse(id, out var guid))
        {
            var result = await _orderService.ChangeStatus(guid, orderStatusDto.StatusId);

            return Ok(result);
        }

        return BadRequest();
    }

    [HttpPost("{id}/products")]
    public async Task<IActionResult> AddProduct(string id, [FromBody] AddOrderProductDTO addProductDto)
    {
        if (Guid.TryParse(id, out var guid))
        {
            var result = await _orderService.AddProduct(guid, addProductDto.ProductId);

            return Ok(result);
        }

        return BadRequest();
    }
}