using CafeSales.Models;
using CafeSales.Models.DTO;
using CafeSales.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CafeSales.Controllers;

[Route("products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProducts();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(string id)
    {
        if (Guid.TryParse(id, out var guid))
        {
            var result = await _productService.GetProduct(guid);

            return Ok(result);
        }

        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(AddProductDTO product)
    {
        var result = await _productService.AddProduct(product);

        return Created(result.Id.ToString(), result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(string id, UpdateProductDTO product)
    {
        if (Guid.TryParse(id, out var guid))
        {
            var result = await _productService.UpdateProduct(guid, product);

            return Ok(result);
        }

        return BadRequest();
    }
}