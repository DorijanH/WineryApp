using System;
using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Izvješća.Spremnici
{
    public class RezultatAnalizeFilterIzvješće
    {
        public string Format { get; set; }

        [Display(Name = "Podrum")]
        public int PodrumId { get; set; }

        [Display(Name = "Spremnik")]
        public int SpremnikId { get; set; }

        [Display(Name = "Uzorak uzeo")]
        public int UzorakUzeoId { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Analize od datuma")]
        public DateTime? DatumOd { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Analize do datuma")]
        public DateTime? DatumDo { get; set; }
    }
}
