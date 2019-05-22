using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WineryApp.Data.Entiteti
{
    public partial class Zaposlenik
    {
        public Zaposlenik()
        {
            PovijestAditiva = new HashSet<PovijestAditiva>();
            PovijestSpremnika = new HashSet<PovijestSpremnika>();
            RezultatAnalize = new HashSet<RezultatAnalize>();
            Spremnik = new HashSet<Spremnik>();
            Zadatak = new HashSet<Zadatak>();
        }

        public int ZaposlenikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Spol { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }

        [Display(Name = "Datum zaposlenja")]
        public DateTime DatumZaposlenja { get; set; }

        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Display(Name = "Uloga")]
        public int UlogaId { get; set; }
        public Microsoft.AspNetCore.Identity.IdentityUser User { get; set; }
        public virtual Uloga Uloga { get; set; }
        public virtual ICollection<PovijestAditiva> PovijestAditiva { get; set; }
        public virtual ICollection<PovijestSpremnika> PovijestSpremnika { get; set; }
        public virtual ICollection<RezultatAnalize> RezultatAnalize { get; set; }
        public virtual ICollection<Spremnik> Spremnik { get; set; }
        public virtual ICollection<Zadatak> Zadatak { get; set; }
    }
}
