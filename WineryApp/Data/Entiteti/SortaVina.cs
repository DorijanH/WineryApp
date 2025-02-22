﻿using System.Collections.Generic;

namespace WineryApp.Data.Entiteti
{
    public partial class SortaVina
    {
        public SortaVina()
        {
            Spremnik = new HashSet<Spremnik>();
        }

        public int SortaVinaId { get; set; }
        public string NazivSorte { get; set; }

        public virtual ICollection<Spremnik> Spremnik { get; set; }
    }
}
