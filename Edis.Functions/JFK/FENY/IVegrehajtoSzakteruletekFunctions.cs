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
    public interface IVegrehajtoSzakteruletekFunctions : IFunctionsBase<VegrehajtoSzakteruletekViewModel, VegrehajtoSzakteruletek>
    {
        RendszerBeallitasokModel GetRendszerbeallitasokModalData(int intezetId);
        void SaveRendszerbeallitasokModalData(RendszerBeallitasokModel model);
    }
}
