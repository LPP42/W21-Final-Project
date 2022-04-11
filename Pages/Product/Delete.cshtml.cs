#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
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
                return Page();
            }
            else
            {
                _logger.Log(LogLevel.Information, "**NO user is  authenticated! BAD VERY BAD!***");
                return RedirectToPage("./Index");
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
