using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Zaposlenici;

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

        public bool amIAdmin(string korisnickoIme)
        {
            return GetAllZaposlenici()
                       .FirstOrDefault(z => z.KorisnickoIme == korisnickoIme)
                       .UlogaId == (int) Uloge.Vlasnik;
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
                .ToList();
        }

        public KategorijaZadatka GetKategorijaZadatka(int id)
        {
            return _context.KategorijaZadatka.FirstOrDefault(kz => kz.KategorijaZadatkaId == id);
        }

        public List<KategorijaZadatka> GetAllKategorijeZadataka()
        {
            return _context.KategorijaZadatka
                .Include(kz => kz.Zadatak)
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

        public void DodajZadatakZaposleniku(Zaposlenik noviZadatakZaduženiZaposlenik, Zadatak noviZadatak)
        {
            var zaposlenik = _context.Zaposlenik.Find(noviZadatakZaduženiZaposlenik.ZaposlenikId);

            zaposlenik.Zadatak.Add(noviZadatak);

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
                .Include(p => p.RezultatAnalize)
                .Include(p => p.SortaVina)
                .Include(p => p.Spremnik)
                .ToList();
        }

        public List<Spremnik> GetAllSpremnici()
        {
            return _context.Spremnik
                .Include(s => s.Zadatak)
                .Include(s => s.Podrum)
                .Include(s => s.Punilac)
                .Include(s => s.SortaVina)
                .Include(s => s.VrstaSpremnika)
                .ToList();
        }
    }
}
