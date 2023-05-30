using CMS.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenIddict.Abstractions;
using Microsoft.AspNetCore.Authentication;

namespace CMS.Models
{

    [Authorize]
    public class Consent : PageModel
    {
        [BindProperty]
        public string? ReturnUrl { get; set; }

        public IActionResult OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string grant)
        {
            User.SetClaim(Consts.ConsentNaming, grant);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, User);
            return Redirect(ReturnUrl);
        }
    }


}
