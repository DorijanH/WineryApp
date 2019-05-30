using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.RezultatiAnalize
{
    public class RezultatAnalizeFilter
    {
        [Display(Name = "Podrum")]
        public int? PodrumId { get; set; }

        [Display(Name = "Spremnik")]
        public int? SpremnikId { get; set; }

        [Display(Name = "Uzorak uzeo")]
        public int? UzorakUzeoId { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Analize od datuma")]
        public DateTime? DatumOd { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Analize do datuma")]
        public DateTime? DatumDo { get; set; }

        public bool IsEmpty()
        {
            bool active = PodrumId.HasValue
                          || SpremnikId.HasValue
                          || UzorakUzeoId.HasValue
                          || DatumOd.HasValue
                          || DatumDo.HasValue;

            return !active;
        }

        public override string ToString()
        {
            return
                $"{PodrumId}-{SpremnikId}-{UzorakUzeoId}-{DatumOd?.ToString("dd.MM.yyyy")}-{DatumDo?.ToString("dd.MM.yyyy")}";
        }

        public static RezultatAnalizeFilter FromString(string s)
        {
            var filter = new RezultatAnalizeFilter();
            var arr = s.Split(new char[] { '-' }, StringSplitOptions.None);
            try
            {
                filter.PodrumId = string.IsNullOrWhiteSpace(arr[0]) ? new int?() : int.Parse(arr[0]);
                filter.SpremnikId = string.IsNullOrWhiteSpace(arr[1]) ? new int?() : int.Parse(arr[1]);
                filter.UzorakUzeoId = string.IsNullOrWhiteSpace(arr[2]) ? new int?() : int.Parse(arr[2]);
                filter.DatumOd = string.IsNullOrWhiteSpace(arr[3]) ? new DateTime?() : DateTime.ParseExact(arr[3], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                filter.DatumDo = string.IsNullOrWhiteSpace(arr[4]) ? new DateTime?() : DateTime.ParseExact(arr[4], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            catch { } //to do: log...
            return filter;
        }
        public IQueryable<RezultatAnalize> PrimjeniFilter(IQueryable<RezultatAnalize> upit)
        {
            if (PodrumId.HasValue)
            {
                upit = upit.Where(ra => ra.Spremnik.PodrumId == PodrumId.Value);
            }
            if (SpremnikId.HasValue)
            {
                upit = upit.Where(ra => ra.SpremnikId == SpremnikId.Value);
            }
            if (UzorakUzeoId.HasValue)
            {
                upit = upit.Where(ra => ra.UzorakUzeoId == UzorakUzeoId.Value);
            }
            if (DatumOd.HasValue)
            {
                upit = upit.Where(ra => ra.DatumUzimanjaUzorka >= DatumOd.Value);
            }
            if (DatumDo.HasValue)
            {
                upit = upit.Where(ra => ra.DatumUzimanjaUzorka <= DatumDo.Value);
            }

            return upit;
        }
    }
}
