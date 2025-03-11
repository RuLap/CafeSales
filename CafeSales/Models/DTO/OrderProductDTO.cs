namespace CafeSales.Models.DTO;

public class OrderProductDTO
{
    public ProductDTO Product { get; set; }

    public int Quantity { get; set; }
}

public class AddOrderProductDTO
{
    public Guid ProductId { get; set; }
}