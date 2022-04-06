using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace shoptry.Models;

public class ShopUser : IdentityUser
{
    // public int UserId { get; set; }
    [PersonalData]
    public string? Name { get; set; }
    // public string? Email { get; set; }
    [PersonalData]
    public int StreetNumber { get; set; } = 1;
    [PersonalData]
    public string? StreetName { get; set; }
    [PersonalData]
    [RegularExpression(@"^[A-Za-z][0-9][A-Za-z][ ]*[0-9][A-Za-z][0-9]$")]
    public string? PostalCode { get; set; }
    [PersonalData]
    public string? City { get; set; }
    [PersonalData]
    public string? Province { get; set; }
    [PersonalData]
    public string? Phone { get; set; }

}