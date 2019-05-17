using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WineryApp.ViewModels.Izvješća
{
    public class ZadatakFilterIzvješće
    {
        public string Format { get; set; }

        [Display(Name = "Osoba")]
        public int ZaposlenikId { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Kategorija")]
        public int KategorijaZadatkaId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Početak zadatka")]
        public DateTime? PočetakZadatka { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Rok zadatka")]
        public DateTime? RokZadatka { get; set; }

    }
}
