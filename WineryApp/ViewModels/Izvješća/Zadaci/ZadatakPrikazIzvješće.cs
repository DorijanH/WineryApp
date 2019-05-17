using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WineryApp.ViewModels.Izvješća
{
    public class ZadatakPrikazIzvješće
    {
        public string Naziv { get; set; }

        public string Kategorija { get; set; }

        [Display(Name = "Početak zadatka")]
        public string PočetakZadatka { get; set; }

        [Display(Name = "Rok zadatka")]
        public string RokZadatka { get; set; }

        [Display(Name = "Zaduženi zaposlenik")]
        public string ZaduženiZaposlenik { get; set; }
       
    }
}
