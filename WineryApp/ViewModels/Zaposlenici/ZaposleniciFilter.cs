using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Zaposlenici
{
    public class ZaposleniciFilter
    {
        [Display(Name = "Spol")]
        public string Spol { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Zaposleni od datuma")]
        public DateTime? DatumOd { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        [Display(Name = "Zaposleni do datuma")]
        public DateTime? DatumDo { get; set; }

        public bool IsEmpty()
        {
            bool active = !string.IsNullOrEmpty(Spol)
                          || DatumOd.HasValue
                          || DatumDo.HasValue;
            return !active;
        }

        public override string ToString()
        {
            return
                $"{Spol}-{DatumOd?.ToString("dd.MM.yyyy")}-{DatumDo?.ToString("dd.MM.yyyy")}";
        }

        public static ZaposleniciFilter FromString(string s)
        {
            var filter = new ZaposleniciFilter();
            var arr = s.Split(new char[] { '-' }, StringSplitOptions.None);
            try
            {
                filter.Spol = string.IsNullOrWhiteSpace(arr[0]) ? "" : arr[0];
                filter.DatumOd = string.IsNullOrWhiteSpace(arr[1]) ? new DateTime?() : DateTime.ParseExact(arr[1], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                filter.DatumDo = string.IsNullOrWhiteSpace(arr[2]) ? new DateTime?() : DateTime.ParseExact(arr[2], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            catch { } //to do: log...
            return filter;
        }
        public IQueryable<Zaposlenik> PrimjeniFilter(IQueryable<Zaposlenik> upit)
        {
            if (!string.IsNullOrEmpty(Spol))
            {
                upit = upit.Where(z => z.Spol == Spol);
            }
            if (DatumOd.HasValue)
            {
                upit = upit.Where(z => z.DatumZaposlenja >= DatumOd.Value);
            }
            if (DatumDo.HasValue)
            {
                upit = upit.Where(z => z.DatumZaposlenja <= DatumDo.Value);
            }
            
            return upit;
        }
    }
}
