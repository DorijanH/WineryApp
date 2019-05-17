using System.Collections.Generic;

namespace WineryApp.Data.Entiteti
{
    public enum Uloge { Vlasnik = 1, Zaposlenik }

    public partial class Uloga
    {
        public Uloga()
        {
            Zaposlenik = new HashSet<Zaposlenik>();
        }

        public int UlogaId { get; set; }
        public string NazivUloga { get; set; }

        public virtual ICollection<Zaposlenik> Zaposlenik { get; set; }
    }
}
