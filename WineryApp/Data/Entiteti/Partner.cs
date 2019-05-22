using System.Collections.Generic;

namespace WineryApp.Data.Entiteti
{
    public partial class Partner
    {
        public Partner()
        {
            Narudžba = new HashSet<Narudžba>();
        }

        public int PartnerId { get; set; }
        public string ImePartnera { get; set; }
        public string Oib { get; set; }
        public string KontaktBroj { get; set; }
        public string Adresa { get; set; }

        public virtual ICollection<Narudžba> Narudžba { get; set; }
    }
}
