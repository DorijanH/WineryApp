using System;
using System.ComponentModel.DataAnnotations;

namespace WineryApp.Data.Entiteti
{
    public enum StatusZadatka
    {
        UTijeku,
        Zavrseno
    };

    public partial class Zadatak
    {
        public int ZadatakId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Zadatak mora imati svoje ime")]
        [Display(Name = "Ime zadatka")]
        public string ImeZadatka { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Odaberite važeći datum")]
        [Display(Name = "Početak zadatka")]
        public DateTime PočetakZadatka { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Odaberite važeći datum")]
        [Display(Name = "Rok zadatka")]
        public DateTime RokZadatka { get; set; }

        [Display(Name = "Status zadatka")]
        public byte? StatusZadatka { get; set; }


        public string Bilješke { get; set; }

        [Display(Name = "Podrum")]
        public int? PodrumId { get; set; }

        [Display(Name = "Spremnik")]
        public int? SpremnikId { get; set; }

        [Display(Name = "Kategorija zadatka")]
        public int KategorijaZadatkaId { get; set; }

        [Display(Name = "Zaduženi zaposlenik")]
        public int ZaduženiZaposlenik { get; set; }

        public virtual KategorijaZadatka KategorijaZadatka { get; set; }
        public virtual Podrum Podrum { get; set; }
        public virtual Spremnik Spremnik { get; set; }
        public virtual Zaposlenik ZaduženiZaposlenikNavigation { get; set; }
    }
}
