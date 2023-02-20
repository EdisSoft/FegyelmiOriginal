using Edis.Diagnostics;
using Edis.Entities.Enums.Kodszotar;
using Edis.Entities.Fany;
using Edis.Functions.Base;
using Edis.Repositories.Contexts;
using Edis.ViewModels.Common;
using Edis.ViewModels.Fany;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace Edis.Functions.Fany
{
    public class FogvatartottFunctions : KonasoftBVFonixFunctionsBase<FogvatartottViewModel, Fogvatartott>, IFogvatartottFunctions
    {
        public DbSet<Fogvatartott> Table => this.KonasoftBVFonixContext.Fogvatartottak;


        public static List<int> VegrehalytasiFokLet = new List<int>() { 2770, 2771, 2772, 2773, 2774, 2768, 2769, 2765, 2766, 2761, 2762 };
        public static List<int> VegrehalytasiFokElit = new List<int>() { 2751, 2752, 2757, 2758, 2750, 2754, 2755 };
        public static List<int> VegrehalytasiFokElz = new List<int>() { 4780, 2763, 1002615 };
        public static List<int> VegrehalytasiFokGyogykezeles = new List<int>() { 2759, 2760 };


        public FogvatartottViewModel NytszEllenorzes(string nytsz)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            var fogv =
                Table.Include(x => x.FogvSzemAdatok).Where(x => x.NyilvantartasiAzonosito == nytsz).FirstOrDefault();
            if (fogv == null) return null;
            return (FogvatartottViewModel)fogv;
        }

        public FogvatartottViewModel GetSzemelyesAdatok(int fogvatartottId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            var fogv = Table.AsNoTracking().Include(x => x.FogvSzemAdatok).SingleOrDefault(x => x.Id == fogvatartottId);
            if (fogv == null)
                throw new Exception(
                    string.Format("FogvatartottFunctions:GetSzemelyesAdatok: nincs ilyen id:" + fogvatartottId));
            else
            {
                return (FogvatartottViewModel)fogv;
            }
        }

        public FogvatartottViewModel GetSzemelyesAdatokElhelyezessel(int fogvatartottId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            var fogv = Table.AsNoTracking().Include(x => x.FogvSzemAdatok)
                .Include(x =>x.IntezetiObjektum)
                .Include(x => x.IntezetiObjektum.Intezet)
                .Include(x => x.Korlet)
                .Include(x => x.Zarka)
                .SingleOrDefault(x => x.Id == fogvatartottId);
            if (fogv == null)
                throw new Exception(
                    string.Format("FogvatartottFunctions:GetSzemelyesAdatok: nincs ilyen id:" + fogvatartottId));
            else
            {
                return (FogvatartottViewModel)fogv;
            }
        }


        public int GetLastFogvatartottIdByFogvSzemelyId(int fogvSzemelyId)
        {
            return KonasoftBVFonixContext.FogvatartottSzemelyesAdatai
                .Where(x => x.FogvatartottSzemelyId == fogvSzemelyId)
                .OrderByDescending(x => x.Id)
                .Select(x => x.FogvatartottId)
                .FirstOrDefault();
        }

        public FogvatartottViewModel GetFogvatartottByNytszAndIntezet(string nytsz, string intezetAzon)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            var fogvatartott =
                Table.Include(x => x.FogvSzemAdatok).Where(
                    x =>
                    (x.NyilvantartasiAzonosito == nytsz || x.AktualisAzonosito == nytsz)
                    && x.NyilvantartoIntezet.Azonosito2 == intezetAzon).FirstOrDefault();
            if (fogvatartott == null) return null;
            return (FogvatartottViewModel)fogvatartott;
        }


        public List<FogvatartottViewModel> GetFogvatartottakByKorletId(int korletId, params int[] ids)
        {
            var result = Table.Where(x => ids.Contains(x.Id) && x.KorletId == korletId)
                .ToList()
                .Select(x => (FogvatartottViewModel)x)
                .ToList();
            return result;
        }

       

    }

}
