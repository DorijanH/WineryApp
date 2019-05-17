using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineryApp.Data.Entiteti;

namespace WineryApp.Data
{
    public interface IRepository
    {
        bool IsThereAdmin();
        bool AmIAdmin(string korisnickoIme);
        Zaposlenik GetZaposlenik(string korisnickoIme);
        Zaposlenik GetZaposlenik(int id);
        List<Zaposlenik> GetAllZaposlenici();
        Zadatak GetZadatak(int id);
        List<Zadatak> GetAllDanašnjiZadaci();
        List<Zadatak> GetAllZadaci();
        KategorijaZadatka GetKategorijaZadatka(int id);
        List<KategorijaZadatka> GetAllKategorijeZadataka();

        void DodajZadatak(Zadatak noviZadatak);
        void SendEmail(Zaposlenik komeSaljem, string messageSubject, string messageBody);
        void DodajZaposlenika(Zaposlenik noviZaposlenik);
        void DodajZadatakZaposleniku(Zaposlenik noviZadatakZaduženiZaposlenik, Zadatak noviZadatak);

        bool ProvjeraEmailAdrese(string Email);

        List<Podrum> GetAllPodrumi();

        List<Spremnik> GetAllSpremnici();

        bool IsThereBerba();
    }
}
