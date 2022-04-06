// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace shoptry.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ShopUser> _userManager;
        private readonly SignInManager<ShopUser> _signInManager;

        public IndexModel(
            UserManager<ShopUser> userManager,
            SignInManager<ShopUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }
        //public string BuyerId { get; set; }
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone Number")]
            public string Phone { get; set; }

            [Required]
            [Display(Name = "Full Name")]
            public string Name { get; set; }

            [Display(Name = "Street Number")]
            public int StreetNumber { get; set; }

            [Display(Name = "Street Name")]
            public string StreetName { get; set; }

            [Display(Name = "Postal Code")]
            [RegularExpression(@"^[A-Za-z][0-9][A-Za-z][ ]*[0-9][A-Za-z][0-9]$")]
            public string PostalCode { get; set; }

            public string City { get; set; }

            [Required]
            public string Province { get; set; }

        }

        private async Task LoadAsync(ShopUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //var userId = await _userManager.GetUserIdAsync(user);
            Username = userName;
            //BuyerId = userId;

            Input = new InputModel
            {
                Phone = user.Phone,
                Name = user.Name,
                StreetNumber = user.StreetNumber,
                StreetName = user.StreetName,
                PostalCode = user.PostalCode,
                City = user.City,
                Province = user.Province
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.Phone = Input.Phone != user.Phone ? Input.Phone : user.Phone;
            user.Name = Input.Name != user.Name ? Input.Name : user.Name;
            user.StreetNumber = Input.StreetNumber != user.StreetNumber ? Input.StreetNumber : user.StreetNumber;
            user.StreetName = Input.StreetName != user.StreetName ? Input.StreetName : user.StreetName;
            user.City = Input.City != user.City ? Input.City : user.City;
            user.PostalCode = Input.PostalCode != user.PostalCode ? Input.PostalCode : user.PostalCode;
            user.Province = Input.Province != user.Province ? Input.Province : user.Province;

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
