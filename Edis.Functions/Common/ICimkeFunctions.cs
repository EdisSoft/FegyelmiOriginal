using Edis.Entities.Common;
using Edis.Functions.Base;
using Edis.ViewModels.Common;
using System.Collections.Generic;

namespace Edis.Functions.Common
{
    public interface ICimkeFunctions : IDbSetBase<Cimke>, IFunctionsBase<CimkeModel, Cimke>
    {
        List<CimkeModel> GetCimkekByFelhoId(int felhoId);
        List<Cimke> GetCimkekByFelhoIds(List<int> felhoIds, bool newContext = false);
        List<CimkeModel> GetFegyelmiVetsegTipusok();
        List<CimkeModel> GetFegyelmiVetsegRendeletSzerint();
        List<CimkeModel> GetFegyelmiFenyitesTipusok();
        List<CimkeModel> GetFegyelmiFenyitesMegszuntetesOkai();
        List<CimkeModel> GetFegyelmiFenyitesHosszanakMennyisegiEgysegei();
        List<CimkeModel> GetFegyelmiFenyitesHatalyonKivulHelyezesOkai();
    }
}
