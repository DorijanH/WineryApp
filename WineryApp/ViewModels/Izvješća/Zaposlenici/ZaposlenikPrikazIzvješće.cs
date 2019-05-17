using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WineryApp.ViewModels.Izvješća.Zaposlenici
{
    public class ZaposlenikPrikazIzvješće
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Spol { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        [Display(Name = "Datum zaposlenja")]
        public string DatumZaposlenja { get; set; }

        [Display(Name = "Korisnicko ime")]
        public string KorisnickoIme { get; set; }
    }
}
