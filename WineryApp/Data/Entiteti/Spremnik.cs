using System.Collections.Generic;

namespace WineryApp.Data.Entiteti
{
    public partial class Spremnik
    {
        public Spremnik()
        {
            Zadatak = new HashSet<Zadatak>();
        }

        public int SpremnikId { get; set; }
        public string ŠifraSpremnika { get; set; }
        public string Kapacitet { get; set; }
        public string Napunjenost { get; set; }
        public string GodinaBerbe { get; set; }
        public int VrstaSpremnikaId { get; set; }
        public int? PunilacId { get; set; }
        public int? PodrumId { get; set; }
        public int? SortaVinaId { get; set; }

        public virtual Podrum Podrum { get; set; }
        public virtual Zaposlenik Punilac { get; set; }
        public virtual SortaVina SortaVina { get; set; }
        public virtual VrstaSpremnika VrstaSpremnika { get; set; }
        public virtual ICollection<Zadatak> Zadatak { get; set; }
    }
}
