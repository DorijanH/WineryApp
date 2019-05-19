using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Izvješća.Spremnici
{
    public class SpremnikFilterIzvješće
    {
        public string Format { get; set; }

        [Display(Name = "Vrsta spremnika")]
        public int VrstaSpremnikaId { get; set; }

        [Display(Name = "Šifra podruma")]
        public int PodrumId { get; set; }

        [Display(Name = "Godina berbe")]
        public int BerbaId { get; set; }

        [Display(Name = "Punilac spremnika")]
        public int PunilacId { get; set; }

        [Display(Name = "Sorta vina")]
        public int SortaVinaId { get; set; }
    }
}
