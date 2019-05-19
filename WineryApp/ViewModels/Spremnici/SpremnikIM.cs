using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Spremnici
{
    public class SpremnikIM
    {
        public int SpremnikId { get; set; }

        [Required(ErrorMessage = "Unesite šifru spremnika")]
        [Display(Name = "Šifra spremnika")]
        [Remote("CheckCode", "Spremnici", ErrorMessage = "Spremnik s unesenom šifrom već postoji!")]
        public string ŠifraSpremnika { get; set; }

        [Required(ErrorMessage = "Unesite kapacitet spremnika")]
        [Display(Name = "Kapacitet spremnika (u litrama)")]
        [Range(0, float.MaxValue, ErrorMessage = "Kapacitet ne može biti negativan broj!")]
        public float Kapacitet { get; set; }

        [Display(Name = "Napunjenost spremnika (u litrama)")]
        [Range(0, float.MaxValue, ErrorMessage = "Napunjenost ne može biti negativan broj!")]
        [Remote("CheckFillValue", "Spremnici", AdditionalFields = "Kapacitet")]
        public float Napunjenost { get; set; }

        [Display(Name = "Faza izrade")]
        public string FazaIzrade { get; set; }

        [Required(ErrorMessage = "Odredite vrstu spremnika")]
        [Display(Name = "Vrsta spremnika")]
        public int VrstaSpremnikaId { get; set; }

        [Display(Name = "Godina berbe")]
        public int? BerbaId { get; set; }

        [Display(Name = "Punilac spremnika")]
        public int? PunilacId { get; set; }

        [Required(ErrorMessage = "Odaberite kojem podrumu pripada spremnik")]
        [Display(Name = "Podrum")]
        public int PodrumId { get; set; }

        [Display(Name = "Sorta vina")]
        public int? SortaVinaId { get; set; }
    }
}
