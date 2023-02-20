using Edis.Entities.Common;
using Edis.Functions.Base;
using Edis.Repositories.Contexts;
using Edis.ViewModels.Common;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Edis.Functions.Common
{
    public class CimkeFunctions : KonasoftBVFonixFunctionsBase<CimkeModel, Cimke>, ICimkeFunctions
    {
        public DbSet<Cimke> Table
        {
            get { return this.KonasoftBVFonixContext.Cimkek; }
        }

        public List<CimkeModel> GetCimkekByFelhoId(int felhoId)
        {
            List<CimkeModel> results = Table.Where(w => w.FelhoId == felhoId).ToList().Select(x => (CimkeModel)x).ToList();
            return results;
        }

        public List<Cimke> GetCimkekByFelhoIds(List<int> felhoIds, bool newContext = false)
        {
            List<Cimke> results;
            if (newContext == true)
            {
                results = new KonasoftBVFonixContext().Cimkek.Where(w => felhoIds.Contains(w.FelhoId)).ToList().Select(x => x).ToList();
            }
            else
            {
                results = Table.Where(w => felhoIds.Contains(w.FelhoId)).ToList().Select(x => x).ToList();
            }

            return results;
        }

        public List<CimkeModel> GetFegyelmiVetsegTipusok()
        {
            return Table.Where(w => w.FelhoId == 15).ToList().Select(s => (CimkeModel)s).ToList();
        }

        public List<CimkeModel> GetFegyelmiVetsegRendeletSzerint()
        {
            return Table.Where(w => w.FelhoId == 22).ToList().Select(s => (CimkeModel)s).ToList();
        }
        
        public List<CimkeModel> GetFegyelmiFenyitesTipusok()
        {
            return Table.Where(w => w.FelhoId == 14).ToList().Select(s => (CimkeModel)s).ToList();
        }

        public List<CimkeModel> GetFegyelmiFenyitesMegszuntetesOkai()
        {
            return Table.Where(w => w.FelhoId == 20).ToList().Select(s => (CimkeModel)s).ToList();
        }

        public List<CimkeModel> GetFegyelmiFenyitesHatalyonKivulHelyezesOkai()
        {
            return Table.Where(w => w.FelhoId == 28).ToList().Select(s => (CimkeModel)s).ToList();
        }

        public List<CimkeModel> GetFegyelmiFenyitesHosszanakMennyisegiEgysegei()
        {
            return Table.Where(w => w.FelhoId == 25).ToList().Select(s => (CimkeModel)s).ToList();
        }
    }
}
