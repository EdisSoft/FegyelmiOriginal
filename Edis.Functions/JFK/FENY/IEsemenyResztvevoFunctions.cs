using Edis.Entities.Fany;
using Edis.Entities.JFK.FENY;
using Edis.Functions.Base;
using Edis.ViewModels.Fany;
using Edis.ViewModels.JFK.FENY;
using Edis.ViewModels.JFK.Nyomtatvany;
using System.Collections.Generic;

namespace Edis.Functions.JFK
{
    public interface IEsemenyResztvevoFunctions : IFunctionsBase<EsemenyResztvevoViewModel, EsemenyResztvevo>
    {
        List<EsemenyResztvevoViewModel> GetEsemenyResztvevok(int esemenyId);
        List<EsemenyResztvevoPanelViewModel> GetEsemenyResztvevokPanelra(int esemenyId);
        //EsemenyResztvevoAdataiViewModel GetEsemenyResztvevoAdatai(int fogvatartottId, int? esemenyId);
        void DeleteEsemenyResztvevok(int esemenyId);
        //EsemenyResztvevoAdataiViewModel SaveEsemenyResztvevo(EsemenyResztvevoViewModel resztvevo);
        void SaveEsemenyResztvevok(List<int> fogvatartottIds, int erintettsegFokaCimkeId, int esemenyId);
        void DeleteEsemenyResztvevok(List<int> fogvatartottIds, int erintettsegFokaCimkeId, int esemenyId);
        void DeleteEsemenyResztvevo(int esemenyId, int fogvatartottId);                
        List<EsemenyResztvevoAdataiViewModel> GetEsemenyResztvevokByIntezetId(int intezetId);
        bool WarningTanuVagyEszleloByFegyelmiUgyIds(List<int> fegyelmiUgyIds);
        List<string> GetEsemenyResztvetelek(int esemenyId, int fogvatartottId);
    }
}
