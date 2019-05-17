using System.Collections.Generic;

namespace WineryApp.Data.Entiteti
{
    public partial class KategorijaZadatka
    {
        public KategorijaZadatka()
        {
            Zadatak = new HashSet<Zadatak>();
        }

        public int KategorijaZadatkaId { get; set; }
        public string ImeKategorije { get; set; }

        public virtual ICollection<Zadatak> Zadatak { get; set; }
    }
}
