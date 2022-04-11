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
        private readonly StoreDBContext _context;
        public IList<Image> Images { get; set; }
        public IList<Cart> Carts { get; set; }
        public DeleteModel(StoreDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(m => m.ProductId == id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
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
                var images = from g in _context.Image select g;

                images = images.Where(g => g.Product == Product);
                Images = await images.ToListAsync();
                foreach (var item in Images)
                {
                    _context.Image.Remove(item);
                }
                await _context.SaveChangesAsync();
                var carts = from c in _context.Cart select c;

                carts = carts.Where(c => c.Product == Product);
                Carts = await carts.ToListAsync();
                foreach (var citem in Carts)
                {
                    _context.Cart.Remove(citem);
                }
                await _context.SaveChangesAsync();
                _context.Product.Remove(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
