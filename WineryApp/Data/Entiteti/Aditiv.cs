using System.Collections.Generic;

namespace WineryApp.Data.Entiteti
{
    public partial class Aditiv
    {
        public Aditiv()
        {
            PovijestAditiva = new HashSet<PovijestAditiva>();
            Zadatak = new HashSet<Zadatak>();
        }

        public int AditivId { get; set; }
        public string ImeAditiva { get; set; }
        public decimal? Koncentracija { get; set; }
        public decimal? Količina { get; set; }
        public string Instrukcije { get; set; }
        public int VrstaAditivaId { get; set; }

        public virtual VrstaAditiva VrstaAditiva { get; set; }
        public virtual ICollection<PovijestAditiva> PovijestAditiva { get; set; }
        public virtual ICollection<Zadatak> Zadatak { get; set; }
    }
}
