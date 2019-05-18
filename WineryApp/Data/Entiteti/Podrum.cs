using System.Collections.Generic;

namespace WineryApp.Data.Entiteti
{
    public partial class Podrum
    {
        public Podrum()
        {
            PovijestAditiva = new HashSet<PovijestAditiva>();
            Spremnik = new HashSet<Spremnik>();
            Zadatak = new HashSet<Zadatak>();
        }

        public int PodrumId { get; set; }
        public string ŠifraPodruma { get; set; }
        public string Popunjenost { get; set; }
        public string FazaIzrade { get; set; }
        public string Lokacija { get; set; }
        public int SortaVinaId { get; set; }

        public virtual SortaVina SortaVina { get; set; }
        public virtual ICollection<PovijestAditiva> PovijestAditiva { get; set; }
        public virtual ICollection<Spremnik> Spremnik { get; set; }
        public virtual ICollection<Zadatak> Zadatak { get; set; }
    }
}
