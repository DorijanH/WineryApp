using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Izvješća.Podrumi
{
    public class AditivFilterIzvješće
    {
        public string Format { get; set; }

        [Display(Name = "Vrsta aditiva")]
        public int VrstaAditivaId { get; set; }
    }
}
