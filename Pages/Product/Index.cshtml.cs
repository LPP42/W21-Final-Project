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
        public string SearchName { get; set; }

        [BindProperty(SupportsGet = true)]
        public ProductCategory? SearchCategory { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool filterOn { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool filterPriceOn { get; set; }

        [BindProperty(SupportsGet = true)]
        public Decimal? SearchAge { get; set; } = null;

        [BindProperty(SupportsGet = true)]
        public Decimal SearchPriceMax { get; set; }

        [BindProperty(SupportsGet = true)]
        public Decimal SearchPriceMin { get; set; }
        public IList<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            var products = from p in _context.Product select p;

            if (filterOn)
            {
                // filter by name
                if (!string.IsNullOrEmpty(SearchName))
                {
                    products = products.Where(p => p.Name.Contains(SearchName));
                }
                // filter by category
                if (SearchCategory != null && SearchCategory != ProductCategory.Any)
                {
                    products = products.Where(p => p.Category ==  SearchCategory);
                }

                 // filter by age
                if (SearchAge != null)
                {
                    products = products.Where(p => p.RecAgeMax >= SearchAge && p.RecAgeMin <= SearchAge);
                }

                 // filter by price
                if (filterPriceOn)
                {
                    products = products.Where(p => p.Price >= SearchPriceMin && p.Price <= SearchPriceMax);
                }
            }
            Product = await products.ToListAsync();
        }
    }
}
