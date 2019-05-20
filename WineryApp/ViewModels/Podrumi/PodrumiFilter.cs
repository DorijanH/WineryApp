using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WineryApp.Data;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Podrumi
{
    public class PodrumiFilter
    {
        private readonly IRepository _repository;

        public PodrumiFilter()
        {
            
        }

        public PodrumiFilter(IRepository repository)
        {
            _repository = repository;
        }

        [Display(Name = "Godina berbe")]
        public int? BerbaId { get; set; }

        [Display(Name = "Sorta vina")]
        public int? SortaVinaId { get; set; }

        public bool IsEmpty()
        {
            bool active = BerbaId.HasValue || SortaVinaId.HasValue;

            return !active;
        }

        public override string ToString()
        {
            return
                $"{BerbaId}-{SortaVinaId}";
        }

        public static PodrumiFilter FromString(string s, IRepository repository)
        {
            var filter = new PodrumiFilter(repository);
            var arr = s.Split(new char[] { '-' }, StringSplitOptions.None);
            try
            {
                filter.BerbaId = string.IsNullOrWhiteSpace(arr[0]) ? new int?() : int.Parse(arr[0]);
                filter.SortaVinaId = string.IsNullOrWhiteSpace(arr[1]) ? new int?() : int.Parse(arr[1]);
            }
            catch { } //to do: log...
            return filter;
        }
        public IQueryable<Podrum> PrimjeniFilter(IQueryable<Podrum> upit)
        {
            if (BerbaId.HasValue)
            {
                var spremniciSGodinom = _repository.GetAllSpremnikWithVintage(BerbaId.Value);
                var podrumiSTimSpremnicima = spremniciSGodinom.Select(s => s.Podrum).Distinct().ToList();

                upit = upit.Where(p => podrumiSTimSpremnicima.Contains(p));
            }
            if (SortaVinaId.HasValue)
            {
                upit = upit.Where(p => p.Spremnik.Any(s => s.SortaVinaId == SortaVinaId));
            }
            return upit;
        }
    }
}
