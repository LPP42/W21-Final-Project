using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shoptry.Models;

public class Image
{
    public uint ImageId { get; set; }

    public virtual Product? Product { get; set; }

    public string? File { get; set; }
    public string? Description { get; set; }


}