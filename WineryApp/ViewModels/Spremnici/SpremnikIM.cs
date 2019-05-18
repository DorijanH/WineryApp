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

        public string Kapacitet { get; set; }
        public string Napunjenost { get; set; }
        public string FazaIzrade { get; set; }
        public int VrstaSpremnikaId { get; set; }
        public int? BerbaId { get; set; }
        public int? PunilacId { get; set; }
        public int? PodrumId { get; set; }
        public int? SortaVinaId { get; set; }
    }
}
