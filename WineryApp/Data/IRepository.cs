using System.Collections.Generic;
using WineryApp.Data.Entiteti;

namespace WineryApp.Data
{
    //Sučelje za pristup bazi podataka.
    public interface IRepository
    {
        bool IsThereAdmin();
        bool AmIAdmin(string userHash);
        Zaposlenik GetZaposlenik(string userHash);
        Zaposlenik GetZaposlenik(int id);
        List<Zaposlenik> GetAllZaposlenici();
        List<Zaposlenik> GetAllZaposleniciBezVlasnika();
        Zadatak GetZadatak(int id);
        List<Zadatak> GetAllDanašnjiZadaci();
        List<Zadatak> GetAllZadaci();
        List<Zadatak> GetAllMojiZadaci(Zaposlenik korisnik);
        void AddPovijestSpremnika(int zadatakId);
        void AddPovijestAditiva(int zadatakId, decimal? iskorištenaKoličina);
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
        Berba GetBerba(int id);
        List<Berba> GetAllBerba();
        List<int> GetAllVintages(Podrum podrum);
        string GetAllVingatesFormatted(Podrum podrum);
        List<string> GetAllVarientals(Podrum podrum);
        List<string> GetAllVarientals(Berba berba);
        string GetAllVarientalsFormatted(Podrum podrum);
        List<RezultatAnalize> GetAllRezultatiAnalize();
        RezultatAnalize GetRezultatAnalize(int rezultatId);
        List<PovijestSpremnika> GetAllPovijestiSpremnika();
        PovijestSpremnika GetPovijestSpremnika(int povijestSpremnikaId);
        List<PovijestAditiva> GetAllPovijestiAditiva();
        PovijestAditiva GetPovijestAditiva(int povijestAditivaId);
        List<VrstaAditiva> GetAllVrsteAditiva();
        List<Aditiv> GetAllAditivi();
        List<Aditiv> GetAllAditivi(int vrstaAditivaId);
        Aditiv GetAditiv(int aditivId);
        void UpdateZaposlenik(string userHash, string inputAddress, string inputGender, string inputCity, string inputPhoneNumber, string inputEmail, string inputName, string inputNewPassword, string inputSurename, string userName);
        List<Partner> GetAllPartneri();
        Partner GetPartner(int id);
        List<Narudžba> GetAllNarudžbe();
        Narudžba GetNarudžba(int id);
        string StatusNarudžbe(Narudžba narudžba);
        decimal GetCijenaVina(int spremnikId);
        bool IsporučiNarudžbu(int id);
        void NaplatiNarudžbu(int id);
    }
}
