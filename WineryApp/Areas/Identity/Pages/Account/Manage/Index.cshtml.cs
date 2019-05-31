using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WineryApp.Data;

namespace WineryApp.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly IRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public IndexModel(IRepository repository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
        {
            _repository = repository;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Korisničko ime")]
            public string Username { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Unesite svoje ime.")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"^[A-ZŠĐČĆŽ][a-zšđčćž]+$", ErrorMessage = "Unesite važeće ime. " +
                "Ime mora početi velikim slovom i smije sadržavati samo abecedne znakove.")]
            [Display(Name = "Ime", Prompt = "Ovdje unesite svoje ime")]
            public string Name { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Unesite svoje prezime.")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"^[A-ZŠĐČĆŽ][a-zšđčćž]+$", ErrorMessage = "Unesite važeće prezime. " +
                "Prezime mora početi velikim slovom i smije sadržavati samo abecedne znakove.")]
            [Display(Name = "Prezime", Prompt = "Ovdje unesite svoje prezime")]
            public string Surename { get; set; }

            [Display(Name = "Spol")]
            public string Gender { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(
                @"^[A-ZŠĐČĆŽ][a-zšđčćž]+ ([A-ZŠĐČĆŽ]?[a-zšđčćž]+ )*[(X|V|I)* ]*[0-9]+[A-Z]?$",
            ErrorMessage = "Unesite važeću adresu .\n" +
                           "Adresa mora početi velikim slovom.\n" +
                           "Za broj odvojka ili ulice koristite rimske brojeve.\n" +
                           "Za kućni broj koristite arapski broj i veliko slovo po potrebi.")]
            [Display(Name = "Adresa", Prompt = "Ovdje unesite svoju adresu")]
            public string Address { get; set; }

            [Display(Name = "Grad", Prompt = "Ovdje unesite svoj grad")]
            public string City { get; set; }

            [DataType(DataType.PhoneNumber, ErrorMessage = "Unesite odgovarajući broj telefona")]
            [Display(Name = "Telefon", Prompt = "Ovdje unesite broj telefona")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Unesite svoj email.")]
            [EmailAddress(ErrorMessage = "Unesite važeći email.")]
            [Display(Name = "Email", Prompt = "Ovdje unesite svoj email.")]
            public string Email { get; set; }

            [DataType(DataType.Password, ErrorMessage = "Unesite važeću lozinku.")]
            [Display(Name = "Stara lozinka", Prompt = "Ovdje unesite staru lozinku")]
            public string OldPassword { get; set; }

            [StringLength(100, ErrorMessage = "{0} mora biti barem {2} znakova i maksimalno {1}.",
                MinimumLength = 6)]
            [DataType(DataType.Password, ErrorMessage = "Unesite važeću lozinku.")]
            [Display(Name = "Nova lozinka", Prompt = "Ovdje unesite novu lozinku")]
            public string NewPassword { get; set; }

        }

        public IActionResult OnGet()
        {
            var user = _userManager.GetUserId(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var korisnik = _repository.GetZaposlenik(user);

            Input = new InputModel
            {
                Name = korisnik.Ime,
                Surename = korisnik.Prezime,
                Gender = korisnik.Spol,
                Address = korisnik.Adresa,
                City = korisnik.Grad,
                PhoneNumber = korisnik.Telefon,
                Email = korisnik.Email,
                NewPassword = korisnik.Lozinka,
                Username = korisnik.User.UserName
        };

            IsEmailConfirmed = true;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var curUser = await _userManager.GetUserAsync(User);
            var userHash = _userManager.GetUserId(User);
            if (userHash == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var lozinkaOk = false;
            var emailOk = false;
            if (!string.IsNullOrWhiteSpace(Input.NewPassword))
            {
                var res = await _userManager.ChangePasswordAsync(curUser, Input.OldPassword, Input.NewPassword);
                lozinkaOk = res.Succeeded;
            }
            var email = await _userManager.GetEmailAsync(curUser);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(curUser, Input.Email);
                emailOk = setEmailResult.Succeeded;
            }

            _repository.UpdateZaposlenik(userHash, Input.Address, Input.Gender, Input.City, Input.PhoneNumber, emailOk ? Input.Email : default, Input.Name, lozinkaOk ? Input.NewPassword : default, Input.Surename, Input.Username);

            await _signInManager.RefreshSignInAsync(curUser);
            StatusMessage = "Vaš korisnički račun je ažuriran!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
