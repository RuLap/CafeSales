using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeSales.Models;

[Table("payment_types", Schema = "public")]
public class PaymentType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public string Name { get; private set; } = string.Empty;
    
    
    private PaymentType() { }
    
    public PaymentType(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static readonly PaymentType Card = new PaymentType(Guid.Parse("9fdbe7bf-68ca-4ef9-ac4a-aa8b78584a7f"), "Безналичный");
    public static readonly PaymentType Cash = new PaymentType(Guid.Parse("16a1fec6-a9ea-40a9-b547-cb228401ccf9"), "Наличный");
}