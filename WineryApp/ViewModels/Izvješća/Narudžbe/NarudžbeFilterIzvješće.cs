using System;
using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Izvješća.Narudžbe
{
    public class NarudžbeFilterIzvješće
    {
        public string Format { get; set; }

        [Display(Name = "Kupac")]
        public int PartnerId { get; set; }

        [Display(Name = "Status narudžbe")]
        public int StatusNarudžbe { get; set; }

        [Display(Name = "Spremnik")]
        public int SpremnikId { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Naručeno od")]
        public DateTime? NarudžbaOd { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Naručeno do")]
        public DateTime? NarudžbaDo { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Isporučeno od")]
        public DateTime? IsporukaOd { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Isporučeno do")]
        public DateTime? IsporukaDo { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Naplaćeno od")]
        public DateTime? NaplataOd { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Naplaćeno do")]
        public DateTime? NaplataDo { get; set; }
    }
}
