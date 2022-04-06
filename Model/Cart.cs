using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shoptry.Models;

public class Cart
{
    public uint CartId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ShopUser? ShopUser { get; set; }
    public uint Quantity { get; set; } = 0;


}