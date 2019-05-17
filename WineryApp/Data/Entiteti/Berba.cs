using System.Collections.Generic;

namespace WineryApp.Data.Entiteti
{
    public partial class Berba
    {
        public Berba()
        {
            PodrumBerba = new HashSet<PodrumBerba>();
        }

        public int BerbaId { get; set; }
        public int GodinaBerbe { get; set; }

        public virtual ICollection<PodrumBerba> PodrumBerba { get; set; }
    }
}
