using CafeSales.Mapper.Custom;
using CafeSales.Middleware.ExceptionHandling.Errors;
using CafeSales.Models;
using CafeSales.Models.DTO;
using CafeSales.Repository;
using CafeSales.Services.Interfaces;
using Serilog;

namespace CafeSales.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;

    public ProductService(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        try
        {
            var products = await _productRepository.GetAllAsync();

            var dtos = products.Select(p => p.ToDTO()).ToList();
            
            return dtos;
        }
        catch (Exception ex)
        {
            throw new InternalException(ex.Message, ex);
        }
    }

    public async Task<ProductDTO> GetProduct(Guid id)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(id);

            var dto = product.ToDTO();
            
            return dto;
        }
        catch (Exception ex)
        {
            throw new InternalException(ex.Message, ex);
        }
    }

    public async Task<ProductDTO> AddProduct(AddProductDTO dto)
    {
        try
        {
            var product = dto.ToProduct();
            product.Id = Guid.NewGuid();
            
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
            
            return product.ToDTO();
        }
        catch (Exception ex)
        {
            throw new InternalException(ex.Message, ex);
        }
    }

    public async Task<ProductDTO> UpdateProduct(Guid id, UpdateProductDTO dto)
    {
        try
        {
            var product = dto.ToProduct();
            product.Id = id;
            
            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
            
            return product.ToDTO();
        }
        catch (Exception ex)
        {
            throw new InternalException(ex.Message, ex);
        }
    }

    public async Task DeleteProduct(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new NotFoundException("Product not found", $"Product with id: {id} was not found");
        }
        
        try
        {
            await _productRepository.DeleteAsync(id);
            await _productRepository.SaveChangesAsync();

            Log.Information($"Product deleted: {product}).");
        }
        catch (Exception ex)
        {
            throw new InternalException(ex.Message, ex);
        }
    }
}