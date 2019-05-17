using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WineryApp.Data;

namespace WineryApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly WineryAppDbContext _context;

        public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger, WineryAppDbContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Unesite svoj email.")]
            [EmailAddress(ErrorMessage = "Unesite važeći email.")]
            [Display(Name = "Email", Prompt = "Ovdje unesite svoj email.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Unesite lozinku.")]
            [StringLength(100, ErrorMessage = "{0} mora biti barem {2} znakova i maksimalno {1}.",
                MinimumLength = 6)]
            [DataType(DataType.Password, ErrorMessage = "Unesite važeću lozinku.")]
            [Display(Name = "Lozinka", Prompt = "Ovdje unesite lozinku")]
            public string Password { get; set; }

            [Display(Name = "Zapamti prijavu?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var user = _context.Zaposlenik.Where(z => z.Email == Input.Email)
                    .Include(z => z.User)
                    .First()
                    .User;
                var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    TempData["PrijavaUspješna"] = "Uspješno ste se prijavili!";
                    _logger.LogInformation("Korisnik uspješno prijavljen!");
                    return RedirectToAction("Dashboard", "Home");
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Neuspješna prijava. Pokušajte ponovno");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
