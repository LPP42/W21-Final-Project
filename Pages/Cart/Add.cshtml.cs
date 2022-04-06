#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shoptry.Models;

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace shoptry.Pages_Cart
{
    public class AddModel : PageModel
    {
        private readonly UserManager<ShopUser> _userManager;
        private readonly SignInManager<ShopUser> _signInManager;

        public string Username { get; set; }
        private readonly StoreDBContext _context;
        //private readonly UserManager<ShopUser> _userManager;
        public AddModel(StoreDBContext context, UserManager<ShopUser> userManager,
            SignInManager<ShopUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> OnGetAsync(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            //var user = userManager.Find(userName);
            //var claimsIdentity = (ClaimsIdentity)User;
            //var user = claimsIdentity.
            var product = await _context.Product.FirstOrDefaultAsync(m => m.ProductId == id);
            //var customer = await _context.ShopUser.Where(c => c.UserName == ).FirstOrDefaultAsync();
            //var userName = await _userManager.GetUserAsync(user);
            var cart = await _context.Cart.FirstOrDefaultAsync(c => c.Product == product && c.ShopUser == user);
            if (cart == null)
            {
                _context.Cart.Add(new Cart { Product = product, ShopUser = user, Quantity = 1 });
            }
            else
            {
                cart.Quantity = cart.Quantity + 1;
            }
            await _context.SaveChangesAsync();

            if (product == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.


    }
}
