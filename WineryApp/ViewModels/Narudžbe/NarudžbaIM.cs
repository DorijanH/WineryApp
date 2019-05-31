using System;
using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Narudžbe
{
    public class NarudžbaIM
    {
        public int NarudžbaId { get; set; }

        public byte? Status { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum narudžbe")]
        public DateTime? DatumNarudzbe { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum isporuke")]
        public DateTime? DatumIsporuke { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum naplate")]
        public DateTime? DatumNaplate { get; set; }

        [Required(ErrorMessage = "Unesite ime kupca")]
        [Display(Name = "Ime kupca", Prompt = "Unesite ime kupca")]
        public string ImeKupca { get; set; }

        [Required(ErrorMessage = "Unesite prezime kupca")]
        [Display(Name = "Prezime kupca", Prompt = "Unesite prezime kupca")]
        public string PrezimeKupca { get; set; }

        [Display(Name = "Adresa kupca", Prompt = "Unesite adresu kupca")]
        public string AdresaKupca { get; set; }

        [Required(ErrorMessage = "Unesite naručenu količinu vina")]
        [Display(Prompt = "Naručena količina vina u litrama (L)")]
        public decimal Količina { get; set; }

        [Display(Name = "Konačna cijena (HRK)")]
        public decimal KonacnaCijena { get; set; }

        [Required(ErrorMessage = "Odaberite podrum iz kojeg uzimate vino")]
        [Display(Name = "Podrum")]
        public int PodrumId { get; set; }

        [Required(ErrorMessage = "Odaberite spremnik iz kojeg uzimate vino")]
        [Display(Name = "Spremnik")]
        public int SpremnikId { get; set; }

        [Required(ErrorMessage = "Odaberite partnera")]
        [Display(Name = "Partner")]
        public int? PartnerId { get; set; }
    }
}
