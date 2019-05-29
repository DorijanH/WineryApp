using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WineryApp.Data;
using WineryApp.Data.Entiteti;

namespace WineryApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly WineryAppDbContext _context;
        private readonly IRepository _repository;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            WineryAppDbContext context,
            IRepository repository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _repository = repository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = "Unesite svoje ime.")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"^[A-ZŠĐČĆŽ][a-zšđčćž]+$", ErrorMessage = "Unesite važeće ime. " +
                "Ime mora početi velikim slovom i smije sadržavati samo abecedne znakove.")]
            [Display(Name = "Ime", Prompt = "Ovdje unesite svoje ime")]
            public string Ime { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Unesite svoje prezime.")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"^[A-ZŠĐČĆŽ][a-zšđčćž]+$", ErrorMessage = "Unesite važeće prezime. " +
                "Prezime mora početi velikim slovom i smije sadržavati samo abecedne znakove.")]
            [Display(Name = "Prezime", Prompt = "Ovdje unesite svoje prezime")]
            public string Prezime { get; set; }
            
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

            [DataType(DataType.Password, ErrorMessage = "Unesite važeću lozinku.")]
            [Display(Name = "Potvrdi lozinku", Prompt = "Ovdje potvrdi lozinku")]
            [Compare("Password", ErrorMessage = "Lozinke se ne podudaraju.")]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnGet(string returnUrl = null)
        {
            if (_repository.IsThereAdmin()) return Redirect("/");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var korisnickoIme = Input.Ime + Input.Prezime.ToCharArray()[0];
                var user = new IdentityUser { UserName = korisnickoIme, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _context.Add(new Zaposlenik
                    {
                        Ime = Input.Ime,
                        Prezime = Input.Prezime,
                        Email = Input.Email,
                        KorisnickoIme = korisnickoIme,
                        Lozinka = Input.Password,
                        User = user,
                        UlogaId = 1
                        
                    });
                    _context.SaveChanges();
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
