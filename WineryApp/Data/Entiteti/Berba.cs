using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WineryApp.Data.Entiteti
{
    public partial class Berba
    {
        public Berba()
        {
            PodrumBerba = new HashSet<PodrumBerba>();
        }

        public int BerbaId { get; set; }

        [Required(ErrorMessage = "Unesite godinu berbe")]
        [Display(Name = "Godina berbe")]
        [Remote("ProvjeriGodinu", "Berba")]
        public int GodinaBerbe { get; set; }

        public virtual ICollection<PodrumBerba> PodrumBerba { get; set; }
    }
}
