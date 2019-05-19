﻿using System;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Podrumi;
using WineryApp.ViewModels.Spremnici;
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

        public Podrum ToPodrum(PodrumIM podrum)
        {
            return new Podrum
            {
                PodrumId = podrum.PodrumId,
                Lokacija = podrum.Lokacija,
                ŠifraPodruma = podrum.ŠifraPodruma
            };
        }

        public PodrumIM ToPodrumIM(Podrum podrum)
        {
            return new PodrumIM
            {
                PodrumId = podrum.PodrumId,
                ŠifraPodruma = podrum.ŠifraPodruma,
                Lokacija = podrum.Lokacija
            };
        }

        public Spremnik ToSpremnik(SpremnikIM spremnik)
        {
            return new Spremnik
            {
                SpremnikId = spremnik.SpremnikId,
                ŠifraSpremnika = spremnik.ŠifraSpremnika,
                PodrumId = spremnik.PodrumId,
                BerbaId = spremnik.BerbaId,
                FazaIzrade = spremnik.FazaIzrade,
                Kapacitet = spremnik.Kapacitet,
                Napunjenost = spremnik.Napunjenost,
                PunilacId = spremnik.PunilacId,
                SortaVinaId = spremnik.SortaVinaId,
                VrstaSpremnikaId = spremnik.VrstaSpremnikaId
            };
        }

        public SpremnikIM ToSpremnikIM(Spremnik spremnik)
        {
            return new SpremnikIM
            {
                SpremnikId = spremnik.SpremnikId,
                PodrumId = spremnik.PodrumId,
                ŠifraSpremnika = spremnik.ŠifraSpremnika,
                Napunjenost = (float) spremnik.Napunjenost,
                SortaVinaId = spremnik.SortaVinaId,
                Kapacitet = (float) spremnik.Kapacitet,
                BerbaId = spremnik.BerbaId,
                FazaIzrade = spremnik.FazaIzrade,
                PunilacId = spremnik.PunilacId,
                VrstaSpremnikaId = spremnik.VrstaSpremnikaId
            };
        }
    }
}
