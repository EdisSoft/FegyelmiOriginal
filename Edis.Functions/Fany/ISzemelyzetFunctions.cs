using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.Entities.Fany;
using Edis.Functions.Base;
using Edis.ViewModels.Fany;
using Edis.Entities.Common;
using Edis.ViewModels.JFK.FENY.FormModel;

namespace Edis.Functions.Fany
{
    public interface ISzemelyzetFunctions : IDbSetBase<Szemelyzet>, IFunctionsBase<SzemelyzetModel,Szemelyzet>
    {
        Szemelyzet FindBySid(string sid, int? intezetId);
        Szemelyzet KeresesById(int id);

        void Uj(Szemelyzet szemelyzet);

        void Modosit(Szemelyzet szemelyzet);

        Szemelyzet KeresesByAzonosito(string azonosito);

       
        
        List<AdFegyelmiUserItem> GetFegyelmiAlkalmazottak();
        List<AdFegyelmiUserItem> GetFegyelmiEgyebSzakteruletAlkalmazottak();
        List<AdFegyelmiUserItem> GetFegyelmiReintegraciosTisztiAlkalmazottak();
        List<AdFegyelmiUserItem> GetFegyelmiJogkorGyakorlojaAlkalmazottak();
        List<AdFegyelmiUserItem> GetFegyelmiEszlelok();
        List<AdFegyelmiUserItem> GetIntezetiAlkalmazottak();
        List<AdFegyelmiSzakvezetoUserItem> GetFegyelmiSzakteruletiVezetok(string term);
        Szemelyzet SzemelyzetLekeresVagyLetrehozas(string sidStr, string userName, int? intezetId = null);

        Szemelyzet SzemelyzetLekeresVagyLetrehozas(string sidStr, string userName, bool frissitesAdAlapjan, int? intezetId, out bool ujLetrehozas);
        List<SzemelyzetModel> GetAllSzemelyzet(int intezetId);
        AdFegyelmiUserItem GetAdFegyelmiUser(SzemelyzetModel szemelyzet);
        List<AdFegyelmiUserItem> GetFegyelmiFofelugyelokAlkalmazottak();

        Szemelyzet SzemelyzetLekeresVagyLetrehozasNevvel(string sidStr, string nev, int? intezetId = null);

        IQueryable<Szemelyzet> GetSzemelyzetTagokBySid(string[] sids, int? intezetId);
        void ResetContext();

    }
}
