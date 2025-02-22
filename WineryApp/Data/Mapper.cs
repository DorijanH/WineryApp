﻿using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Aditivi;
using WineryApp.ViewModels.Narudžbe;
using WineryApp.ViewModels.Partneri;
using WineryApp.ViewModels.Podrumi;
using WineryApp.ViewModels.RezultatiAnalize;
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
                AditivId = zadatak.AditivId,
                PodrumId = zadatak.PodrumId == -1 ? new int?() : zadatak.PodrumId,
                SpremnikId = zadatak.SpremnikId == -1 ? new int?() : zadatak.SpremnikId,
                KategorijaZadatkaId = zadatak.KategorijaZadatkaId,
                Bilješke = zadatak.Bilješke,
                StatusZadatka = zadatak.StatusZadatka,
                ZaduženiZaposlenik = zadatak.ZaposlenikId
            };
        }

        public ZadatakIM ToZadatakIM(Zadatak zadatak, IRepository repository)
        {
            return new ZadatakIM
            {
                ZadatakId = zadatak.ZadatakId,
                PodrumId = zadatak.PodrumId,
                AditivId = zadatak.AditivId,
                VrstaAditivaId = zadatak.AditivId.HasValue ? repository.GetAditiv(zadatak.AditivId.Value).VrstaAditivaId : new int?(),
                SpremnikId = zadatak.SpremnikId,
                PočetakZadatka = zadatak.PočetakZadatka,
                RokZadatka = zadatak.RokZadatka,
                ImeZadatka = zadatak.ImeZadatka,
                KategorijaZadatkaId = zadatak.KategorijaZadatkaId,
                Bilješke = zadatak.Bilješke,
                ZaposlenikId = zadatak.ZaduženiZaposlenik,
                StatusZadatka = zadatak.StatusZadatka
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
                CijenaLitre = spremnik.CijenaLitre,
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
                CijenaLitre = spremnik.CijenaLitre,
                BerbaId = spremnik.BerbaId,
                FazaIzrade = spremnik.FazaIzrade,
                PunilacId = spremnik.PunilacId,
                VrstaSpremnikaId = spremnik.VrstaSpremnikaId
            };
        }

        public RezultatAnalize ToRezultatAnalize(RezultatAnalizeIM rezultat, IRepository repository)
        {
            return new RezultatAnalize
            {
                RezultatAnalizeId = rezultat.RezultatAnalizeId,
                DatumUzimanjaUzorka = rezultat.DatumUzorka.Date,
                Kiselina = rezultat.Kiselina,
                PhVrijednost = rezultat.PhVrijednost,
                PostotakAlkohola = rezultat.PostotakAlkohola,
                ŠifraUzorka = rezultat.ŠifraUzorka,
                Šećer = rezultat.Šećer,
                RezidualniŠećer = rezultat.RezidualniŠećer,
                SlobodniSumpor = rezultat.SlobodniSumpor,
                SpremnikId = rezultat.SpremnikId,
                UkupniSumpor = rezultat.UkupniSumpor,
                UzorakUzeoId = rezultat.UzorakUzeoId
            };
        }

        public RezultatAnalizeIM ToRezultatAnalizeIM(RezultatAnalize rezultat)
        {
            return new RezultatAnalizeIM
            {
                RezultatAnalizeId = rezultat.RezultatAnalizeId,
                Kiselina = rezultat.Kiselina.Value,
                DatumUzorka = rezultat.DatumUzimanjaUzorka.Date,
                UkupniSumpor = rezultat.UkupniSumpor.Value,
                UzorakUzeoId = rezultat.UzorakUzeoId,
                SpremnikId = rezultat.SpremnikId,
                Šećer = rezultat.Šećer.Value,
                PhVrijednost = rezultat.PhVrijednost.Value,
                ŠifraUzorka = rezultat.ŠifraUzorka,
                RezidualniŠećer = rezultat.RezidualniŠećer.Value,
                SlobodniSumpor = rezultat.SlobodniSumpor.Value,
                PostotakAlkohola = rezultat.PostotakAlkohola.Value
            };
        }

        public Aditiv ToAditiv(AditiviIM aditiv)
        {
            return new Aditiv
            {
                AditivId = aditiv.AditivId,
                VrstaAditivaId = aditiv.VrstaAditivaId,
                ImeAditiva = aditiv.ImeAditiva,
                Instrukcije = aditiv.Instrukcije,
                Količina = aditiv.Količina,
                Koncentracija = aditiv.Koncentracija
            };
        }

        public AditiviIM ToAditivIM(Aditiv aditiv)
        {
            return new AditiviIM
            {
                AditivId = aditiv.AditivId,
                VrstaAditivaId = aditiv.VrstaAditivaId,
                ImeAditiva = aditiv.ImeAditiva,
                Instrukcije = aditiv.Instrukcije,
                Količina = aditiv.Količina,
                Koncentracija = aditiv.Koncentracija
            };
        }

        public Partner ToPartner(PartnerIM partner)
        {
            return new Partner
            {
                PartnerId = partner.PartnerId,
                ImePartnera = partner.ImePartnera,
                Adresa = partner.Adresa,
                KontaktBroj = partner.KontaktBroj,
                Oib = partner.OIB
            };
        }

        public PartnerIM ToPartnerIM(Partner partner)
        {
            return new PartnerIM
            {
                PartnerId = partner.PartnerId,
                ImePartnera = partner.ImePartnera,
                OIB = partner.Oib,
                KontaktBroj = partner.KontaktBroj,
                Adresa = partner.Adresa
            };
        }

        public Narudžba ToNarudžba(NarudžbaIM narudžba)
        {
            return new Narudžba
            {
                NarudzbaId = narudžba.NarudžbaId,
                PartnerId = narudžba.PartnerId,
                DatumIsporuke = narudžba.DatumIsporuke,
                DatumNaplate = narudžba.DatumNaplate,
                DatumNarudzbe = narudžba.DatumNarudzbe,
                ImeKupca = narudžba.ImeKupca,
                PrezimeKupca = narudžba.PrezimeKupca,
                AdresaKupca = narudžba.AdresaKupca,
                Količina = narudžba.Količina,
                KonacnaCijena = narudžba.KonacnaCijena,
                SpremnikId = narudžba.SpremnikId,
                Status = narudžba.Status
            };
        }

        public NarudžbaIM ToNarudžbaIM(Narudžba narudžba)
        {
            return new NarudžbaIM
            {
                NarudžbaId = narudžba.NarudzbaId,
                PartnerId = narudžba.PartnerId,
                ImeKupca = narudžba.ImeKupca,
                PrezimeKupca = narudžba.PrezimeKupca,
                AdresaKupca = narudžba.AdresaKupca,
                Količina = narudžba.Količina,
                KonacnaCijena = narudžba.KonacnaCijena,
                SpremnikId = narudžba.SpremnikId,
                PodrumId = narudžba.Spremnik.PodrumId,
                DatumNarudzbe = narudžba.DatumNarudzbe,
                DatumNaplate = narudžba.DatumNaplate,
                DatumIsporuke = narudžba.DatumIsporuke,
                Status = narudžba.Status
            };
        }
    }
}
