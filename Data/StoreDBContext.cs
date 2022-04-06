#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using shoptry.Models;

public class StoreDBContext : IdentityDbContext
{
    public StoreDBContext(DbContextOptions<StoreDBContext> options)
        : base(options)
    {
    }

    public DbSet<shoptry.Models.Product> Product { get; set; }
    public DbSet<ShopUser> ShopUser { get; set; }
    public DbSet<Cart> Cart { get; set; }
}
