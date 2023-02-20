namespace Edis.Functions.JFK
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Edis.Diagnostics;
    using Edis.Entities.Enums;
    using Edis.Entities.Enums.Cimke;
    using Edis.Entities.Fany;
    using Edis.Entities.JFK.FENY;
    using Edis.Functions.Base;
    using Edis.Functions.Common;
    using Edis.Functions.Fany;
    using Edis.Functions.JFK;
    using Edis.Functions.JFK.FENY;
    using Edis.IoC.Attributes;
    using Edis.Repositories.Contexts;
    using Edis.ViewModels.Fany;
    using Edis.ViewModels.JFK.FENY;
    using Edis.ViewModels.JFK.Nyomtatvany;

    public class EsemenyResztvevoFunctions : KonasoftBVFonixFunctionsBase<EsemenyResztvevoViewModel, EsemenyResztvevo>, IEsemenyResztvevoFunctions
    {        

        public DbSet<EsemenyResztvevo> Table => KonasoftBVFonixContext.EsemenyResztvevok;

        [Inject]
        public IAlkalmazasKontextusFunctions AlkalmazasKontextusFunctions { get; set; }

        public List<EsemenyResztvevoViewModel> GetEsemenyResztvevok(int esemenyId)
        {
            var result = Table.AsNoTracking().Where(w => w.EsemenyId == esemenyId).ToList().Select(x => (EsemenyResztvevoViewModel)x).ToList();
            return result;
        }

        public List<string> GetEsemenyResztvetelek(int esemenyId, int fogvatartottId)
        {
            var result = Table.Include(x => x.ErintettsegFoka).AsNoTracking().Where(w => w.EsemenyId == esemenyId && w.FogvatartottId == fogvatartottId).ToList();
            Log.Info($"{esemenyId} esemény és {fogvatartottId} fogvatartott eseményeinek száma: " + result.Count());
            return result.Select(x => x.ErintettsegFoka.Nev).ToList();
        }

        public List<EsemenyResztvevoPanelViewModel> GetEsemenyResztvevokPanelra(int esemenyId)
        {

            var result = (from resztvevo in KonasoftBVFonixContext.EsemenyResztvevok
                          join foka in KonasoftBVFonixContext.Cimkek on resztvevo.ErintettsegFokaCimkeId equals foka.Id
                          join fogv in KonasoftBVFonixContext.Fogvatartottak on resztvevo.FogvatartottId equals fogv.Id
                          join szemely in KonasoftBVFonixContext.FogvatartottSzemelyesAdatai on fogv.Id equals szemely.FogvatartottId
                          join fegyelmi in KonasoftBVFonixContext.FegyelmiUgyek on
                                             new { resztvevo.EsemenyId, FogvatartottId=(int)resztvevo.FogvatartottId } equals new { fegyelmi.EsemenyId, fegyelmi.FogvatartottId } into fegyelmiL
                          from fegyelmiLeft in fegyelmiL.DefaultIfEmpty()
                          where resztvevo.EsemenyId == esemenyId
                          select new EsemenyResztvevoPanelViewModel()
                          {
                              FogvatartottId = (int)resztvevo.FogvatartottId,
                              //Bizonyitek=resztvevo.Bizonyitek,
                              EsemenyId = esemenyId,
                              ErintettsegFokaCimId = resztvevo.ErintettsegFokaCimkeId,
                              ErintettsegFokaNev = foka.Nev,
                              FegyelmiUgyszam = fegyelmiLeft == null ? "" : (fegyelmiLeft.UgySorszamaIntezetAzon + "/" + fegyelmiLeft.UgySorszamaEv + "/" + fegyelmiLeft.UgySorszama),
                              FogvatartottNev = szemely.SzuletesiCsaladiNev_NE_HASZNALD + " " + szemely.SzuletesiUtonev_NE_HASZNALD,
                              NyilvantartasiAzonosito = fogv.NyilvantartasiAzonosito
                          }).ToList();
            return result;
        }

        //public EsemenyResztvevoAdataiViewModel GetEsemenyResztvevoAdatai(int fogvatartottId, int? esemenyId)
        //{
        //    var fogvatartott = KonasoftBVFonixContext.FogvatartottakFegyelmiView.AsNoTracking()
        //        .FirstOrDefault(x => x.Id == fogvatartottId);
        //    //var esemeny = KonasoftBVFonixContext.Esemenyek.AsNoTracking().FirstOrDefault(x => x.Id == esemenyId);
        //    EsemenyResztvevoAdataiViewModel result = new EsemenyResztvevoAdataiViewModel()
        //    {
        //        FogvatartottId = fogvatartottId,
        //        Nev = $"{fogvatartott.SzuletesiCsaladiNev} {fogvatartott.SzuletesiUtonev}",
        //        NyilvantartasiAzonosito = fogvatartott.NyilvantartasiAzonosito,
        //        Ugyszam = "BVOP-1230/2019"
        //    };
        //    return result;
        //}

        public void DeleteEsemenyResztvevok(int esemenyId)
        {
            bool isNewTransaction = KonasoftBVFonixContext.Database.CurrentTransaction == null; // ha még nincs tranzakció folyamtban csak akkor csinálunk újat
            using (DbContextTransaction transaction = isNewTransaction ? KonasoftBVFonixContext.Database.BeginTransaction() : null)
            {
                try
                {
                    var resztvevok = Table.Where(w => w.EsemenyId == esemenyId).ToList();
                    foreach (var resztvevo in resztvevok)
                        Delete(resztvevo.Id);
                    KonasoftBVFonixContext.SaveChanges();
                    if (isNewTransaction)
                        transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a résztvevõk törlése során (eseményId: {esemenyId})", e);
                    if (isNewTransaction)
                        transaction.Rollback();
                    throw;
                }
            }
        }

        //public EsemenyResztvevoAdataiViewModel SaveEsemenyResztvevo(EsemenyResztvevoViewModel resztvevo)
        //{
        //    using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var entity = new EsemenyResztvevo()
        //            {
        //                FogvatartottId = resztvevo.FogvatartottId,
        //                ErintettsegFokaCimkeId = resztvevo.ErintettsegFokaCimkeId,
        //                EsemenyId = resztvevo.EsemenyId,
        //                //Bizonyitek = resztvevo.Bizonyitek
        //            };

        //            Table.Add(entity);
        //            KonasoftBVFonixContext.SaveChanges();

        //            transaction.Commit();
        //        }
        //        catch (Exception e)
        //        {
        //            Log.Error($"Hiba a résztvevõk felvétele során, fogvId:" + resztvevo.FogvatartottId, e);
        //            transaction.Rollback();
        //            throw;
        //        }
        //    }

        //    return GetEsemenyResztvevoAdatai(resztvevo.FogvatartottId, resztvevo.EsemenyId);
        //}

        public void SaveEsemenyResztvevok(List<int> fogvatartottIds, int erintettsegFokaCimkeId, int esemenyId)
        {
            bool isNewTransaction = KonasoftBVFonixContext.Database.CurrentTransaction == null; // ha még nincs tranzakció folyamtban csak akkor csinálunk újat
            using (DbContextTransaction transaction = isNewTransaction ? KonasoftBVFonixContext.Database.BeginTransaction() : null)
            {
                try
                {
                    if(erintettsegFokaCimkeId==(int)CimkeEnums.ErintettsegFoka.SzemelyiAllomanyiTanu)
                    {
                        foreach (int id in fogvatartottIds)
                        {
                            var entity = new EsemenyResztvevo()
                            {
                                AllomanyiSzemelyId = id,
                                ErintettsegFokaCimkeId = erintettsegFokaCimkeId,
                                EsemenyId = esemenyId
                            };

                            Table.Add(entity);
                            KonasoftBVFonixContext.SaveChanges();
                        }
                    }
                    else
                    {
                        foreach (int fogvatartottId in fogvatartottIds)
                        {
                            var entity = new EsemenyResztvevo()
                            {
                                FogvatartottId = fogvatartottId,
                                ErintettsegFokaCimkeId = erintettsegFokaCimkeId,
                                EsemenyId = esemenyId
                            };

                            Table.Add(entity);
                            KonasoftBVFonixContext.SaveChanges();
                        }
                    }
                   
                    if (isNewTransaction)
                        transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a résztvevõk felvétele során, fogvId:" + esemenyId, e);
                    if (isNewTransaction)
                        transaction.Rollback();
                    throw;
                }
            }
        }

        public void DeleteEsemenyResztvevok(List<int> fogvatartottIds, int erintettsegFokaCimkeId, int esemenyId)
        {
            bool isNewTransaction = KonasoftBVFonixContext.Database.CurrentTransaction == null; // ha még nincs tranzakció folyamtban csak akkor csinálunk újat
            using (DbContextTransaction transaction = isNewTransaction ? KonasoftBVFonixContext.Database.BeginTransaction() : null)
            {
                try
                {
                    List<EsemenyResztvevo> toroltResztvevok = new List<EsemenyResztvevo>();
                    if(erintettsegFokaCimkeId==(int)CimkeEnums.ErintettsegFoka.SzemelyiAllomanyiTanu)
                    {
                        toroltResztvevok = Table.Where(x => x.EsemenyId == esemenyId && x.ErintettsegFokaCimkeId == erintettsegFokaCimkeId && x.AllomanyiSzemelyId != null && fogvatartottIds.Contains((int)x.AllomanyiSzemelyId)).ToList();
                    }
                    else
                    {
                        toroltResztvevok = Table.Where(x => x.EsemenyId == esemenyId && x.ErintettsegFokaCimkeId == erintettsegFokaCimkeId && x.FogvatartottId != null && fogvatartottIds.Contains((int)x.FogvatartottId)).ToList();
                    }
                    foreach (var resztvevo in toroltResztvevok)
                    {
                        resztvevo.TOROLT_FL = true;
                        KonasoftBVFonixContext.SaveChanges();
                    }

                    if (isNewTransaction)
                        transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a résztvevõk törlése során, fogvId:" + esemenyId, e);
                    if (isNewTransaction)
                        transaction.Rollback();
                    throw;
                }
            }
        }

        public void DeleteEsemenyResztvevo(int esemenyId, int fogvatartottId)
        {
            var fegyelmiugyekSzama = KonasoftBVFonixContext.FegyelmiUgyek.Where(x => x.EsemenyId == esemenyId && x.FogvatartottId == fogvatartottId).Count();

            if (fegyelmiugyekSzama > 0)
            {
                throw new WarningException("Már van fegyelmi ügy indítva a fogvatartotthoz, ezért nem törölhető.");
            }

            bool isNewTransaction = KonasoftBVFonixContext.Database.CurrentTransaction == null; // ha még nincs tranzakció folyamtban csak akkor csinálunk újat
            using (DbContextTransaction transaction = isNewTransaction ? KonasoftBVFonixContext.Database.BeginTransaction() : null)
            {
                try
                {
                    var resztvevo = Table.SingleOrDefault(x => x.EsemenyId == esemenyId && x.FogvatartottId == fogvatartottId);
                    resztvevo.TOROLT_FL = true;

                    KonasoftBVFonixContext.SaveChanges();

                    if (isNewTransaction)
                        transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a résztvevõ törlése során, fogvId:" + esemenyId, e);
                    if (isNewTransaction)
                        transaction.Rollback();
                    throw;
                }
            }

        }

        public List<EsemenyResztvevoAdataiViewModel> GetEsemenyResztvevokByIntezetId(int intezetId)
        {
            var result = Table.AsNoTracking().Where(x => x.RogzitoIntezetId == intezetId || intezetId == (int)BvIntezet.Bvop)
                .Join(KonasoftBVFonixContext.FogvatartottakFegyelmiView,
                resztvevo => resztvevo.FogvatartottId,
                nezet => nezet.Id,
                (resztvevo, nezet) => new EsemenyResztvevoAdataiViewModel() { FogvatartottId = nezet.Id, Nev = nezet.SzuletesiCsaladiNev + " " + nezet.SzuletesiUtonev, NyilvantartasiAzonosito = nezet.NyilvantartasiAzonosito, EsemenyId = resztvevo.EsemenyId, ErintettsegFokaCimId = resztvevo.ErintettsegFokaCimkeId })
                .OrderBy(o => o.Nev)
                .ToList();

            return result;
        }

        public bool WarningTanuVagyEszleloByFegyelmiUgyIds(List<int> fegyelmiUgyIds)
        {
            var warning = false;
            var modalFelnyitoja = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId;
            var esemenyIds = KonasoftBVFonixContext.FegyelmiUgyek
                        .Where(x => fegyelmiUgyIds.Contains(x.Id))
                        .Select(x => x.EsemenyId)
                        .ToList();
            foreach (var esemenyId in esemenyIds)
            {
                var eszlelokLista = KonasoftBVFonixContext.Esemenyek
                    .Where(x => x.Id == esemenyId && x.EszleloId == modalFelnyitoja)
                    .Select(x => x.EszleloId);
                var tanukLista = Table.AsNoTracking()
                    .Where(x => x.EsemenyId == esemenyId &&
                                x.ErintettsegFokaCimkeId == (int)CimkeEnums.ErintettsegFoka.SzemelyiAllomanyiTanu
                                 && x.TOROLT_FL == false)
                     .Select(x => (int)x.AllomanyiSzemelyId)
                    .ToList();

                warning = eszlelokLista.Contains(modalFelnyitoja) || tanukLista.Contains(modalFelnyitoja) ? true : false;
            }
            return warning;
        }
    }
}
