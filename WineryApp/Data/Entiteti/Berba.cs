using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WineryApp.Data.Entiteti
{
    public partial class Berba
    {
        public Berba()
        {
            Spremnik = new HashSet<Spremnik>();
        }

        public int BerbaId { get; set; }

        [Required(ErrorMessage = "Unesite godinu berbe")]
        [Display(Name = "Godina berbe")]
        [Remote("CheckYear", "Berba")]
        public int GodinaBerbe { get; set; }

        public virtual ICollection<Spremnik> Spremnik { get; set; }
    }
}
