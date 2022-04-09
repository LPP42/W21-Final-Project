
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
    public class IndexModel : PageModel
    {
        private readonly StoreDBContext _context;
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(StoreDBContext context, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _context = context;
        }
        public IList<Product> Product { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SeachName { get; set; }

        [BindProperty(SupportsGet = true)]
        public ProductCategory? SeachCategory { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool filterOn { get; set; }
        public IList<Product> Products { get; set; }

        public class ProductQuantity
        {
            public uint? ProductId { get; set; }
            public uint? Quantity { get; set; }
        }
        public async Task OnGetAsync()
        {
            var products = from p in _context.Product select p;

            if (filterOn)
            {
                // filter by name
                if (!string.IsNullOrEmpty(SeachName))
                {
                    products = products.Where(g => g.Name.Contains(SeachName));
                }
                // filter by category
                if (SeachCategory != null && SeachCategory != ProductCategory.Any)
                {
                    products = products.Where(g => g.Category == SeachCategory);
                }
            }
            Product = await products.ToListAsync();
        }
        public async void OnPostAsync(ProductQuantity productQuantity)
        {
            _logger.Log(LogLevel.Information, productQuantity.ProductId.ToString());
            _logger.Log(LogLevel.Information, productQuantity.Quantity.ToString());

            Product = await _context.Product.ToListAsync();
        }
    }
}
