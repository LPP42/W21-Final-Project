#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using shoptry.Models;

namespace shoptry.Pages_Product
{
    public class CreateModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly StoreDBContext _context;
        public CreateModel(StoreDBContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

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

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            if (User.Identity.IsAuthenticated)
            {
                var usr = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var siteUsr = _context.ShopUser.Where(u => u.Id == usr).FirstOrDefault();
                if (siteUsr != null)
                {
                    if (siteUsr.isAdmin)
                    {
                        _context.Product.Add(Product);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("./Index");
                    }
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
