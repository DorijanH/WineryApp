using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Aditivi
{
    public class AditivFilter
    {
        [Display(Name = "Vrsta aditiva")]
        public int? VrstaAditivaId { get; set; }

        public bool IsEmpty()
        {
            bool active = VrstaAditivaId.HasValue;

            return !active;
        }

        public override string ToString()
        {
            return
                $"{VrstaAditivaId}";
        }

        public static AditivFilter FromString(string s)
        {
            var filter = new AditivFilter();
            var arr = s.Split(new char[] { '-' }, StringSplitOptions.None);
            try
            {
                filter.VrstaAditivaId = string.IsNullOrWhiteSpace(arr[0]) ? new int?() : int.Parse(arr[0]);
            }
            catch { } //to do: log...
            return filter;
        }
        public IQueryable<Aditiv> PrimjeniFilter(IQueryable<Aditiv> upit)
        {
            if (VrstaAditivaId.HasValue)
            {
                upit = upit.Where(a => a.VrstaAditivaId == VrstaAditivaId.Value);
            }

            return upit;
        }
    }
}
