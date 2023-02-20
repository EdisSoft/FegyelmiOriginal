using Edis.Diagnostics;
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
    public class MaganelzarasFofelugyelokFunctions : KonasoftBVFonixFunctionsBase<MaganelzarasFofelugyelokViewModel, MaganelzarasFofelugyelok>, IMaganelzarasFofelugyelokFunctions
    {
        public DbSet<MaganelzarasFofelugyelok> Table => KonasoftBVFonixContext.MaganelzarasFofelugyelok;
        public MaganelzarasFofelugyelokViewModel GetMaganelzarasFofelugyelokByFegyelmiUgyId(int fegyelmiUgyId)
        {
            var result = Table.AsQueryable().FirstOrDefault(f => f.FegyelmiUgyId == fegyelmiUgyId && f.TOROLT_FL == false);
            if (result != null)
            {
                return (MaganelzarasFofelugyelokViewModel)result;
            }
            return null;
        }
        public MaganelzarasFofelugyelokViewModel GetMaganelzarasFofelugyelokByNaploId(int naploId)
        {
            var result = Table.AsQueryable().FirstOrDefault(f => f.NaploId == naploId && f.TOROLT_FL == false);
            if (result != null)
            {
                return (MaganelzarasFofelugyelokViewModel)result;
            }
            return null;
        }


        public void SaveMaganelzarasFofelugyelok(List<int> felugyeloIds, int fegyelmiId, int naploId)
        {
            bool isNewTransaction = KonasoftBVFonixContext.Database.CurrentTransaction == null; // ha még nincs tranzakció folyamtban csak akkor csinálunk újat
            using (DbContextTransaction transaction = isNewTransaction ? KonasoftBVFonixContext.Database.BeginTransaction() : null)
            {
                try
                {
                    foreach (int id in felugyeloIds)
                    {
                        var entity = new MaganelzarasFofelugyelok()
                        {
                            FofelugyeloId = id,
                            FegyelmiUgyId = fegyelmiId,
                            NaploId = naploId
                        };
                        Table.Add(entity);
                        KonasoftBVFonixContext.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a főfelügyelők felvétele során, fegyelmiId:" + fegyelmiId, e);
                    if (isNewTransaction)
                        transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
