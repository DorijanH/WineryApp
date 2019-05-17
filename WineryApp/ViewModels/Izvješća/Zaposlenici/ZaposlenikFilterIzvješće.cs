﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WineryApp.ViewModels.Izvješća.Zaposlenici
{
    public class ZaposlenikFilterIzvješće
    {
        public string Format { get; set; }

        public string Spol { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Zaposleni od datuma")]
        public DateTime? DatumOd { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Zaposleni do datuma")]
        public DateTime? DatumDo { get; set; }
    }
}
