using Edis.Entities.JFK.FENY;
using Edis.Functions.Base;
using Edis.ViewModels.JFK.FENY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.JFK.FENY
{
    public interface IMaganelzarasFofelugyelokFunctions : IFunctionsBase<MaganelzarasFofelugyelokViewModel, MaganelzarasFofelugyelok>
    {
        MaganelzarasFofelugyelokViewModel GetMaganelzarasFofelugyelokByFegyelmiUgyId(int fegyelmiUgyId);
        MaganelzarasFofelugyelokViewModel GetMaganelzarasFofelugyelokByNaploId(int naploId);
        void SaveMaganelzarasFofelugyelok(List<int> felugyeloIds, int fegyelmiId, int naploId);

    }
}
