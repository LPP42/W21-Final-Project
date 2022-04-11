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
    public class DetailsModel : PageModel
    {
        private readonly StoreDBContext _context;
        public IList<Image> Images { get; set; }
        public DetailsModel(StoreDBContext context)
        {
            _context = context;
        }
        public IList<Product> SimilarProducts { get; set; }
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var similarproducts = from p in _context.Product select p;
            Product = await _context.Product.FirstOrDefaultAsync(m => m.ProductId == id);

            if (Product == null)
            {
                return NotFound();
            }

            similarproducts = similarproducts.Where(p => p.Category == Product.Category && p != Product && p.Price <= (Product.Price + 10) && p.Price >= Product.Price - 10);

            // var products = from p in _context.Product select p;
            // products = products.Where(g => g.Category == SeachCategory);
            // Product = await products.ToListAsync();

            var images = from g in _context.Image select g;

            images = images.Where(g => g.Product == Product);

            Images = await images.ToListAsync();
            SimilarProducts = await similarproducts.ToListAsync();
            return Page();
        }
        //     public ActionResult GetImage(int id)
        // {
        //     var firstOrDefault = _context.Image.Where(c => c.Product == Product).FirstOrDefault();
        //     if (firstOrDefault != null)
        //     {
        //         byte[] image = firstOrDefault.File;
        //         return File(image, "image/jpg");
        //     }
        //     else
        //     {
        //         return null;
        //     }
        // } 
    }
}
