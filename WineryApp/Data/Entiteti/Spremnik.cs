using System.Collections.Generic;

namespace WineryApp.Data.Entiteti
{
    public partial class Spremnik
    {
        public Spremnik()
        {
            RezultatAnalize = new HashSet<RezultatAnalize>();
            Zadatak = new HashSet<Zadatak>();
        }

        public int SpremnikId { get; set; }
        public string ŠifraSpremnika { get; set; }
        public string Kapacitet { get; set; }
        public string Napunjenost { get; set; }
        public int VrstaSpremnikaId { get; set; }
        public int? BerbaId { get; set; }
        public int? PunilacId { get; set; }
        public int? PodrumId { get; set; }
        public int? SortaVinaId { get; set; }

        public virtual Berba Berba { get; set; }
        public virtual Podrum Podrum { get; set; }
        public virtual Zaposlenik Punilac { get; set; }
        public virtual SortaVina SortaVina { get; set; }
        public virtual VrstaSpremnika VrstaSpremnika { get; set; }
        public virtual ICollection<RezultatAnalize> RezultatAnalize { get; set; }
        public virtual ICollection<Zadatak> Zadatak { get; set; }
    }
}
