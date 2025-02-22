﻿using System;

namespace WineryApp.Data.Entiteti
{
    public partial class PovijestAditiva
    {
        public int PovijestAditivaId { get; set; }
        public string ImeZadatka { get; set; }
        public DateTime? Datum { get; set; }
        public decimal? IskorištenaKoličina { get; set; }
        public decimal? PreostalaKoličina { get; set; }
        public int AditivId { get; set; }
        public int? SpremnikId { get; set; }
        public int ZaposlenikId { get; set; }

        public virtual Aditiv Aditiv { get; set; }
        public virtual Spremnik Spremnik { get; set; }
        public virtual Zaposlenik Zaposlenik { get; set; }
    }
}
