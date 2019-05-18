using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Zadaci;

namespace WineryApp.Data
{
    //Sučelje za mapiranje objekata iz baze podataka u modele
    public interface IMapper
    {
        Zadatak ToZadatak(ZadatakIM zadatak);
        ZadatakIM ToZadatakIM(Zadatak zadatak);
    }
}
