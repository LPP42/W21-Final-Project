// #nullable disable
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Identity;
// using shoptry.Models;



// namespace shoptry.Pages_Product
// {
//     public class IndexModel : PageModel
//     {
//         private readonly UserManager<ShopUser> _userManager;
//         private readonly SignInManager<ShopUser> _signInManager;

//         public string Username { get; set; }
//         private readonly StoreDBContext _context;
//         private readonly ILogger<IndexModel> _logger;
//         public IndexModel(StoreDBContext context, ILogger<IndexModel> logger, UserManager<ShopUser> userManager,
//             SignInManager<ShopUser> signInManager)
//         {
//             _context = context;
//             _logger = logger;
//             _signInManager = signInManager;
//         }
//         public IList<Product> Product { get; set; }

//         public uint? Quantity { get; set; }

//         public uint? Id { get; set; }
//         [BindProperty(SupportsGet = true)]
//         public uint? FormId { get; set; }
//         [BindProperty(SupportsGet = true)]
//         public uint? FormQuantity { get; set; }
//         //public var quantity;
//         public async Task OnGetAsync()
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user != null && FormId != null && FormQuantity != null)
//             {
//                 _context.Cart.Where(c => c.Product.ProductId == FormId && c.ShopUser == user).FirstOrDefault().Quantity = FormQuantity ?? 0;
//                 await _context.SaveChangesAsync();
//             }
//             Product = await _context.Product.ToListAsync();
//             _logger.Log(LogLevel.Information, FormId.ToString());
//             _logger.Log(LogLevel.Information, FormQuantity.ToString());
//         }
//         public async Task OnPostAsync()
//         {
//             Product = await _context.Product.ToListAsync();
//             if (Id != null && Quantity != null)
//             {

//             }
//         }
//     }
// }
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
			_context = context;
			_logger = logger;
		}

		public class ProductQuantity
		{
			public uint? ProductId { get; set; }
			public uint? Quantity { get; set; }
		}



		public IList<Product> Product { get; set; }

		public async Task OnGetAsync()
		{
			Product = await _context.Product.ToListAsync();
		}

		public async void OnPostAsync(ProductQuantity productQuantity)
		{
			_logger.Log(LogLevel.Information, productQuantity.ProductId.ToString());
			_logger.Log(LogLevel.Information, productQuantity.Quantity.ToString());

			Product = await _context.Product.ToListAsync();
		}
	}
}
