namespace CafeSales.Models.DTO;

public class OrderDTO
{
    public Guid Id { get; set; }

    public string ClientName { get; set; }

    public DateTime Time { get; set; }

    public OrderStatus Status { get; set; }

    public PaymentType PaymentType { get; set; }

    public List<OrderProductDTO> Products { get; set; }
}

public class AddOrderDTO
{
    public string ClientName { get; set; }

    public Guid PaymentType { get; set; }

    public List<Guid> Products { get; set; }
}

public class ChangeOrderStatusDTO
{
    public Guid StatusId { get; set; }
}