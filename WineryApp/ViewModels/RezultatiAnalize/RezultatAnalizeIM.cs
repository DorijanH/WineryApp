using System;
using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.RezultatiAnalize
{
    public class RezultatAnalizeIM
    {
        public int RezultatAnalizeId { get; set; }

        [Required(ErrorMessage = "Unesite datum analize uzorka")]
        [DataType(DataType.Date, ErrorMessage = "Odaberite važeći datum")]
        [Display(Name = "Datum analize uzorka")]
        public DateTime DatumUzorka { get; set; }

        [Required(ErrorMessage = "Odaberite podrum")]
        [Display(Name = "Podrum")]
        public int PodrumId { get; set; }

        [Required(ErrorMessage = "Odaberite spremnik")]
        [Display(Name = "Spremnik")]
        public int SpremnikId { get; set; }

        [Required(ErrorMessage = "Unesite šifru uzorka")]
        [Display(Name = "Šifra uzorka")]
        public string ŠifraUzorka { get; set; }

        [Required(ErrorMessage = "Unesite pH vrijednost")]
        [Display(Name = "pH vrijednost")]
        public decimal PhVrijednost { get; set; }

        [Required(ErrorMessage = "Unesite koncentraciju šećera")]
        [Display(Name = "Koncentracija šećera", Prompt = "x,x (g/L)")]
        public decimal Šećer { get; set; }

        [Required(ErrorMessage = "Unesite koncentraciju rezidualnog šećera")]
        [Display(Name = "Koncentracija rezidualnog šećera", Prompt = "x,x (g/L)")]
        public decimal RezidualniŠećer { get; set; }

        [Required(ErrorMessage = "Unesite koncentraciju slobodnog sumpora")]
        [Display(Name = "Koncentracija slobodnog sumpora", Prompt = "x,x (mg/L)")]
        public decimal SlobodniSumpor { get; set; }

        [Required(ErrorMessage = "Unesite koncentraciju ukupnog sumpora")]
        [Display(Name = "Koncentracija ukupnog sumpora", Prompt = "x,x (mg/L)")]
        public decimal UkupniSumpor { get; set; }

        [Required(ErrorMessage = "Unesite koncentraciju kiseline")]
        [Display(Name = "Koncentracija kiseline", Prompt = "x,x (g/L)")]
        public decimal Kiselina { get; set; }

        [Required(ErrorMessage = "Unesite postotak alkohola")]
        [Range(0, 100, ErrorMessage = "Postotak ne može biti manji od 0 ili veći od 100!")]
        [Display(Name = "Postotak alkohola", Prompt = "%")]
        public decimal PostotakAlkohola { get; set; }

        [Required(ErrorMessage = "Odredite zaposlenika")]
        [Display(Name = "Uzorak uzeo i izmjerio")]
        public int UzorakUzeoId { get; set; }
    }
}
