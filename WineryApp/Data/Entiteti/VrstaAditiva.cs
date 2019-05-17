using System.Collections.Generic;

namespace WineryApp.Data.Entiteti
{
    public partial class VrstaAditiva
    {
        public VrstaAditiva()
        {
            Aditiv = new HashSet<Aditiv>();
        }

        public int VrstaAditivaId { get; set; }
        public string NazivVrste { get; set; }
        public string Opis { get; set; }

        public virtual ICollection<Aditiv> Aditiv { get; set; }
    }
}
