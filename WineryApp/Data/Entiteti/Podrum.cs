using System.Collections.Generic;

namespace WineryApp.Data.Entiteti
{
    public partial class Podrum
    {
        public Podrum()
        {
            Spremnik = new HashSet<Spremnik>();
            Zadatak = new HashSet<Zadatak>();
        }

        public int PodrumId { get; set; }
        public string ŠifraPodruma { get; set; }
        public string Lokacija { get; set; }

        public virtual ICollection<Spremnik> Spremnik { get; set; }
        public virtual ICollection<Zadatak> Zadatak { get; set; }
    }
}
