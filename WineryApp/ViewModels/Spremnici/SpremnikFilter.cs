using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Spremnici
{
    public class SpremnikFilter : IPageFilter
    {
        [Display(Name = "Vrsta spremnika")]
        public int? VrstaSpremnikaId { get; set; }

        [Display(Name = "Šifra podruma")]
        public int? PodrumId { get; set; }

        [Display(Name = "Godina berbe")]
        public int? BerbaId { get; set; }

        [Display(Name = "Punilac spremnika")]
        public int? PunilacId { get; set; }

        [Display(Name = "Sorta vina")]
        public int? SortaVinaId { get; set; }

        public bool IsEmpty()
        {
            bool active = VrstaSpremnikaId.HasValue
                          || PodrumId.HasValue
                          || BerbaId.HasValue
                          || PunilacId.HasValue
                          || SortaVinaId.HasValue;

            return !active;
        }

        public override string ToString()
        {
            return
                $"{VrstaSpremnikaId}-{PodrumId}-{BerbaId}-{PunilacId}-{SortaVinaId}";
        }

        public static SpremnikFilter FromString(string s)
        {
            var filter = new SpremnikFilter();
            var arr = s.Split(new char[] { '-' }, StringSplitOptions.None);
            try
            {
                filter.VrstaSpremnikaId = string.IsNullOrWhiteSpace(arr[0]) ? new int?() : int.Parse(arr[0]);
                filter.PodrumId = string.IsNullOrWhiteSpace(arr[1]) ? new int?() : int.Parse(arr[1]);
                filter.BerbaId = string.IsNullOrWhiteSpace(arr[2]) ? new int?() : int.Parse(arr[2]);
                filter.PunilacId = string.IsNullOrWhiteSpace(arr[3]) ? new int?() : int.Parse(arr[3]);
                filter.SortaVinaId = string.IsNullOrWhiteSpace(arr[4]) ? new int?() : int.Parse(arr[4]);
            }
            catch { } //to do: log...
            return filter;
        }
        public IQueryable<Spremnik> PrimjeniFilter(IQueryable<Spremnik> upit)
        {
            if (VrstaSpremnikaId.HasValue)
            {
                upit = upit.Where(s => s.VrstaSpremnikaId == VrstaSpremnikaId.Value);
            }
            if (PodrumId.HasValue)
            {
                upit = upit.Where(s => s.PodrumId == PodrumId.Value);
            }
            if (BerbaId.HasValue)
            {
                upit = upit.Where(s => s.BerbaId == BerbaId.Value);
            }
            if (PunilacId.HasValue)
            {
                upit = upit.Where(s => s.PunilacId == PunilacId.Value);
            }
            if (SortaVinaId.HasValue)
            {
                upit = upit.Where(s => s.SortaVinaId == SortaVinaId.Value);
            }

            return upit;
        }
    }
}
