using Edis.Entities.Fany;
using Edis.Functions.Base;
using Edis.ViewModels.Fany;
using System.Collections.Generic;

namespace Edis.Functions.Fany
{
    public interface IFogvatartottFunctions : IDbSetBase<Fogvatartott>, IFunctionsBase<FogvatartottViewModel, Fogvatartott>
    {
        FogvatartottViewModel NytszEllenorzes(string nytsz);
        FogvatartottViewModel GetSzemelyesAdatok(int fogvatartottId);
        FogvatartottViewModel GetSzemelyesAdatokElhelyezessel(int fogvatartottId);
        FogvatartottViewModel GetFogvatartottByNytszAndIntezet(string nytsz, string intezetAzon);


        int GetLastFogvatartottIdByFogvSzemelyId(int fogvSzemelyId);
        List<FogvatartottViewModel> GetFogvatartottakByKorletId(int korletId, params int[] ids);


    }

    public interface IFogvatartottFanyFunctions : IDbSetBase<Fogvatartott>, IFunctionsBase<FogvatartottViewModel, Fogvatartott>
    {
        //IList<FogvatartottReintModel> Kereses(int intezetId);
        //List<IteletListModel> GetFogvatartottIteletei(int fogvatartottId);
       

    }
}
