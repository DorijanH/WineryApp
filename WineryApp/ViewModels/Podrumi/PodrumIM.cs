using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Podrumi
{
    public class PodrumIM
    {
        public int PodrumId { get; set; }

        [Required(ErrorMessage = "Unesite šifru podruma")]
        [Display(Name = "Šifra podruma", Prompt = "Unesite šifru podruma")]
        public string ŠifraPodruma { get; set; }

        [Required(ErrorMessage = "Unesite adresu lokacije podruma")]
        [Display(Name = "Adresa podruma", Prompt = "Unesite adresu podruma")]
        public string Lokacija { get; set; }
    }
}
