using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Podrumi
{
    public class PodrumIM
    {
        [Required(ErrorMessage = "Unesite šifru podruma")]
        [Display(Name = "Šifra podruma", Prompt = "Unesite šifru podruma")]
        public string ŠifraPodruma { get; set; }

        [Required(ErrorMessage = "Unesite adresu lokacije podruma")]
        [Display(Name = "Adresa podruma", Prompt = "Unesite adresu podruma")]
        public string Lokacija { get; set; }

        [Required(ErrorMessage = "Odaberite sortu vina koja će se nalaziti u podrumu")]
        [Display(Name = "Sorta vina")]
        public int SortaVinaId { get; set; }
    }
}
