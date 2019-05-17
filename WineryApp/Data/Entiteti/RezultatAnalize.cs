using System;
using System.Collections.Generic;

namespace WineryApp.Data.Entiteti
{
    public partial class RezultatAnalize
    {
        public RezultatAnalize()
        {
            Podrum = new HashSet<Podrum>();
        }

        public int RezultatAnalizeId { get; set; }
        public string ŠifraUzorka { get; set; }
        public DateTime? DatumUzimanjaUzorka { get; set; }
        public byte? StatusRezultata { get; set; }
        public string ŠifraPodruma { get; set; }
        public decimal? PhVrijednost { get; set; }
        public decimal? Šećer { get; set; }
        public decimal? RezidualniŠećer { get; set; }
        public decimal? SlobodniSumpor { get; set; }
        public decimal? UkupniSumpor { get; set; }
        public decimal? Kiselina { get; set; }
        public decimal? PostotakAlkohola { get; set; }
        public int UzorakUzeoId { get; set; }

        public virtual Zaposlenik UzorakUzeo { get; set; }
        public virtual ICollection<Podrum> Podrum { get; set; }
    }
}
