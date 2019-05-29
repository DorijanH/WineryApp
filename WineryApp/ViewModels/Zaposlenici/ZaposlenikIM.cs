using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WineryApp.ViewModels.Zaposlenici
{
    public class ZaposlenikIM
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unesite ime zaposlenika")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Ime zaposlenika", Prompt = "Ovdje unesite ime zaposlenika")]
        public string Ime { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Unesite prezime zaposlenika")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Prezime zaposlenika", Prompt = "Ovdje unesite prezime zaposlenika")]
        public string Prezime { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Odaberite spol")]
        [Display(Name = "Spol", Prompt = "Odaberite spol")]
        public string Spol { get; set; }

        [Display(Name = "Adresa zaposlenika", Prompt = "Ovdje unesite adresu zaposlenika")]
        public string Adresa { get; set; }

        [Display(Name = "Grad zaposlenika", Prompt = "Ovdje unesite grad zaposlenika")]
        public string Grad { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Unesite valjani kontakt broj")]
        [Display(Name = "Kontakt broj zaposlenika", Prompt = "Ovdje unesite kontakt broj zaposlenika")]
        public string Telefon { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Unesite email zaposlenika")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Unesite valjanu email adresu")]
        [Display(Name = "Email adresa zaposlenika", Prompt = "Ovdje unesite email adresu zaposlenika")]
        [Remote(action:"ProvjeraEmailAdrese", controller:"Zaposlenici", ErrorMessage = "Email adresa se već koristi!")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Unesite lozinku za zaposlenika")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.Password, ErrorMessage = "Unesite lozinku za zaposlenika")]
        [Display(Name = "Lozinka za login zaposlenika", Prompt = "Ovdje unesite lozinku za zaposlenika")]
        public string Lozinka { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite valjani datum")]
        [Display(Name = "Datum zaposlenja zaposlenika", Prompt = "Ovdje unesite datum zaposlenja zaposlenika")]
        public DateTime? DatumZaposlenja { get; set; }

        [Display(Name = "Korisničko ime zaposlenika")]
        public string KorisnickoIme { get; set; }
    }
}
