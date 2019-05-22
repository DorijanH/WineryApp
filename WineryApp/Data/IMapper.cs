using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Aditivi;
using WineryApp.ViewModels.Partneri;
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
        ZadatakIM ToZadatakIM(Zadatak zadatak, IRepository repository);

        Podrum ToPodrum(PodrumIM podrum);
        PodrumIM ToPodrumIM(Podrum podrum);

        Spremnik ToSpremnik(SpremnikIM spremnik);
        SpremnikIM ToSpremnikIM(Spremnik spremnik);

        RezultatAnalize ToRezultatAnalize(RezultatAnalizeIM rezultat, IRepository repository);
        RezultatAnalizeIM ToRezultatAnalizeIM(RezultatAnalize rezultat);

        Aditiv ToAditiv(AditiviIM aditiv);
        AditiviIM ToAditivIM(Aditiv aditiv);

        Partner ToPartner(PartnerIM partner);
        PartnerIM ToPartnerIM(Partner partner);
    }
}
