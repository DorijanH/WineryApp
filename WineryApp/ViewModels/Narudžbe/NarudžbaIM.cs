using System;
using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Narudžbe
{
    public class NarudžbaIM
    {
        public int NarudžbaId { get; set; }

        public byte? Status { get; set; }

        [Display(Name = "Datum narudžbe")]
        public DateTime? DatumNarudzbe { get; set; }

        [Display(Name = "Datum isporuke")]
        public DateTime? DatumIsporuke { get; set; }

        [Display(Name = "Datum naplate")]
        public DateTime? DatumNaplate { get; set; }

        [Display(Name = "Ime kupca", Prompt = "Unesite ime kupca")]
        public string ImeKupca { get; set; }

        [Display(Name = "Prezime kupca", Prompt = "Unesite prezime kupca")]
        public string PrezimeKupca { get; set; }

        [Display(Name = "Adresa kupca", Prompt = "Unesite adresu kupca")]
        public string AdresaKupca { get; set; }

        [Required(ErrorMessage = "Unesite naručenu količinu vina")]
        [Display(Prompt = "Unesite naručenu količinu vina")]
        public decimal Količina { get; set; }

        [Display(Name = "Konačna cijena (HRK)")]
        public decimal KonacnaCijena { get; set; }

        [Required(ErrorMessage = "Odaberite podrum iz kojeg uzimate vino")]
        [Display(Name = "Podrum")]
        public int PodrumId { get; set; }

        [Required(ErrorMessage = "Odaberite spremnik iz kojeg uzimate vino")]
        [Display(Name = "Spremnik")]
        public int SpremnikId { get; set; }

        [Display(Name = "Partner")]
        public int? PartnerId { get; set; }
    }
}
