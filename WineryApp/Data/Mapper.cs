using System;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Zadaci;

namespace WineryApp.Data
{
    public class Mapper : IMapper
    {
        public Zadatak ToZadatak(ZadatakIM zadatak)
        {
            return new Zadatak
            {
                ImeZadatka = zadatak.ImeZadatka,
                PočetakZadatka = zadatak.PočetakZadatka,
                RokZadatka = zadatak.RokZadatka,
                ZadatakId = zadatak.ZadatakId,
                PodrumId = zadatak.PodrumId == -1 ? new int?() : zadatak.PodrumId,
                SpremnikId = zadatak.SpremnikId == -1 ? new int?() : zadatak.SpremnikId,
                KategorijaZadatkaId = zadatak.KategorijaZadatkaId,
                Bilješke = zadatak.Bilješke,
                StatusZadatka = zadatak.StatusZadatka,
                ZaduženiZaposlenik = zadatak.ZaposlenikId
            };
        }

        public ZadatakIM ToZadatakIM(Zadatak zadatak)
        {
            return new ZadatakIM
            {
                ZadatakId = zadatak.ZadatakId,
                PodrumId = zadatak.PodrumId,
                SpremnikId = zadatak.SpremnikId,
                PočetakZadatka = zadatak.PočetakZadatka,
                RokZadatka = zadatak.RokZadatka,
                ImeZadatka = zadatak.ImeZadatka,
                KategorijaZadatkaId = zadatak.KategorijaZadatkaId,
                Bilješke = zadatak.Bilješke,
                ZaposlenikId = zadatak.ZaduženiZaposlenik
            };
        }
    }
}
