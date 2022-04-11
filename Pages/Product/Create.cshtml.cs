#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
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
                return Page();
            }
            else
            {
                _logger.Log(LogLevel.Information, "**NO user is  authenticated! BAD VERY BAD!***");
                return RedirectToPage("./Index");
            }
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
