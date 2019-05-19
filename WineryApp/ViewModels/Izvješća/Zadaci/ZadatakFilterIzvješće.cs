using System;
using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Izvješća.Zadaci
{
    public class ZadatakFilterIzvješće
    {
        public string Format { get; set; }

        [Display(Name = "Osoba")]
        public int ZaposlenikId { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Kategorija")]
        public int KategorijaZadatkaId { get; set; }

        [Display(Name = "Podrum")]
        public int PodrumId { get; set; }

        [Display(Name = "Spremnik")]
        public int SpremnikId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Početak zadatka")]
        public DateTime? PočetakZadatka { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Rok zadatka")]
        public DateTime? RokZadatka { get; set; }

    }
}
