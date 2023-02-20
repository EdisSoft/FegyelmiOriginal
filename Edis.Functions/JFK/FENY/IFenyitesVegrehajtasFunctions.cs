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
    interface IFenyitesVegrehajtasFunctions : IFunctionsBase<FenyitesVegrehajtasokViewModel, FenyitesVegrehajtasok>
    {
        int GetLetoltottMaganelzarasPercekByFegyelmiUgyId(int fegyelmiUgyId);
    }
}
