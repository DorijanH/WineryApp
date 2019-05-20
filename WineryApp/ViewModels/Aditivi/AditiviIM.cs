using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Aditivi
{
    public class AditiviIM
    {
        public int AditivId { get; set; }

        [Required(ErrorMessage = "Odaberite vrstu aditiva")]
        [Display(Name = "Vrsta aditiva")]
        public int VrstaAditivaId { get; set; }

        [Required(ErrorMessage = "Unesite ime aditiva")]
        [Display(Name = "Ime aditiva", Prompt = "Ovdje unesite ime aditiva")]
        public string ImeAditiva { get; set; }

        [Display(Prompt = "g/100mL")]
        public decimal? Koncentracija { get; set; }

        [Display(Prompt = "Količina aditiva u skladištu na raspolaganju (L)")]
        public decimal? Količina { get; set; }

        [Display(Prompt = "Instrukcije za korištenje aditiva")]
        public string Instrukcije { get; set; }
    }
}
