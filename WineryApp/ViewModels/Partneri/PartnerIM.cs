using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Partneri
{
    public class PartnerIM
    {
        public int PartnerId { get; set; }

        [Required(ErrorMessage = "Unesite ime partnera")]
        [Display(Name = "Ime partnera", Prompt = "Unesite ime partnera")]
        public string ImePartnera { get; set; }

        [Required(ErrorMessage = "Unesite OIB partnera")]
        [Display(Prompt = "Unesite OIB partnera")]
        [RegularExpression(@"[0-9]{11}", ErrorMessage = "OIB čine 11 znamenaka!")]
        public string OIB { get; set; }

        [Required(ErrorMessage = "Unesite kontakt broj partnera")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Unesite valjani kontakt broj")]
        [Display(Name = "Kontakt broj", Prompt = "Unesite kontakt broj partnera")]
        public string KontaktBroj { get; set; }

        [Required(ErrorMessage = "Unesite adresu partnera")]
        [Display(Prompt = "Unesite adresu partnera")]
        public string Adresa { get; set; }
    }
}
