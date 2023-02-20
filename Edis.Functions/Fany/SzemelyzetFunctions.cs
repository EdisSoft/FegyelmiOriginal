using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.Entities.Fany;
using Edis.Functions.Base;
using Edis.Functions.Common;
using Edis.IoC;
using Edis.ViewModels.Fany;
using Edis.IoC.Attributes;
using System.DirectoryServices.AccountManagement;
using Edis.Diagnostics;
using Edis.Entities.Base;

namespace Edis.Functions.Fany
{
    using Entities.Common;
    using Edis.Repositories.Utils;
    using Edis.ViewModels.JFK.FENY.FormModel;
    using System.Data.SqlClient;
    using System.Security.Principal;
    using System.Web.Mvc;

    public class SzemelyzetFunctions : KonasoftBVFonixFunctionsBase<SzemelyzetModel, Szemelyzet>, ISzemelyzetFunctions
    {
        public DbSet<Szemelyzet> Table
        {
            get { return this.KonasoftBVFonixContext.Szemelyzet; }
        }


        private const string FUNKCIOAZONOSITO = "jogosultsag_karb";
        private const int _technikai_intezet_id = 137;
        private const int _technikai_szemelyzet_id = 1;

        [Inject]
        public IActiveDirectoryFunctions ActiveDirectoryFunctions { get; set; }

        [Inject]
        public IAlkalmazasKontextusFunctions AlkalmazasKontextusFunctions { get; set; }

        [Inject]
        public IIntezetFunctions IntezetFunctions { get; set; }


        [Inject]
        public IJogosultsagCacheFunctions JogosultsagCacheFunctions { get; set; }


        public AdFegyelmiUserItem GetAdFegyelmiUser(SzemelyzetModel szemelyzet)
        {
            AdFegyelmiUserItem adFegyelmiUser = new AdFegyelmiUserItem();


            adFegyelmiUser =
                (from szem in KonasoftBVFonixContext.Szemelyzet
                 join rendfokozat in KonasoftBVFonixContext.Kodszotar on szem.RendfokozatKszId equals rendfokozat.Id
                 where szem.Id == szemelyzet.Id
                 select new AdFegyelmiUserItem
                 {
                     Rendfokozat = rendfokozat.Nev
                 }).SingleOrDefault();

            adFegyelmiUser.Sid = szemelyzet.AdSid;
            adFegyelmiUser.Displayname = szemelyzet.Nev;

            return adFegyelmiUser;

        }
        public List<AdFegyelmiUserItem> GetFegyelmiEszlelok()
        {
            return GetIntezetiAlkalmazottak();
        }

        public List<AdFegyelmiUserItem> GetIntezetiAlkalmazottak()
        {
            var fegyelmiUsers = KonasoftBVFonixContext.Database.SqlQuery<AdFegyelmiUserItem>
            ("fegyelmi.KeresActiveDirectoryUsers @AdGroupName",
                new SqlParameter("@AdGroupName", JogosultsagCacheFunctions.AktualisIntezet.Azonosito2 + "-users")
            ).ToList();

            fegyelmiUsers = fegyelmiUsers.GroupBy(x => x.Sid).Select(x => x.First()).ToList();

#if !DEBUG
            var userDic = fegyelmiUsers.ToDictionary(x => x.Sid);
            //var excludedSidList= KonasoftBVFonixContext.DebugSidek.Select(x => x.Sid).ToList();
            //foreach (var sid in excludedSidList)
            //{
            //    if (userDic.ContainsKey(sid))
            //        userDic.Remove(sid);
            //}
            fegyelmiUsers = userDic.Values.ToList();
#endif

            return fegyelmiUsers;
        }

        public List<AdFegyelmiUserItem> GetFegyelmiAlkalmazottak()
        {
            var fegyelmiUsers = new List<AdFegyelmiUserItem>();

            fegyelmiUsers.AddRange(GetFegyelmiJogkorGyakorlojaAlkalmazottak());
            fegyelmiUsers.AddRange(GetFegyelmiReintegraciosTisztiAlkalmazottak());
            fegyelmiUsers.AddRange(GetFegyelmiEgyebSzakteruletAlkalmazottak());

            fegyelmiUsers = fegyelmiUsers.GroupBy(x => x.Sid).Select(x => x.First()).ToList();
            return fegyelmiUsers;
        }

        public List<AdFegyelmiUserItem> GetFegyelmiEgyebSzakteruletAlkalmazottak()
        {
            var users= KonasoftBVFonixContext.Database.SqlQuery<AdFegyelmiUserItem>
                    ("fegyelmi.KeresActiveDirectoryUsers @AdGroupName",
                        new SqlParameter("@AdGroupName", JogosultsagCacheFunctions.AktualisIntezet.Azonosito2 + "-FN-Fegyelmi-egyeb-szakterulet")
                    ).ToList().GroupBy(x => x.Sid).Select(x => x.First()).ToList();

#if !DEBUG
            var userDic = users.ToDictionary(x => x.Sid);
            //var excludedSidList = KonasoftBVFonixContext.DebugSidek.Select(x => x.Sid).ToList();
            //foreach (var sid in excludedSidList)
            //{
            //    if (userDic.ContainsKey(sid))
            //        userDic.Remove(sid);
            //}
            users = userDic.Values.ToList();
#endif
            return users;
        }

        public List<AdFegyelmiUserItem> GetFegyelmiReintegraciosTisztiAlkalmazottak()
        {
            var users= KonasoftBVFonixContext.Database.SqlQuery<AdFegyelmiUserItem>
                    ("fegyelmi.KeresActiveDirectoryUsers @AdGroupName",
                        new SqlParameter("@AdGroupName", JogosultsagCacheFunctions.AktualisIntezet.Azonosito2 + "-FN-Fegyelmi-reintegracios-tiszt")
                    ).ToList().GroupBy(x => x.Sid).Select(x => x.First()).ToList();

#if !DEBUG
            var userDic = users.ToDictionary(x => x.Sid);
            //var excludedSidList = KonasoftBVFonixContext.DebugSidek.Select(x => x.Sid).ToList();
            //foreach (var sid in excludedSidList)
            //{
            //    if (userDic.ContainsKey(sid))
            //        userDic.Remove(sid);
            //}
            users = userDic.Values.ToList();
#endif
            return users;
        }

        public List<AdFegyelmiSzakvezetoUserItem> GetFegyelmiSzakteruletiVezetok(string term)
        {
            return KonasoftBVFonixContext.Database.SqlQuery<AdFegyelmiSzakvezetoUserItem>
                    ("[fegyelmi].[KeresActiveDirectorySzakvezetok] @AdGroupName, @Term",
                        new SqlParameter("@AdGroupName", JogosultsagCacheFunctions.AktualisIntezet.Azonosito2 + "-users")
                        , new SqlParameter("@Term", term)
                    ).GroupBy(x => x.Sid).Select(x => x.First()).ToList();
        }

        public List<AdFegyelmiUserItem> GetFegyelmiJogkorGyakorlojaAlkalmazottak()
        {
            var users= KonasoftBVFonixContext.Database.SqlQuery<AdFegyelmiUserItem>
                        ("fegyelmi.KeresActiveDirectoryUsers @AdGroupName",
                            new SqlParameter("@AdGroupName", JogosultsagCacheFunctions.AktualisIntezet.Azonosito2 + "-FN-Fegyelmi-jogkor-gyakorloja")
                        ).ToList().GroupBy(x => x.Sid).Select(x => x.First()).ToList();

#if !DEBUG
            var userDic = users.ToDictionary(x => x.Sid);
            //var excludedSidList = KonasoftBVFonixContext.DebugSidek.Select(x => x.Sid).ToList();
            //foreach (var sid in excludedSidList)
            //{
            //    if (userDic.ContainsKey(sid))
            //        userDic.Remove(sid);
            //}
            users = userDic.Values.ToList();
#endif
            return users;
        }

        public Szemelyzet SzemelyzetLekeresVagyLetrehozas(string sidStr, string userName, int? intezetId = null)
        {
            bool uj;
            return SzemelyzetLekeresVagyLetrehozas(sidStr, userName, true, intezetId, out uj);
        }

        public Szemelyzet SzemelyzetLekeresVagyLetrehozas(string sidStr, string userName, bool frissitesAdAlapjan, int? intezetId, out bool ujLetrehozva)
        {
            ujLetrehozva = false;
            if (string.IsNullOrEmpty(sidStr)) return null;

            Szemelyzet adSzemelyzet = null;
            UserPrincipal up = ActiveDirectoryFunctions.BetoltesUserPrincipal(sidStr);

            if (up == null)
            {
                Szemelyzet sz = FindBySid(sidStr, intezetId);
                return sz;
            }

            string hadrendiSzam = ActiveDirectoryFunctions.KeresHadrendiSzam(sidStr);
            string rendfokozat = ActiveDirectoryFunctions.KeresRendfokozat(sidStr);
            string beosztas = ActiveDirectoryFunctions.KeresBeosztas(sidStr);
            string beosztasiFokozat = ActiveDirectoryFunctions.KeresBeosztasiFokozat(sidStr);

            if (intezetId == null)
                intezetId = JogosultsagCacheFunctions.AktualisIntezet.Id;

            int rendfokozatKszId = 0;
            int beosztasKszId = 0;
            int beosztasiFokozatKszId = 0;

            if (!String.IsNullOrEmpty(rendfokozat))
            {
                IList<Kodszotar> rendfokozatok =
                    KonasoftBVFonixContext.Kodszotar.Where(x => x.KodszotarCsoportId == 299).ToList();
                Kodszotar rendfokozatKsz = rendfokozatok.FirstOrDefault(r => r.Nev.ToLower() == rendfokozat.ToLower() || r.RovidNev.ToLower() == rendfokozat.ToLower());
                if (rendfokozatKsz != null)
                    rendfokozatKszId = rendfokozatKsz.Id;
            }
            if (!String.IsNullOrEmpty(beosztas))
            {
                IList<Kodszotar> beosztasok =
                   KonasoftBVFonixContext.Kodszotar.Where(x => x.KodszotarCsoportId == 118).ToList();
                Kodszotar beosztasKsz = beosztasok.FirstOrDefault(r => r.Nev.ToLower() == beosztas.ToLower() || r.RovidNev.ToLower() == beosztas.ToLower());
                if (beosztasKsz != null)
                    beosztasKszId = beosztasKsz.Id;
            }
            if (!String.IsNullOrEmpty(beosztasiFokozat))
            {
                IList<Kodszotar> beosztasiFokozatok =
                    KonasoftBVFonixContext.Kodszotar.Where(x => x.KodszotarCsoportId == 380).ToList();
                Kodszotar beosztasiFokozatKsz = beosztasiFokozatok.FirstOrDefault(r => r.Nev.ToLower() == beosztasiFokozat.ToLower() || r.RovidNev.ToLower() == beosztasiFokozat.ToLower());
                if (beosztasiFokozatKsz != null)
                    beosztasiFokozatKszId = beosztasiFokozatKsz.Id;
            }

            adSzemelyzet = new Szemelyzet();
            adSzemelyzet.Nev = new string((up.DisplayName?.ToUpper() ?? up.SamAccountName?.ToUpper() ?? userName ?? "NÉVTELEN").Take(200).ToArray());
            adSzemelyzet.AdSid = sidStr;
            if (String.IsNullOrEmpty(hadrendiSzam))
            {
                adSzemelyzet.Azonosito = sidStr.Substring(0, 2) + sidStr.Substring(sidStr.Length - 5, 5);
            }
            else
            {
                adSzemelyzet.Azonosito = new string(hadrendiSzam.Take(50).ToArray());
            }
            adSzemelyzet.IntezetId = intezetId;
            adSzemelyzet.RendfokozatKszId = rendfokozatKszId;
            adSzemelyzet.BeosztasKszId = beosztasKszId;
            adSzemelyzet.BesorolasKszId = beosztasiFokozatKszId;

            Szemelyzet szemelyzet = FindBySid(sidStr, intezetId);
            if (szemelyzet == null)
            {
                ujLetrehozva = true;
                Uj(adSzemelyzet);
                Save();
                szemelyzet = adSzemelyzet;
            }
            else if (frissitesAdAlapjan)
            {
                bool valtozott = false;

                //  valtozott |= szemelyzet.IntezetId != adSzemelyzet.IntezetId;
                valtozott = valtozott || szemelyzet.Nev.ToUpper() != adSzemelyzet.Nev.ToUpper();
                //valtozott = valtozott || szemelyzet.RendfokozatKszId != adSzemelyzet.RendfokozatKszId;
                valtozott = valtozott || szemelyzet.Azonosito != adSzemelyzet.Azonosito;
                valtozott = valtozott || szemelyzet.AdSid != adSzemelyzet.AdSid;
                valtozott = valtozott || szemelyzet.RendfokozatKszId != adSzemelyzet.RendfokozatKszId;
                valtozott = valtozott || szemelyzet.BeosztasKszId != adSzemelyzet.BeosztasKszId;
                valtozott = valtozott || szemelyzet.BesorolasKszId != adSzemelyzet.BesorolasKszId;

                if (valtozott)
                {
                    //BeallitModositashoz(szemelyzet);
                    //  szemelyzet.IntezetId = adSzemelyzet.IntezetId;
                    szemelyzet.BeosztasKszId = adSzemelyzet.BeosztasKszId;
                    szemelyzet.Nev = adSzemelyzet.Nev;
                    szemelyzet.RendfokozatKszId = adSzemelyzet.RendfokozatKszId;
                    szemelyzet.Azonosito = adSzemelyzet.Azonosito;
                    szemelyzet.AdSid = adSzemelyzet.AdSid;
                    if (adSzemelyzet.RendfokozatKszId != 0)
                        szemelyzet.RendfokozatKszId = adSzemelyzet.RendfokozatKszId;
                    if (adSzemelyzet.BeosztasKszId != 0)
                        szemelyzet.BeosztasKszId = adSzemelyzet.BeosztasKszId;
                    if (adSzemelyzet.BesorolasKszId != 0)
                        szemelyzet.BesorolasKszId = adSzemelyzet.BesorolasKszId;

                    Modosit(szemelyzet);
                }
            }

            return szemelyzet;
        }


        public Szemelyzet FindBySid(string sid, int? intezetId)
        {
            if (!intezetId.HasValue)
                intezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId;

            return Table.SingleOrDefault(x => x.AdSid == sid && x.IntezetId == intezetId);
        }



        public IQueryable<Szemelyzet> GetSzemelyzetTagokBySid(string[] sids, int? intezetId)
        {
            if (!intezetId.HasValue)
                intezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId;

            return Table.Where(x => sids.Contains(x.AdSid) && x.IntezetId == intezetId);
        }




        public Szemelyzet KeresesById(int id)
        {
            return KonasoftBVFonixContext.Szemelyzet.SingleOrDefault(x => x.Id == id);
        }

        public void Uj(Szemelyzet szemelyzet)
        {

            //    szemelyzet.Id = KonasoftBVFonixContext.Database.SqlQuery<Szekvencia>("SELECT get_kovetkezo_id_fv(null,{0}) AS Id;", "SZEMELYZET").First().Id;

            KonasoftBVFonixContext.Szemelyzet.Add(szemelyzet);
        }

        public void Modosit(Szemelyzet szemelyzet)
        {
            Szemelyzet sz = KeresesById(szemelyzet.Id);
            KonasoftBVFonixContext.Entry(sz).CurrentValues.SetValues(szemelyzet);
            KonasoftBVFonixContext.SaveChanges();
        }

        public Szemelyzet KeresesByAzonosito(string azonosito)
        {
            IAlkalmazasKontextusFunctions appsettingsFunctions;
            using (InjectionKernel.Instance.BeginScope())
            {
                appsettingsFunctions = InjectionKernel.Instance.GetInstance<IAlkalmazasKontextusFunctions>();

            }

            var returnValue = Table.SingleOrDefault(e => e.Azonosito == azonosito && e.IntezetId == appsettingsFunctions.Kontextus.RogzitoIntezetId);
            if (returnValue == null)
            {
                IList<Szemelyzet> returnValueList = Table.Where(x => x.Azonosito == azonosito).ToList(); //Dal.Kereses("Azonosito", azonosito, UjInjectalas<Szemelyzet>, false);
                if (returnValueList.Any(r => r.IntezetId == appsettingsFunctions.Kontextus.RogzitoIntezetId))
                {
                    returnValue = returnValueList.FirstOrDefault(r => r.IntezetId == appsettingsFunctions.Kontextus.RogzitoIntezetId);
                    //if (returnValue == null)
                    //    returnValue = returnValueList.First();
                    //-AdatKontextus.Szemelyzetek.Add(returnValue);
                    //-KesleltetettBetoltokBeallitasa(returnValue);
                }
            }
            return returnValue;
        }

        public List<SzemelyzetModel> GetAllSzemelyzet(int intezetId)
        {
            var result = Table
                .Where(x => x.Id != 0 && x.Nev != null && x.IntezetId == intezetId)
                .ToList()
                .Select(x => (SzemelyzetModel)x)
                .ToList();

            return result;
        }

        public List<AdFegyelmiUserItem> GetFegyelmiFofelugyelokAlkalmazottak()
        {
            return KonasoftBVFonixContext.Database.SqlQuery<AdFegyelmiUserItem>
                        ("fegyelmi.KeresActiveDirectoryUsers @AdGroupName",
                            new SqlParameter("@AdGroupName", JogosultsagCacheFunctions.AktualisIntezet.Azonosito2 + "-FN-Fegyelmi-fofelugyelo")
                        ).ToList().GroupBy(x => x.Sid).Select(x => x.First()).ToList();
        }

        public Szemelyzet SzemelyzetLekeresVagyLetrehozasNevvel(string sidStr, string nev, int? intezetId = null)
        {
            bool uj;
            Szemelyzet user = null;
            try
            {
                user = FindBySid(sidStr, intezetId);
            }
            catch (Exception) { }

            if (user == null)
            {
                user = SzemelyzetLekeresVagyLetrehozas(sidStr, nev, true, intezetId, out uj);
                if (uj)
                {
                    user.Nev = nev;
                    Save();
                }
            }
            return user;
        }

        public void ResetContext()
        {
            if (KonasoftBVFonixContext.IsDisposed())
            {
                KonasoftBVFonixContext = Repositories.Contexts.KonasoftBVFonixContext.GetContextInstance();
            }
        }
    }
}
