using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.RezultatiAnalize;

namespace WineryApp.ViewModels.Narudžbe
{
    public class NarudžbaFilter
    {
        [Display(Name = "Kupac")]
        public int? PartnerId { get; set; }

        [Display(Name = "Status narudžbe")]
        public int? StatusNarudžbe { get; set; }

        [Display(Name = "Spremnik")]
        public int? SpremnikId { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Naručeno od")]
        public DateTime? NarudžbaOd { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Naručeno do")]
        public DateTime? NarudžbaDo { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Isporučeno od")]
        public DateTime? IsporukaOd { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Isporučeno do")]
        public DateTime? IsporukaDo { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Naplaćeno od")]
        public DateTime? NaplataOd { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Naplaćeno do")]
        public DateTime? NaplataDo { get; set; }

        public bool IsEmpty()
        {
            bool active = PartnerId.HasValue
                          || StatusNarudžbe.HasValue
                          || SpremnikId.HasValue
                          || NarudžbaOd.HasValue
                          || NarudžbaDo.HasValue
                          || IsporukaOd.HasValue
                          || IsporukaDo.HasValue
                          || NaplataOd.HasValue
                          || NaplataDo.HasValue;

            return !active;
        }

        public override string ToString()
        {
            return
                $"{PartnerId}-{StatusNarudžbe}-{SpremnikId}-{NarudžbaOd?.ToString("dd.MM.yyyy")}-{NarudžbaDo?.ToString("dd.MM.yyyy")}" +
                $"-{IsporukaOd?.ToString("dd.MM.yyyy")}-{IsporukaDo?.ToString("dd.MM.yyyy")}" +
                $"-{NaplataOd?.ToString("dd.MM.yyyy")}-{NaplataDo?.ToString("dd.MM.yyyy")}";
        }

        public static NarudžbaFilter FromString(string s)
        {
            var filter = new NarudžbaFilter();
            var arr = s.Split(new char[] { '-' }, StringSplitOptions.None);
            try
            {
                filter.PartnerId = string.IsNullOrWhiteSpace(arr[0]) ? new int?() : int.Parse(arr[0]);
                filter.StatusNarudžbe = string.IsNullOrWhiteSpace(arr[1]) ? new int?() : int.Parse(arr[1]);
                filter.SpremnikId = string.IsNullOrWhiteSpace(arr[2]) ? new int?() : int.Parse(arr[2]);
                filter.NarudžbaOd = string.IsNullOrWhiteSpace(arr[3]) ? new DateTime?() : DateTime.ParseExact(arr[3], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                filter.NarudžbaDo = string.IsNullOrWhiteSpace(arr[4]) ? new DateTime?() : DateTime.ParseExact(arr[4], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                filter.IsporukaOd = string.IsNullOrWhiteSpace(arr[5]) ? new DateTime?() : DateTime.ParseExact(arr[5], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                filter.IsporukaDo = string.IsNullOrWhiteSpace(arr[6]) ? new DateTime?() : DateTime.ParseExact(arr[6], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                filter.NaplataOd = string.IsNullOrWhiteSpace(arr[7]) ? new DateTime?() : DateTime.ParseExact(arr[7], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                filter.NaplataDo = string.IsNullOrWhiteSpace(arr[8]) ? new DateTime?() : DateTime.ParseExact(arr[8], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            catch { } //to do: log...
            return filter;
        }
        public IQueryable<Narudžba> PrimjeniFilter(IQueryable<Narudžba> upit)
        {
            if (PartnerId.HasValue)
            {
                upit = PartnerId.Value == 0 ? upit.Where(n => !n.PartnerId.HasValue) : upit.Where(n => n.PartnerId == PartnerId.Value);
            }
            if (StatusNarudžbe.HasValue)
            {
                upit = upit.Where(n => n.Status == StatusNarudžbe.Value);
            }
            if (SpremnikId.HasValue)
            {
                upit = upit.Where(n => n.SpremnikId == SpremnikId.Value);
            }
            if (NarudžbaOd.HasValue)
            {
                upit = upit.Where(n => n.DatumNarudzbe >= NarudžbaOd.Value);
            }
            if (NarudžbaDo.HasValue)
            {
                upit = upit.Where(n => n.DatumNarudzbe <= NarudžbaDo.Value);
            }
            if (IsporukaOd.HasValue)
            {
                upit = upit.Where(n => n.DatumIsporuke >= IsporukaOd.Value);
            }
            if (IsporukaDo.HasValue)
            {
                upit = upit.Where(n => n.DatumIsporuke <= IsporukaDo.Value);
            }
            if (NaplataOd.HasValue)
            {
                upit = upit.Where(n => n.DatumNaplate >= NaplataOd.Value);
            }
            if (NaplataDo.HasValue)
            {
                upit = upit.Where(n => n.DatumNaplate <= NaplataDo.Value);
            }

            return upit;
        }
    }
}
