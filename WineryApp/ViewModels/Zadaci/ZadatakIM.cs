using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Zadaci
{
    public class ZadatakIM
    {
        public int ZadatakId { get; set; }

        [Display(Name = "Status zadatka")]
        public byte? StatusZadatka { get; set; }

        [Required(ErrorMessage = "Odaberite kategoriju zadatka")]
        [Display(Name = "Kategorija zadatka", Prompt = "Odaberite kategoriju zadatka")]
        public int KategorijaZadatkaId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Unesite ime zadatka")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Ime zadatka", Prompt = "Ovdje unesite ime zadatka")]
        public string ImeZadatka { get; set; }

        [Display(Name = "Podrum", Prompt = "Odredite podrum za koji je zadatak")]
        public int? PodrumId { get; set; }

        [Display(Name = "Spremnik", Prompt = "Odredite spremnik za koji je zadatak")]
        public int? SpremnikId { get; set; }

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
        [Display(Name = "Bilješke", Prompt = "Ovdje unesite bilješke")]
        public string Bilješke { get; set; }

        [Required(ErrorMessage = "Pridjelite zadatak zaposleniku")]
        [Display(Name = "Zaposlenik", Prompt = "Odaberite zaposlenika")]
        public int ZaposlenikId { get; set; }

        [Required(ErrorMessage = "Odaberite vrstu aditiva")]
        [Display(Name = "Vrsta aditiva")]
        public int? VrstaAditivaId { get; set; }

        [Required(ErrorMessage = "Odaberite aditiv")]
        [Display(Name = "Aditiv")]
        public int? AditivId { get; set; }

    }
}
