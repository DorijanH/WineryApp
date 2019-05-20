using System;

namespace WineryApp.Data.Entiteti
{
    public partial class PovijestSpremnika
    {
        public int PovijestSpremnikaId { get; set; }
        public DateTime? Datum { get; set; }
        public string KategorijaZadatka { get; set; }
        public string ImeZadatka { get; set; }
        public string Bilješka { get; set; }
        public int SpremnikId { get; set; }
        public int ZaposlenikId { get; set; }

        public virtual Spremnik Spremnik { get; set; }
        public virtual Zaposlenik Zaposlenik { get; set; }
    }
}
