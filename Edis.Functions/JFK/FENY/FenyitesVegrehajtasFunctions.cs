using Edis.Entities.JFK.FENY;
using Edis.Functions.Base;
using Edis.ViewModels.JFK.FENY;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.JFK.FENY
{
    public class FenyitesVegrehajtasFunctions : KonasoftBVFonixFunctionsBase<FenyitesVegrehajtasokViewModel, FenyitesVegrehajtasok>, IFenyitesVegrehajtasFunctions
    {
        public DbSet<FenyitesVegrehajtasok> Table => KonasoftBVFonixContext.FenyitesVegrehajtasok;
        
        public int GetLetoltottMaganelzarasPercekByFegyelmiUgyId(int fegyelmiUgyId)
        {
            var elzarasok = Table
                .Where(x => x.FegyelmiUgyId == fegyelmiUgyId && x.VegeIdo != null)
                .ToList();
            var osszPerc = 0;
            foreach (var elzaras in elzarasok)
            {
                osszPerc += (int)(elzaras.VegeIdo.Value - elzaras.KezdeteIdo.Value).TotalMinutes; 
            }

            return osszPerc;
        }

    }
}
