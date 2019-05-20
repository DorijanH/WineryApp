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

        public bool AmIAdmin(string korisnickoIme)
        {
            return GetAllZaposlenici()
                       .First(z => z.KorisnickoIme == korisnickoIme).UlogaId == (int)Uloge.Vlasnik;
        }

        public Zaposlenik GetZaposlenik(string korisnickoIme)
        {
            return GetAllZaposlenici()
                .FirstOrDefault(z => z.KorisnickoIme == korisnickoIme);
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
                .ToList();
        }

        public List<Zadatak> GetAllZadaci()
        {
            return _context.Zadatak
                .Include(z => z.KategorijaZadatka)
                .Include(z => z.ZaduženiZaposlenikNavigation)
                .Include(z => z.Podrum)
                .Include(z => z.Spremnik)
                .OrderBy(z => z.ImeZadatka)
                .ToList();
        }

        public void AddPovijestSpremnika(int zadatakId)
        {
            var zadatak = GetZadatak(zadatakId);

            var povijestSpremnika = new PovijestSpremnika
            {
                SpremnikId = zadatak.SpremnikId.Value,
                DatumAkcije = DateTime.Today,
                Akcija = zadatak.KategorijaZadatka.ImeKategorije,
                DetaljiAkcije = zadatak.ImeZadatka,
                Bilješka = zadatak.Bilješke,
                ZaposlenikId = zadatak.ZaduženiZaposlenik
            };

            _context.Add(povijestSpremnika);
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

            MailMessage mm = new MailMessage("WineryAppService@gmail.com", komeSaljem.Email, messageSubject, messageBody);
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
                .Include(p => p.PovijestAditiva)
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
                .Include(s => s.Punilac)
                .Include(s => s.SortaVina)
                .Include(s => s.VrstaSpremnika)
                .Include(s => s.Berba)
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
            
            return vintages.Count == 1 ? vintages[0].ToString() : string.Join(",", vintages);
        }

        public List<string> GetAllVarientals()
        {
            return GetAllSpremnici()
                .Where(s => s.Napunjenost != 0)
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

            return varientals.Count == 1 ? varientals[0] : string.Join(",", varientals);
        }

        public List<RezultatAnalize> GetAllRezultatiAnalize()
        {
            return _context.RezultatAnalize
                .Include(ra => ra.Spremnik)
                .Include(ra => ra.Spremnik.SortaVina)
                .Include(ra => ra.Spremnik.Berba)
                .Include(ra => ra.Spremnik.Podrum)
                .Include(ra => ra.UzorakUzeo)
                .OrderBy(ra => ra.ŠifraUzorka)
                .ToList();
        }

        public RezultatAnalize GetRezultatAnalize(int rezultatId)
        {
            return GetAllRezultatiAnalize()
                .First(ra => ra.RezultatAnalizeId == rezultatId);
        }
    }
}
