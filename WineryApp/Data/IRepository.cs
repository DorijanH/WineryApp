using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Abstractions.Internal;
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
        bool ProvjeraEmailAdrese(string Email);
        List<Podrum> GetAllPodrumi();
        Podrum GetPodrum(int id);
        List<Spremnik> GetAllSpremnici();
        List<Spremnik> GetAllSpremnici(int podrumId);
        List<Spremnik> GetAllSpremnikWithVintage(int vintage);
        Spremnik GetSpremnik(int id);
        List<VrstaSpremnika> GetAllVrsteSpremnika();
        string GetBasementFill(int podrumId);
        string GetSpremnikFill(int spremnikId);
        List<SortaVina> GetAllSorteVina();
        bool IsThereBerba();
        List<Berba> GetAllBerba();
        List<int> GetAllVintages(Podrum podrum);
        string GetAllVingatesFormatted(Podrum podrum);
        List<string> GetAllVarientals(Podrum podrum);
        string GetAllVarientalsFormatted(Podrum podrum);
        List<RezultatAnalize> GetAllRezultatiAnalize();
        RezultatAnalize GetRezultatAnalize(int rezultatId);
    }
}
