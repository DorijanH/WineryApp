using System;

namespace WineryApp.Data.Entiteti
{

    public enum StatusNarudžbe { Naručeno, Isporučeno, Plaćeno }

    public partial class Narudžba
    {
        public int NarudzbaId { get; set; }
        public byte? StatusId { get; set; }
        public DateTime? DatumNarudzbe { get; set; }
        public DateTime? DatumIsporuke { get; set; }
        public DateTime? DatumNaplate { get; set; }
        public string ImeKupca { get; set; }
        public string PrezimeKupca { get; set; }
        public string AdresaKupca { get; set; }
        public decimal? Količina { get; set; }
        public decimal? KonacnaCijena { get; set; }
        public int SpremnikId { get; set; }
        public int? PartnerId { get; set; }

        public virtual Partner Partner { get; set; }
        public virtual Spremnik Spremnik { get; set; }
    }
}
