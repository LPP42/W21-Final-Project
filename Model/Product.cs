using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace shoptry.Models;

public enum Category { Board, Computer, Lego, Plastic, Doll }
public class Product
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    [Range(0, 9999)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(6,2)")]
    public decimal Price { get; set; } = 1000.00M;
    public uint Stock { get; set; } = 0;
    public Category cat { get; set; }

}