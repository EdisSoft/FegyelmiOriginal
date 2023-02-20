using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.Entities.Fany;
using Edis.Functions.Base;
using Edis.ViewModels.Fany;
using Edis.Entities.Enums.Kodszotar;
using Edis.Entities.Enums;
using Edis.Diagnostics;
using Edis.Repositories.Contexts;

namespace Edis.Functions.Fany
{

    public class KodszotarFunctions : KonasoftBVFonixFunctionsBase<KodszotarModel, Kodszotar>, IKodszotarFunctions
    {
        public DbSet<Kodszotar> Table
        {
            get { return this.KonasoftBVFonixContext.Kodszotar; }
        }

        public IQueryable<KodszotarModel> GetRendfokozatok()
        {
            var entities = Table.Where(x => x.KodszotarCsoportId == 299).AsNoTracking().ToList();

            var list = new List<KodszotarModel>();
            foreach (var item in entities)
            {
                list.Add((KodszotarModel)item);
            }

            return list.AsQueryable();
        }

        public IQueryable<KodszotarModel> GetBeosztasok()
        {
            var entities = Table.Where(x => x.KodszotarCsoportId == 118).AsNoTracking().ToList();

            var list = new List<KodszotarModel>();
            foreach (var item in entities)
            {
                list.Add((KodszotarModel)item);
            }

            return list.AsQueryable();
        }

        public IQueryable<Kodszotar> GetRendfokozatokEntities(bool newContext = false)
        {
            List<Kodszotar> entities;
            if (newContext) /// Ez minek?
            {
                entities = new KonasoftBVFonixContext().Kodszotar.Where(x => x.KodszotarCsoportId == 299).AsNoTracking().ToList();
            }
            else
            {
                entities = Table.Where(x => x.KodszotarCsoportId == 299).AsNoTracking().ToList();
            }

            var list = new List<Kodszotar>();
            foreach (var item in entities)
            {
                list.Add((Kodszotar)item);
            }

            return list.AsQueryable();
        }

        

        List<KodszotarModel> EntityToModel(IEnumerable<Kodszotar> entities)
        {
            var list = new List<KodszotarModel>();
            foreach (var item in entities)
            {
                list.Add((KodszotarModel)item);
            }
            return list;
        }

        public IQueryable<KodszotarModel> GetKodszotarakCsoportAlapjan(int csoportId)
        {
            var entities = Table.Where(x => x.KodszotarCsoportId == csoportId).AsNoTracking().ToList();

            var list = new List<KodszotarModel>();
            foreach (var item in entities)
            {
                list.Add((KodszotarModel)item);
            }
            return list.AsQueryable();
        }

        public List<KodszotarModel> GetKodszotarakCsoportAlapjan(KodszotarCsoportok csoport)
        {
            var entities = Table.Where(x => x.KodszotarCsoportId == (int)csoport).AsNoTracking().ToList();

            return EntityToModel(entities);
        }

        
    }
}
