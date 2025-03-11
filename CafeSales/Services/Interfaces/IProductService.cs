using CafeSales.Models;
using CafeSales.Models.DTO;

namespace CafeSales.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetProducts();
    
    Task<ProductDTO> GetProduct(Guid id);
    
    Task<ProductDTO> AddProduct(AddProductDTO product);
    
    Task<ProductDTO> UpdateProduct(Guid id, UpdateProductDTO product);
    
    Task DeleteProduct(Guid id);
}