using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Podrumi;
using WineryApp.ViewModels.RezultatiAnalize;
using WineryApp.ViewModels.Spremnici;
using WineryApp.ViewModels.Zadaci;

namespace WineryApp.Data
{
    //Sučelje za mapiranje objekata iz baze podataka u modele
    public interface IMapper
    {
        Zadatak ToZadatak(ZadatakIM zadatak);
        ZadatakIM ToZadatakIM(Zadatak zadatak);

        Podrum ToPodrum(PodrumIM podrum);
        PodrumIM ToPodrumIM(Podrum podrum);

        Spremnik ToSpremnik(SpremnikIM spremnik);
        SpremnikIM ToSpremnikIM(Spremnik spremnik);

        RezultatAnalize ToRezultatAnalize(RezultatAnalizeIM rezultat, IRepository repository);
        RezultatAnalizeIM ToRezultatAnalizeIM(RezultatAnalize rezultat);
    }
}
