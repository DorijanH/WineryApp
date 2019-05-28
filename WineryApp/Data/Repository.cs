using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using WineryApp.Data.Entiteti;

namespace WineryApp.Data
{
    public class Repository : IRepository
    {
        private readonly WineryAppDbContext _context;

        public Repository(WineryAppDbContext context)
        {
            _context = context;
        }

        public bool IsThereAdmin()
        {
            return _context.Zaposlenik.Any(z => z.UlogaId == 1);
        }

        public bool AmIAdmin(string userHash)
        {
            return GetAllZaposlenici()
                       .First(z => z.User.Id == userHash).UlogaId == (int) Uloge.Vlasnik;
        }

        public Zaposlenik GetZaposlenik(string userHash)
        {
            return GetAllZaposlenici()
                .FirstOrDefault(z => z.User.Id == userHash);
        }

        public Zaposlenik GetZaposlenikByUserId(string id)
        {
            return GetAllZaposlenici()
                .FirstOrDefault(z => z.User.Id == id);
        }

        public Zaposlenik GetZaposlenik(int id)
        {
            return GetAllZaposlenici()
                .FirstOrDefault(z => z.ZaposlenikId == id);
        }

        public List<Zaposlenik> GetAllZaposlenici()
        {
            return _context.Zaposlenik
                .Include(z => z.Uloga)
                .Include(z => z.PovijestAditiva)
                .Include(z => z.PovijestSpremnika)
                .Include(z => z.RezultatAnalize)
                .Include(z => z.Zadatak)
                .Include(z => z.User)
                .OrderBy(z => z.Prezime)
                .ToList();
        }

        public List<Zaposlenik> GetAllZaposleniciBezVlasnika()
        {
            return GetAllZaposlenici()
                .Where(z => z.UlogaId == (int) Uloge.Zaposlenik)
                .ToList();
        }

        public Zadatak GetZadatak(int id)
        {
            return GetAllZadaci()
                .FirstOrDefault(z => z.ZadatakId == id);
        }

        public List<Zadatak> GetAllDanašnjiZadaci()
        {
            var današnjiDatum = DateTime.Today;

            return GetAllZadaci()
                .Where(z => (z.PočetakZadatka <= današnjiDatum) && (z.RokZadatka >= današnjiDatum))
                .Where(z => z.StatusZadatka == (int) StatusZadatka.UTijeku)
                .ToList();
        }

        public List<Zadatak> GetAllZadaci()
        {
            return _context.Zadatak
                .Include(z => z.KategorijaZadatka)
                .Include(z => z.ZaduženiZaposlenikNavigation)
                .Include(z => z.Podrum)
                .Include(z => z.Spremnik)
                .Include(z => z.Aditiv)
                .OrderBy(z => z.StatusZadatka)
                .ToList();
        }

        public List<Zadatak> GetAllMojiZadaci(Zaposlenik korisnik)
        {
            return GetAllZadaci()
                .Where(z => z.ZaduženiZaposlenikNavigation == korisnik)
                .Where(z => z.StatusZadatka == (int) StatusZadatka.UTijeku)
                .ToList();
        }

        public void AddPovijestSpremnika(int zadatakId)
        {
            var zadatak = GetZadatak(zadatakId);

            var povijestSpremnika = new PovijestSpremnika
            {
                SpremnikId = zadatak.SpremnikId.Value,
                Datum = DateTime.Now,
                KategorijaZadatka = zadatak.KategorijaZadatka.ImeKategorije,
                ImeZadatka = zadatak.ImeZadatka,
                Bilješka = zadatak.Bilješke,
                ZaposlenikId = zadatak.ZaduženiZaposlenik
            };

            _context.Add(povijestSpremnika);
            _context.SaveChanges();
        }

        public void AddPovijestAditiva(int zadatakId, decimal? iskorištenaKoličina)
        {
            var zadatak = GetZadatak(zadatakId);

            var aditiv = GetAditiv(zadatak.AditivId.Value);

            var povijestAditiva = new PovijestAditiva
            {
                AditivId = zadatak.AditivId.Value,
                IskorištenaKoličina = iskorištenaKoličina,
                PreostalaKoličina = iskorištenaKoličina.HasValue
                    ? aditiv.Količina - iskorištenaKoličina
                    : aditiv.Količina,
                Datum = DateTime.Now,
                ImeZadatka = zadatak.ImeZadatka,
                ZaposlenikId = zadatak.ZaduženiZaposlenik,
                SpremnikId = zadatak.SpremnikId
            };

            UpdateKolicinuAditiva(aditiv, povijestAditiva.PreostalaKoličina);

            _context.Add(povijestAditiva);
            _context.SaveChanges();
        }

        private void UpdateKolicinuAditiva(Aditiv aditiv, decimal? PreostalaKoličina)
        {
            var ad = GetAditiv(aditiv.AditivId);
            ad.Količina = PreostalaKoličina;

            _context.Update(ad);
            _context.SaveChanges();
        }

        public KategorijaZadatka GetKategorijaZadatka(int id)
        {
            return GetAllKategorijeZadataka()
                .FirstOrDefault(kz => kz.KategorijaZadatkaId == id);
        }

        public List<KategorijaZadatka> GetAllKategorijeZadataka()
        {
            return _context.KategorijaZadatka
                .Include(kz => kz.Zadatak)
                .OrderBy(kz => kz.ImeKategorije)
                .ToList();
        }

        public void DodajZadatak(Zadatak noviZadatak)
        {
            _context.Zadatak.Add(noviZadatak);
            _context.SaveChanges();
        }

        public void SendEmail(Zaposlenik komeSaljem, string messageSubject, string messageBody)
        {
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("wineryappservice@gmail.com", "dorijan101")
            };

            MailMessage mm = new MailMessage("WineryAppService@gmail.com", komeSaljem.Email, messageSubject,
                messageBody);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }

        public void DodajZaposlenika(Zaposlenik noviZaposlenik)
        {
            _context.Zaposlenik.Add(noviZaposlenik);
            _context.SaveChanges();
        }

        public bool ProvjeraEmailAdrese(string Email)
        {
            return GetAllZaposlenici().All(z => z.Email != Email);
        }

        public List<Podrum> GetAllPodrumi()
        {
            return _context.Podrum
                .Include(p => p.Zadatak)
                .Include(p => p.Spremnik)
                .OrderBy(p => p.ŠifraPodruma)
                .ToList();
        }

        public Podrum GetPodrum(int id)
        {
            return GetAllPodrumi().First(p => p.PodrumId == id);
        }

        public List<Spremnik> GetAllSpremnici()
        {
            return _context.Spremnik
                .Include(s => s.Zadatak)
                .Include(s => s.RezultatAnalize)
                .Include(s => s.Podrum)
                .Include(s => s.Narudžba)
                .Include(s => s.PovijestAditiva)
                .Include(s => s.Punilac)
                .Include(s => s.SortaVina)
                .Include(s => s.VrstaSpremnika)
                .Include(s => s.Berba)
                .Include(s => s.PovijestSpremnika)
                .OrderBy(s => s.ŠifraSpremnika)
                .ToList();
        }

        public List<Spremnik> GetAllSpremnici(int podrumId)
        {
            return GetAllSpremnici()
                .Where(s => s.PodrumId == podrumId)
                .ToList();
        }

        public List<Spremnik> GetAllSpremnikWithVintage(int vintage)
        {
            return GetAllSpremnici().Where(s => s.Berba.BerbaId == vintage).ToList();
        }

        public Spremnik GetSpremnik(int id)
        {
            return GetAllSpremnici().First(s => s.SpremnikId == id);
        }

        public List<VrstaSpremnika> GetAllVrsteSpremnika()
        {
            return _context.VrstaSpremnika
                .Include(vs => vs.Spremnik)
                .OrderBy(vs => vs.NazivVrste)
                .ToList();
        }

        public string GetBasementFill(int podrumId)
        {
            return GetAllSpremnici(podrumId).Sum(s => s.Napunjenost).ToString("F1") + " L";
        }

        public string GetSpremnikFill(int spremnikId)
        {
            var spremnik = GetSpremnik(spremnikId);

            return $"{spremnik.Napunjenost} / {spremnik.Kapacitet} L";
        }

        public List<SortaVina> GetAllSorteVina()
        {
            return _context.SortaVina
                .Include(sv => sv.Spremnik)
                .OrderBy(sv => sv.NazivSorte)
                .ToList();
        }

        public bool IsThereBerba()
        {
            return _context.Berba.Any();
        }

        public Berba GetBerba(int id)
        {
            return GetAllBerba()
                .First(b => b.BerbaId == id);
        }

        public List<Berba> GetAllBerba()
        {
            return _context.Berba
                .Include(b => b.Spremnik)
                .OrderBy(b => b.GodinaBerbe)
                .ToList();
        }

        public List<int> GetAllVintages(Podrum podrum)
        {
            return GetAllSpremnici(podrum.PodrumId)
                .Where(s => s.Napunjenost != 0)
                .Select(s => s.Berba.GodinaBerbe).Distinct().ToList();
        }

        public List<int> GetAllVintages()
        {
            return GetAllSpremnici()
                .Where(s => s.Napunjenost != 0)
                .Select(s => s.Berba.GodinaBerbe).Distinct().ToList();
        }

        public string GetAllVingatesFormatted(Podrum podrum)
        {
            var vintages = GetAllVintages(podrum);

            return vintages.Count == 1 ? vintages[0].ToString() : string.Join("\n", vintages);
        }

        public List<string> GetAllVarientals(Berba berba)
        {
            return GetAllSpremnici()
                .Where(s => s.Napunjenost != 0)
                .Where(s => s.BerbaId.Value == berba.BerbaId)
                .Select(s => s.SortaVina.NazivSorte).Distinct().ToList();
        }

        public List<string> GetAllVarientals(Podrum podrum)
        {
            return GetAllSpremnici(podrum.PodrumId)
                .Where(s => s.Napunjenost != 0)
                .Select(s => s.SortaVina.NazivSorte).Distinct().ToList();
        }

        public string GetAllVarientalsFormatted(Podrum podrum)
        {
            var varientals = GetAllVarientals(podrum);

            return varientals.Count == 1 ? varientals[0] : string.Join("\n", varientals);
        }

        public List<RezultatAnalize> GetAllRezultatiAnalize()
        {
            return _context.RezultatAnalize
                .Include(ra => ra.Spremnik)
                .Include(ra => ra.Spremnik.SortaVina)
                .Include(ra => ra.Spremnik.Berba)
                .Include(ra => ra.Spremnik.Podrum)
                .Include(ra => ra.UzorakUzeo)
                .OrderByDescending(ra => ra.DatumUzimanjaUzorka)
                .ToList();
        }

        public RezultatAnalize GetRezultatAnalize(int rezultatId)
        {
            return GetAllRezultatiAnalize()
                .First(ra => ra.RezultatAnalizeId == rezultatId);
        }

        public List<PovijestSpremnika> GetAllPovijestiSpremnika()
        {
            return _context.PovijestSpremnika
                .Include(ps => ps.Spremnik)
                .Include(ps => ps.Zaposlenik)
                .OrderByDescending(ps => ps.Datum)
                .ToList();
        }

        public PovijestSpremnika GetPovijestSpremnika(int povijestSpremnikaId)
        {
            return GetAllPovijestiSpremnika()
                .First(ps => ps.PovijestSpremnikaId == povijestSpremnikaId);
        }

        public List<PovijestAditiva> GetAllPovijestiAditiva()
        {
            return _context.PovijestAditiva
                .Include(pa => pa.Aditiv)
                .Include(pa => pa.Spremnik)
                .Include(pa => pa.Zaposlenik)
                .OrderByDescending(pa => pa.Datum)
                .ToList();
        }

        public PovijestAditiva GetPovijestAditiva(int povijestAditivaId)
        {
            return GetAllPovijestiAditiva()
                .First(pa => pa.PovijestAditivaId == povijestAditivaId);
        }

        public List<VrstaAditiva> GetAllVrsteAditiva()
        {
            return _context.VrstaAditiva
                .Include(va => va.Aditiv)
                .OrderBy(va => va.NazivVrste)
                .ToList();
        }

        public List<Aditiv> GetAllAditivi()
        {
            return _context.Aditiv
                .Include(a => a.VrstaAditiva)
                .Include(a => a.PovijestAditiva)
                .OrderBy(a => a.ImeAditiva)
                .ToList();
        }

        public List<Aditiv> GetAllAditivi(int vrstaAditivaId)
        {
            return GetAllAditivi()
                .Where(a => a.VrstaAditivaId == vrstaAditivaId)
                .ToList();
        }

        public Aditiv GetAditiv(int aditivId)
        {
            return GetAllAditivi()
                .First(a => a.AditivId == aditivId);
        }

        public void UpdateZaposlenik(string userHash, string inputAddress, string inputGender, string inputCity,
            string inputPhoneNumber, string inputEmail, string inputName, string inputNewPassword, string inputSurename)
        {
            var zaposlenik = GetZaposlenik(userHash);

            zaposlenik.Adresa = inputAddress;
            zaposlenik.Spol = inputGender;
            zaposlenik.Grad = inputCity;
            zaposlenik.Telefon = inputPhoneNumber;
            zaposlenik.Email = string.IsNullOrWhiteSpace(inputEmail) ? zaposlenik.Email : inputEmail;
            zaposlenik.Ime = inputName;
            zaposlenik.Prezime = inputSurename;
            zaposlenik.Lozinka = string.IsNullOrWhiteSpace(inputNewPassword) ? zaposlenik.Lozinka : inputNewPassword;
            _context.SaveChanges();
        }

        public List<Partner> GetAllPartneri()
        {
            return _context.Partner
                .Include(p => p.Narudžba)
                .OrderBy(p => p.ImePartnera)
                .ToList();
        }

        public Partner GetPartner(int id)
        {
            return GetAllPartneri()
                .First(p => p.PartnerId == id);
        }

        public List<Narudžba> GetAllNarudžbe()
        {
            return _context.Narudžba
                .Include(n => n.Partner)
                .Include(n => n.Spremnik)
                .Include(n => n.Spremnik.SortaVina)
                .Include(n => n.Spremnik.Podrum)
                .OrderByDescending(n => n.DatumNarudzbe)
                .ToList();
        }

        public Narudžba GetNarudžba(int id)
        {
            return GetAllNarudžbe()
                .First(n => n.NarudzbaId == id);
        }

        public string StatusNarudžbe(Narudžba narudžba)
        {
            switch (narudžba.Status)
            {
                case (int) Entiteti.StatusNarudžbe.Isporučeno:
                    return "Isporučeno";
                case (int) Entiteti.StatusNarudžbe.Naručeno:
                    return "Naručeno";
                case (int)Entiteti.StatusNarudžbe.Plaćeno:
                    return "Plaćeno";
                default:
                    throw new Exception("Status narudžbe ne postoji!");
            }
        }

        public decimal GetCijenaVina(int spremnikId)
        {
            var spremnik = GetSpremnik(spremnikId);

            return spremnik.CijenaLitre ?? 0;
        }

        public bool IsporučiNarudžbu(int id)
        {
            var narudžba = GetNarudžba(id);
            var spremnik = GetSpremnik(narudžba.SpremnikId);

            var rezultat = spremnik.Napunjenost - (double)narudžba.Količina;

            if (rezultat < 0)
            {
                return false;
            }

            spremnik.Napunjenost -= (double)narudžba.Količina;
            narudžba.DatumIsporuke = DateTime.Today;
            narudžba.Status = (int)Entiteti.StatusNarudžbe.Isporučeno;

            _context.SaveChanges();
            return true;
        }

        public void NaplatiNarudžbu(int id)
        {
            var narudžba = GetNarudžba(id);

            narudžba.DatumNaplate = DateTime.Today;
            narudžba.Status = (int) Entiteti.StatusNarudžbe.Plaćeno;
            _context.SaveChanges();
        }
    }
}