using System.Collections.Generic;

namespace WineryApp.Data.Entiteti
{
    public partial class VrstaSpremnika
    {
        public VrstaSpremnika()
        {
            Spremnik = new HashSet<Spremnik>();
        }

        public int VrstaSpremnikaId { get; set; }
        public string NazivVrste { get; set; }
        public string Opis { get; set; }

        public virtual ICollection<Spremnik> Spremnik { get; set; }
    }
}
