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
    public interface IFeltoltesFunctions : IFunctionsBase<FeltoltesekViewModel, Feltoltesek>
    {
        FeltoltesekViewModel GetFeltoltottFajlById(int fileId);
        void DeleteFile(int fileId);
        List<FeltoltesekViewModel> GetFeltoltottFajlByEsemenyId(int esemenyId);
        List<FeltoltesekViewModel> GetFeltoltottFajloklByNaploId(int naploId);
        List<FeltoltesekViewModel> GetFeltoltottFilesByIds(List<int> naploIds, int esemenyId);
    }
}
