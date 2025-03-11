using CafeSales.Models;
using CafeSales.Models.DTO;

namespace CafeSales.Mapper.Custom;

public static class ProductExtensions
{
    public static ProductDTO ToDTO(this Product product)
    {
        return new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
        };
    }

    public static Product ToProduct(this ProductDTO productDTO)
    {
        return new Product
        {
            Id = productDTO.Id,
            Name = productDTO.Name,
        };
    }

    public static Product ToProduct(this AddProductDTO productDTO)
    {
        return new Product
        {
            Name = productDTO.Name,
        };
    }

    public static Product ToProduct(this UpdateProductDTO productDTO)
    {
        return new Product
        {
            Name = productDTO.Name,
        };
    }
}