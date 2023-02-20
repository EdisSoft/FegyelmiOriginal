using Edis.ViewModels;
using Edis.ViewModels.JFK.FENY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.JFK
{
    public interface IFenyitesDashboardFunctions
    {
        //List<FenyitesDashboardListItemViewModel> GetFenyitesDashboardList();
        //List<FenyitesTweetListItemViewModel> GetFenyitesTop5List();
        //void UpdateFenyitesListaByFegyelmiUgyId(int fegyelmiUgyId);

        // Chart metódusok
        //List<FenyitesChartItemViewModel> GetIntezetekForeVonatkozottFegyelmiUgyei();
        List<FenyitesChartItemViewModel> GetIntezetenkentVegrehajtasraVaroFegyelmiUgyek();
        List<FenyitesChartItemViewModel> GetIntezetenkentHetenHataridosUgyekSzama();
        List<FenyitesChartItemViewModel> GetFenyitesTipusokAranyai();
    }
}
