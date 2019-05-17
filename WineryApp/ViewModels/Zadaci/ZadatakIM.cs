﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WineryApp.ViewModels.Zadaci
{
    public class ZadatakIM
    {
        [Required(ErrorMessage = "Odaberite kategoriju zadatka")]
        [Display(Name = "Kategorija zadatka", Prompt = "Odaberite kategoriju zadatka")]
        public int KategorijaZadatkaId { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Unesite ime zadatka")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Ime zadatka", Prompt = "Ovdje unesite ime zadatka")]
        public string ImeZadatka { get; set; }

        [Display(Name = "Podrum", Prompt = "Odredite podrum za koji je zadatak")]
        public int PodrumId { get; set; }

        [Display(Name = "Spremnik", Prompt = "Odredite spremnik za koji je zadatak")]
        public int SpremnikId { get; set; }

        [Required(ErrorMessage = "Odredite početak zadatka")]
        [DataType(DataType.Date, ErrorMessage = "Odaberite važeći datum")]
        [Display(Name = "Početak zadatka", Prompt = "Odredite početak zadatka")]
        [Remote("ValidDate", "Zadaci", ErrorMessage = "Datum je već prošao!")]
        public DateTime PočetakZadatka { get; set; }

        [Required(ErrorMessage = "Odredite rok zadatka")]
        [DataType(DataType.Date, ErrorMessage = "Odaberite važeći datum")]
        [Display(Name = "Rok zadatka", Prompt = "Odredite rok zadatka")]
        [Remote("ValidDate", "Zadaci", AdditionalFields = "PočetakZadatka", ErrorMessage = "Datum je već prošao!")]
        public DateTime RokZadatka { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Opis zadatka", Prompt = "Ovdje unesite opis zadatka")]
        public string Bilješke { get; set; }

        [Required(ErrorMessage = "Pridjelite zadatak zaposleniku")]
        [Display(Name = "Zaposlenik", Prompt = "Odaberite zaposlenika")]
        public int ZaposlenikId { get; set; }
    }
}
