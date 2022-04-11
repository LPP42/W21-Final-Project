#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using shoptry.Models;

namespace shoptry.Pages_Product
{
    public class DeleteModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly StoreDBContext _context;

        public DeleteModel(StoreDBContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Product Product { get; set; }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var usr = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var siteUsr = _context.ShopUser.Where(u => u.Id == usr).FirstOrDefault();
                if (siteUsr != null)
                {
                    if (siteUsr.isAdmin)
                    {
                        return Page();
                    }
                }
            }
            _logger.Log(LogLevel.Information, "*** Can't access that page!!! ***");
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usr = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var siteUsr = _context.ShopUser.Where(u => u.Id == usr).FirstOrDefault();
                if (siteUsr != null)
                {
                    if (siteUsr.isAdmin)
                    {
                        Product = await _context.Product.FindAsync(id);
                        if (Product != null)
                        {
                            _context.Product.Remove(Product);
                            await _context.SaveChangesAsync();
                        }
                        return RedirectToPage("./Index");
                    }
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
