using System;
using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Izvješća.Podrumi
{
    public class PodrumFilterIzvješće
    {
        public string Format { get; set; }

        [Display(Name = "Godina berbe")]
        public int BerbaId { get; set; }

        [Display(Name = "Sorta vina")]
        public int SortaVinaId { get; set; }
    }
}
