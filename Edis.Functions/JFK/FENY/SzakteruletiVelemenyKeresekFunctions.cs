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
    public class SzakteruletiVelemenyKeresekFunctions : KonasoftBVFonixFunctionsBase<SzakteruletiVelemenyKeresekViewModel, SzakteruletiVelemenyKeresek>, ISzakteruletiVelemenyKeresekFunctions
    {
        public List<int> GetFegyelmiUgyIdsByVelemenykeresId(int velemenykeresId)
        {
            string fegyelmiUgyIdsString = FindById(velemenykeresId).FegyelmiUgyIdLista;
            List<int> fegyelmiUgyIds = fegyelmiUgyIdsString.Split(',').Select(x => int.Parse(x)).ToList();

            return fegyelmiUgyIds;
        }
    }
}
