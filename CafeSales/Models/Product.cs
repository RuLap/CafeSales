using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeSales.Models;

[Table("products", Schema = "public")]
public class Product
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
}