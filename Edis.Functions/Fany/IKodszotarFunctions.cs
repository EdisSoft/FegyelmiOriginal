using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.Entities.Fany;
using Edis.Functions.Base;
using Edis.ViewModels.Fany;
using Edis.Entities.Enums;

namespace Edis.Functions.Fany
{
    public interface IKodszotarFunctions : IDbSetBase<Kodszotar>, IFunctionsBase<KodszotarModel, Kodszotar>
    {
        IQueryable<KodszotarModel> GetRendfokozatok();
        IQueryable<KodszotarModel> GetBeosztasok();
        IQueryable<Kodszotar> GetRendfokozatokEntities(bool newContext = false);

      
        IQueryable<KodszotarModel> GetKodszotarakCsoportAlapjan(int csoportId);
        List<KodszotarModel> GetKodszotarakCsoportAlapjan(KodszotarCsoportok csoport);
 
    }
}
