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
    public class ViewModel : PageModel
    {
        private readonly UserManager<ShopUser> _userManager;
        private readonly SignInManager<ShopUser> _signInManager;

        public string Username { get; set; }
        public IList<Cart> Cart { get; set; }
        private readonly StoreDBContext _context;
        //private readonly UserManager<ShopUser> _userManager;
        public ViewModel(StoreDBContext context, UserManager<ShopUser> userManager,
            SignInManager<ShopUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> OnGetAsync()
        {

            var user = await _userManager.GetUserAsync(User);
            var cusProducts = from m in _context.Cart
                              select m;
            cusProducts = cusProducts.Where(s => s.ShopUser == user);
            Cart = await cusProducts.ToListAsync();

            return Page();
        }

    }
}
