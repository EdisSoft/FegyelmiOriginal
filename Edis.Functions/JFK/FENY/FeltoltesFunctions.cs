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
    public class FeltoltesFunctions : KonasoftBVFonixFunctionsBase<FeltoltesekViewModel, Feltoltesek>, IFeltoltesFunctions
    {
        public DbSet<Feltoltesek> Table => KonasoftBVFonixContext.Feltoltesek;
        public FeltoltesekViewModel GetFeltoltottFajlById(int fileId)
        {
            var result = Table.AsQueryable().FirstOrDefault(f => f.Id == fileId && f.TOROLT_FL == false);
            if (result != null)
            {
                return (FeltoltesekViewModel)result;
            }
            return null;
        }

        public void DeleteFile(int fileId)
        {
            var feltoltottFajl = Table.AsQueryable().FirstOrDefault(x => x.Id == fileId);
            feltoltottFajl.TOROLT_FL = true;
            KonasoftBVFonixContext.SaveChanges();
        }

        public List<FeltoltesekViewModel> GetFeltoltottFajlByEsemenyId(int esemenyId)
        {
            var result = Table.AsQueryable().Where(f => f.EsemenyId == esemenyId && f.TOROLT_FL == false).ToList();
            if (result != null)
            {
                return result.Select(x => (FeltoltesekViewModel)x).ToList();
            }
            return new List<FeltoltesekViewModel>();
        }

        public List<FeltoltesekViewModel> GetFeltoltottFajloklByNaploId(int naploId)
        {
            var result = Table.AsQueryable().Where(f => f.NaploId == naploId && f.TOROLT_FL == false).ToList();
            if (result != null)
            {
                return result.Select(x => (FeltoltesekViewModel)x).ToList();
            }
            return new List<FeltoltesekViewModel>();
        }

        public List<FeltoltesekViewModel> GetFeltoltottFilesByIds(List<int> naploIds, int esemenyId)
        {
            List<FeltoltesekViewModel> result = new List<FeltoltesekViewModel>();
            var feltoltottFegyelmiFajl = GetFeltoltottFajlByEsemenyId(esemenyId);

            result.AddRange(feltoltottFegyelmiFajl);
            foreach (var naploId in naploIds)
            {
                var feltoltottNaploFajlok = GetFeltoltottFajloklByNaploId(naploId);
                if (feltoltottNaploFajlok.Count > 0)
                {
                    result.AddRange(feltoltottNaploFajlok);
                }
            }
            return result;
        }
    }
}
