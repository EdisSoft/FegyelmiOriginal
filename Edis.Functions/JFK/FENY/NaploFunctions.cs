namespace Edis.Functions.JFK
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Security.Principal;
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
    using Edis.ViewModels.JFK.FENY.FormModel;
    using Edis.ViewModels.JFK.Nyomtatvany;
    using static Edis.Entities.Enums.Cimke.CimkeEnums;

    public class NaploFunctions : KonasoftBVFonixFunctionsBase<NaploViewModel, Naplo>, INaploFunctions
    {
        public DbSet<Naplo> Table => KonasoftBVFonixContext.Naplo;

        [Inject]
        public IAlkalmazasKontextusFunctions AlkalmazasKontextusFunctions { get; set; }
        [Inject]
        ISzemelyzetFunctions SzemelyzetFunctions { get; set; }
        [Inject]
        public ICimkeFunctions CimkeFunctions { get; set; }


        public void CreateNaploBejegyzesDontesFegyelmiUgyElrendeleserol(List<int> fegyelmiUgyIds)
        {

            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                int currentId = 0;
                try
                {
                    foreach (var fegyelmiUgyId in fegyelmiUgyIds)
                    {
                        currentId = fegyelmiUgyId;

                        var fegyelmiUgy = (FegyelmiUgyViewModel)KonasoftBVFonixContext.FegyelmiUgyek.FirstOrDefault(f => f.Id == fegyelmiUgyId);

                        var entity = new Naplo()
                        {
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            FegyelmiUgyId = fegyelmiUgy.Id,
                            JegyzokonyvTartalma = fegyelmiUgy.StatuszCimkeId == (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban ?
                                CimkeFunctions.FindById((int)FegyelmiNaploTipus.EljarastElrendelem).Nev : CimkeFunctions.FindById((int)FegyelmiNaploTipus.ReintegraciosJogkorbe).Nev,
                            TipusCimkeId = fegyelmiUgy.StatuszCimkeId == (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban ? (int)FegyelmiNaploTipus.EljarastElrendelem : (int)FegyelmiNaploTipus.ReintegraciosJogkorbe,
                            DonteshozoSzemelyId = fegyelmiUgy.ElrendeloSzemelyId,
                            KivizsgaloSzemelyId = fegyelmiUgy.KivizsgaloSzemelyId,
                            Hatarido = fegyelmiUgy.KivizsgalasiHatarido
                        };

                        Table.Add(entity);
                    }
                    KonasoftBVFonixContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a naplóbejegyzés rögzítése során, fegyelmiÜgyId:" + currentId, e);
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void CreateNaploBejegyzesJogiKepviseletrol(List<int> fegyelmiUgyIds)
        {

            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                int currentId = 0;
                try
                {
                    foreach (var fegyelmiUgyId in fegyelmiUgyIds)
                    {
                        currentId = fegyelmiUgyId;

                        var fegyelmiUgy = (FegyelmiUgyViewModel)KonasoftBVFonixContext.FegyelmiUgyek.FirstOrDefault(f => f.Id == fegyelmiUgyId);

                        var entity = new Naplo()
                        {
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            FegyelmiUgyId = fegyelmiUgy.Id,
                            JegyzokonyvTartalma = CimkeFunctions.FindById((int)FegyelmiNaploTipus.JogiKepviseletetKer).Nev,
                            TipusCimkeId = (int)FegyelmiNaploTipus.JogiKepviseletetKer
                        };

                        Table.Add(entity);
                    }
                    KonasoftBVFonixContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a naplóbejegyzés rögzítése során, fegyelmiÜgyId:" + currentId, e);
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public List<NaploListaViewmodel> GetNaplobejegyzesekByFegyelmiUgyId(int fegyelmiUgyId)
        {

            var dic = new Dictionary<string, object>();
            dic.Add("@FegyelmiUgyId", fegyelmiUgyId);
            List<NaploListaViewmodel> list=new List<NaploListaViewmodel>();
            //if (fegyelmiUgyId > 0)
            //    list = KonasoftBVFonixContext.RunStoredProcedureByNev<NaploListaViewmodel>("Fegyelmi.GetNaploBejegyzesekByFegyelmiId", dic);
            //else
            //    list = F3Context.Database.SqlQuery<NaploListaViewmodel>
            //        ("Fegyelmi.GetNaploBejegyzesekByFegyelmiFanyId @FegyelmiUgyId", new SqlParameter("@FegyelmiUgyId", fegyelmiUgyId)).ToList();
            
            return list;

        }

        public void ModifyNaploBejegyzes(FegyelmiUgyHatarozatRogziteseModel model)
        {
            var fenyitesHosszanakMennyisegiEgysegei = CimkeFunctions.GetFegyelmiFenyitesHosszanakMennyisegiEgysegei();

            var naploBejegyzes = Table.Single(s => s.Id == model.NaplobejegyzesId.Value);
            naploBejegyzes.AlairtaFl = !model.PanasszalElt;
            naploBejegyzes.Vegleges = model.IsVegleges;//model.FenyitesTipusaCimkeId == FenyitesTipusok.Megszuntetes.CastToInt() ? true : false;
            naploBejegyzes.FenyitesTipusCimkeId = model.FenyitesTipusaCimkeId;
            naploBejegyzes.FenyitesHossza = model.FenyitesHossza;
            if (!string.IsNullOrWhiteSpace(model.FenyitesHosszaMennyisegiEgyseg))
                naploBejegyzes.FenyitesHosszaMennyisegiEgysegCimkeId = fenyitesHosszanakMennyisegiEgysegei.Single(s => s.Nev.ToLower() == model.FenyitesHosszaMennyisegiEgyseg.ToLower()).Id;
            naploBejegyzes.JegyzokonyvTartalma = model.Leiras;
            naploBejegyzes.FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId;
            naploBejegyzes.VetsegRendeletSzerintCimkeId = model.VetsegRendeletSzerintCimkeId;
            naploBejegyzes.DonteshozoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(WindowsIdentity.GetCurrent().User.Value, null, null).Id;
            naploBejegyzes.KietkezesCsokkentes = model.KietkezesCsokkentes;
            naploBejegyzes.MegszuntetesOkaCimkeId = model.MegszuntetesOkaCimkeId;
            KonasoftBVFonixContext.SaveChanges();
        }

        public void ModifyNaploBejegyzes(FegyelmiUgyHatarozatRogziteseMasodfokonModel model)
        {
            var fenyitesHosszanakMennyisegiEgysegei = CimkeFunctions.GetFegyelmiFenyitesHosszanakMennyisegiEgysegei();

            var naplobejegyzesId = model.NaplobejegyzesIds.First();
            var naploBejegyzes = Table.Single(s => s.Id == naplobejegyzesId);
            naploBejegyzes.Vegleges = model.FenyitesTipusaCimkeId == FenyitesTipusok.Megszuntetes.CastToInt() ? true : false;
            naploBejegyzes.FenyitesTipusCimkeId = model.FenyitesTipusaCimkeId;
            naploBejegyzes.FenyitesHossza = model.FenyitesHossza;
            if (!string.IsNullOrWhiteSpace(model.FenyitesHosszaMennyisegiEgyseg))
                naploBejegyzes.FenyitesHosszaMennyisegiEgysegCimkeId = fenyitesHosszanakMennyisegiEgysegei.Single(s => s.Nev.ToLower() == model.FenyitesHosszaMennyisegiEgyseg.ToLower()).Id;
            naploBejegyzes.JegyzokonyvTartalma = model.Leiras;
            naploBejegyzes.FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId;
            naploBejegyzes.VetsegRendeletSzerintCimkeId = model.VetsegRendeletSzerintCimkeId;
            if(model.TorvenyszekId.HasValue)
                naploBejegyzes.DonteshozoSzemelyId = null;
            else
                naploBejegyzes.DonteshozoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(WindowsIdentity.GetCurrent().User.Value, null, null).Id;
            naploBejegyzes.KietkezesCsokkentes = model.KietkezesCsokkentes;
            naploBejegyzes.TorvenyszekId = model.TorvenyszekId;
            naploBejegyzes.IsHelybenhagyasFl = model.IsHelybenhagyas;
            naploBejegyzes.AlairtaFl = model.IsVegleges;
            KonasoftBVFonixContext.SaveChanges();
        }

        public void NaploBejegyzesInaktivalasa(int naploBejegyzesId)
        {
            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    var naploBejegyzes = Table.Single(s => s.Id == naploBejegyzesId);
                    naploBejegyzes.InaktivFl = true;
                    KonasoftBVFonixContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Log.Error($"Hiba történt a(z) {naploBejegyzesId} naplóbejegyzés inaktiválása során", e);
                    throw new Exception("A naplóbejegyzés inaktiválása nem sikerült.");
                }
            }
        }

        public void NaploBejegyzesAktivalasa(int naploBejegyzesId)
        {
            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    var naploBejegyzes = Table.Single(s => s.Id == naploBejegyzesId);
                    naploBejegyzes.InaktivFl = false;
                    KonasoftBVFonixContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Log.Error($"Hiba történt a(z) {naploBejegyzesId} naplóbejegyzés aktiválása során", e);
                    throw new Exception("A naplóbejegyzés aktiválása nem sikerült.");
                }
            }
        }
    }
}
