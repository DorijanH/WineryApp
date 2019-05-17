using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Podrumi
{
    public class PodrumiFilter : IPageFilter
    {
        [Display(Name = "Status")]
        public int? Status { get; set; }

        [Display(Name = "Odgovorna osoba")]
        public int? OdgovornaOsobaId { get; set; }

        [Display(Name = "Kategorija zadatka")]
        public int? KategorijaZadatkaId { get; set; }

        [Display(Name = "Podrum")]
        public int? PodrumId { get; set; }

        [Display(Name = "Spremnik")]
        public int? SpremnikId { get; set; }

        [Display(Name = "Datum od")]
        [DataType(DataType.Date, ErrorMessage = "Odaberite važeći datum")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", ApplyFormatInEditMode = false)]
        public DateTime? DatumOd { get; set; }

        [Display(Name = "Datum do")]
        [DataType(DataType.Date, ErrorMessage = "Odaberite važeći datum")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", ApplyFormatInEditMode = false)]
        public DateTime? DatumDo { get; set; }

        public bool IsEmpty()
        {
            bool active = Status.HasValue
                          || DatumOd.HasValue
                          || DatumDo.HasValue
                          || OdgovornaOsobaId.HasValue
                          || KategorijaZadatkaId.HasValue
                          || PodrumId.HasValue
                          || SpremnikId.HasValue;
            return !active;
        }

        public override string ToString()
        {
            return
                $"{Status}-{DatumOd?.ToString("dd.MM.yyyy")}-{DatumDo?.ToString("dd.MM.yyyy")}-{OdgovornaOsobaId}-{KategorijaZadatkaId}-{PodrumId}-{SpremnikId}";
        }

        public static Zadaci.ZadaciFilter FromString(string s)
        {
            var filter = new Zadaci.ZadaciFilter();
            var arr = s.Split(new char[] { '-' }, StringSplitOptions.None);
            try
            {
                filter.Status = string.IsNullOrWhiteSpace(arr[0]) ? new int?() : int.Parse(arr[0]);
                filter.DatumOd = string.IsNullOrWhiteSpace(arr[1]) ? new DateTime?() : DateTime.ParseExact(arr[1], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                filter.DatumDo = string.IsNullOrWhiteSpace(arr[2]) ? new DateTime?() : DateTime.ParseExact(arr[2], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                filter.OdgovornaOsobaId = string.IsNullOrWhiteSpace(arr[3]) ? new int?() : int.Parse(arr[3]);
                filter.KategorijaZadatkaId = string.IsNullOrWhiteSpace(arr[4]) ? new int?() : int.Parse(arr[4]);
                filter.PodrumId = string.IsNullOrWhiteSpace(arr[5]) ? new int?() : int.Parse(arr[5]);
                filter.SpremnikId = string.IsNullOrWhiteSpace(arr[6]) ? new int?() : int.Parse(arr[6]);
            }
            catch { } //to do: log...
            return filter;
        }
        public IQueryable<Zadatak> PrimjeniFilter(IQueryable<Zadatak> upit)
        {
            if (Status.HasValue)
            {
                upit = upit.Where(z => z.StatusZadatka == Status.Value);
            }
            if (DatumOd.HasValue)
            {
                upit = upit.Where(z => z.PočetakZadatka >= DatumOd.Value);
            }
            if (DatumDo.HasValue)
            {
                upit = upit.Where(z => z.RokZadatka <= DatumDo.Value);
            }
            if (OdgovornaOsobaId.HasValue)
            {
                upit = upit.Where(z => z.ZaduženiZaposlenik == OdgovornaOsobaId.Value);
            }
            if (KategorijaZadatkaId.HasValue)
            {
                upit = upit.Where(z => z.KategorijaZadatkaId == KategorijaZadatkaId.Value);
            }
            if (PodrumId.HasValue)
            {
                upit = upit.Where(z => z.PodrumId == PodrumId.Value);
            }
            if (SpremnikId.HasValue)
            {
                upit = upit.Where(z => z.SpremnikId == SpremnikId.Value);
            }
            return upit;
        }
    }
}
