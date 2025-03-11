namespace CafeSales.Models.DTO;

public class ProductDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}

public class AddProductDTO
{
    public string Name { get; set; }
}

public class UpdateProductDTO
{
    public string Name { get; set; }
}