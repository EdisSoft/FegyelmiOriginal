using Edis.Entities.Enums;
using Edis.Entities.Fany;
using Edis.Functions.Base;
using Edis.Functions.Common;
using Edis.IoC;
using Edis.IoC.Attributes;
using Edis.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using Edis.Diagnostics;
using System.Web;
using Edis.Entities.Common;
using System.IO;
using System.Configuration;

namespace Edis.Functions.Fany
{
    using Edis.Repositories.Contexts;
    using Edis.ViewModels.Fany;
    using System.Web.Configuration;

    public class JogosultsagKezeloFunctions : KonasoftBVFonixFunctionsBase<object, object>, IJogosultsagKezeloFunctions
    {
        [Inject]
        public IAlkalmazasKontextusFunctions AlkalmazasKontextus { get; set; }

        [Inject]
        public ITranzakcioAdatKontextusFunctions TranzakcioAdatKontextus { get; set; }

        [Inject]
        public IIntezetFunctions IntezetFunctions { get; set; }

        [Inject]
        public IActiveDirectoryFunctions ActiveDirectoryFunctions { get; set; }


        #region mezők
        private const string FUNKCIOAZONOSITO = "jogosultsag_karb";
        private const int _technikai_intezet_id = 137;
        private const int _technikai_szemelyzet_id = 1;
        #endregion

        #region jellemzők

        public IActiveDirectoryFunctions ADTarolo { get; private set; }

        [Inject]
        public ISzemelyzetFunctions SzemelyzetTarolo { get; set; }
        public IIntezetFunctions IntezetTarolo { get; private set; }
        public IKodszotarFunctions KodszotarTarolo { get; private set; }
        public SzemelyzetSzerepkor AktualisSzemelyzetSzerepkor { get; set; }
        public SzemelyzetCsoport AktualisSzemelyzetCsoport { get; set; }
        private IJogosultsagCacheFunctions JogosultsagCacheFunctions { get; set; }
        //        private LogGenerator LogGenerator { get; set; }


        //        [Perzisztalt(new[] { Jogosultsagok.ATSzSzLi, Jogosultsagok.ATSzSzRe })]
        public SzerepkorModositasTipus AktualisSzerepkorModositasTipus { get; set; }

        //        [Perzisztalt(new[] { Jogosultsagok.ATSzSzLi, Jogosultsagok.ATSzSzRe })]
        public object AktualisJogosultsagFa { get; set; }

        #endregion jellemzők

        #region konstruktor
        public JogosultsagKezeloFunctions(AlkalmazasKontextus alkalmazasKontextus,
                                          IActiveDirectoryFunctions adTarolo,
                                          ISzemelyzetFunctions szemelyzetTarolo,
                                          IIntezetFunctions intezetTarolo,
                                                  IJogosultsagCacheFunctions jogosultsagCache/*,
                                                  INaplozasSeged naplozasSeged,
                                                  LogGenerator logGenerator*/)
        //: base(alkalmazasKontextus, adatKontextus, alkalmazasNaploTarolo, tranzakcioTarolo, alkalmazasFunkcioTarolo, FUNKCIOAZONOSITO)
        {
            ADTarolo = adTarolo;
            SzemelyzetTarolo = szemelyzetTarolo;
            IntezetTarolo = intezetTarolo;
            JogosultsagCacheFunctions = jogosultsagCache;
        }

        #endregion

        #region eljárások
        public void LehetsegesIntezetekBeallitasaCsakActiveDirectoryOlvasassal(WindowsIdentity sid)
        {
            if (JogosultsagCacheFunctions.AktualisIntezet != null)
                return;

            JogosultsagCacheFunctions.JogosultIntezetek = IntezetFunctions.IntezetListaLegyujtes(sid.User.Value);

        }
        public void JogosultsagNelkuliIntezetBeallitas(WindowsIdentity sid)
        {
            if (JogosultsagCacheFunctions.AktualisIntezet != null)
                return;

            JogosultsagCacheFunctions.JogosultIntezetek = IntezetFunctions.IntezetListaLegyujtes(sid.User.Value, true);

        }

        public void LehetsegesIntezetekBeallitasaAdCsoportbol(UserPrincipal user)
        {
            if (JogosultsagCacheFunctions.AktualisIntezet != null)
                return;

            JogosultsagCacheFunctions.JogosultIntezetek = new IntezetFunctions().IntezetListaLegyujtesAdCsoport(user);

        }
        //public void SetLehetsegesRaktarak(List<int> intezetIds)
        //{
        //    if (JogosultsagCacheFunctions.JogosultRaktarak != null && JogosultsagCacheFunctions.JogosultRaktarak.Any())
        //        return;

        //    JogosultsagCacheFunctions.JogosultRaktarak = KeszletRaktarFunctions.GetRaktarakByIntezetIds(intezetIds);
        //}

        public void LehetsegesIntezetekBeallitasa(WindowsIdentity sid)
        {
            Edis.Diagnostics.Log.Debug(string.Format("SID: {0}, {1}", sid.Name, sid.User));
            LehetsegesIntezetekBeallitasa(sid, "");
        }

        public void LehetsegesIntezetekBeallitasa(WindowsIdentity sid, string kapottAzonosito)
        {
            if (JogosultsagCacheFunctions.AktualisIntezet != null) return;
            //-LogGenerator.LogToFile("LehetsegesIntezetekBeallitasa START:" + sid.User.Value, 1);
            JogosultsagCacheFunctions.JogosultIntezetek = IntezetListaLegyujtes(sid.User.Value);
            if (!String.IsNullOrEmpty(kapottAzonosito))
            {
                JogosultsagCacheFunctions.AktualisIntezet = JogosultsagCacheFunctions.JogosultIntezetek.FirstOrDefault(i => i.Azonosito == kapottAzonosito);
                if (JogosultsagCacheFunctions.AktualisIntezet != null)
                    AlkalmazasKontextus.Kontextus.RogzitoIntezetId = JogosultsagCacheFunctions.AktualisIntezet.Id;
            }
            if (JogosultsagCacheFunctions.AktualisIntezet != null) return;
            int? intezetId = (JogosultsagCacheFunctions.JogosultIntezetek.Any()) ? JogosultsagCacheFunctions.JogosultIntezetek.First().Id : (int?)null;
            if (intezetId.HasValue)
            {
                //foreach (var intezet in JogosultIntezetek)
                //    LogGenerator.LogToFile("intezet: " + intezet.RovidNev, 1);
            }
            else
            {
                //-LogGenerator.LogToFile("intezetId: Nincs", 1);
                return;
            }
            if (JogosultsagCacheFunctions.JogosultIntezetek.Count == 1)
                JogosultsagCacheFunctions.AktualisIntezet = JogosultsagCacheFunctions.JogosultIntezetek.First();
            else
            {
                IDictionary<string, int> intezetAzonosito2IdMegfeleltetes = IntezetAzonosito2IdMegfeleltetes("LehetsegesIntezetekBeallitasa;IntezetTarolo nincs feltoltve");
                //-LogGenerator.LogToFile("IntezetAzonosito2IdMegfeleltetes", 1);

                //var szerepkorTable = SzerepkorTarolo.Table.ToList();
                //foreach (var item in szerepkorTable)
                //{
                //    item.Azonosito = item.Azonosito.ToLower();
                //}


                //var jogosultsagIterator = new SzemelyzetJogosultsagok(SzemelyzetTarolo.Table.Where(x => x.AdSid == sid.User.Value).FirstOrDefault(),
                //                                                     ADTarolo.ListazasUserGroupPrincipal(
                //                                                         sid.User.Value),
                //                                                        szerepkorTable,
                //                                                     intezetAzonosito2IdMegfeleltetes, true);
                ////-LogGenerator.LogToFile("josoultsagIterator", 1);
                //List<JogosultsagInformacio> jogosultsagInformaciok = jogosultsagIterator != null ? jogosultsagIterator.ToList() : null;
                //if (jogosultsagInformaciok != null && jogosultsagInformaciok.Any())
                //{
                //    var groupBy = jogosultsagInformaciok.GroupBy(ji => ji.BvIntezetId);


                //    var legtobbetHasznalt = groupBy.OrderBy(g => g.Count()).ThenBy(g => g.Key).LastOrDefault();

                //    if (legtobbetHasznalt != null && legtobbetHasznalt.Key.HasValue)
                //    {
                //        //AktualisIntezet = IntezetTarolo.Table.Where(x => x.Azonosito == "200").FirstOrDefault();

                //        JogosultsagCacheFunctions.AktualisIntezet = IntezetTarolo.Table.Where(x => x.Id == legtobbetHasznalt.Key.Value).FirstOrDefault();
                //        AlkalmazasKontextus.Kontextus.RogzitoIntezetId = JogosultsagCacheFunctions.AktualisIntezet.Id;
                //        //-LogGenerator.LogToFile("AktualisIntezet:" + AktualisIntezet.ToString(), 1);
                //    }

                //}
                //else
                //{
                JogosultsagCacheFunctions.AktualisIntezet = JogosultsagCacheFunctions.JogosultIntezetek.FirstOrDefault();
                if (JogosultsagCacheFunctions.AktualisIntezet != null)
                {
                    AlkalmazasKontextus.Kontextus.RogzitoIntezetId = JogosultsagCacheFunctions.AktualisIntezet.Id;

                }
                //-LogGenerator.LogToFile("AktualisIntezet:" + AktualisIntezet.ToString(), 1);
                //}
            }
            JogosultsagCacheFunctions.Torol();
        }

        public bool VanJogosultsaga(Jogosultsagok jogosultsagAzonosito)
        {
            var jogosultsagInformacio = VanJogosultsaga(jogosultsagAzonosito.ToString());

            return jogosultsagInformacio;
        }

        public bool VanJogosultsaga(string jogosultsagAzonosito)
        {
            var sid = AlkalmazasKontextus.Kontextus.AdUserIdentity;
            var muveletTargyBvIntId = AlkalmazasKontextus.Kontextus.RogzitoIntezetId;
            return VanJogosultsaga(sid, jogosultsagAzonosito, (int?)muveletTargyBvIntId);
        }

        public bool VanJogosultsaga(WindowsIdentity sid, string jogosultsagAzonosito, int? muveletTargyBvIntId)
        {

            //JogosultsagInformacio jogosultsagInformacio = null;
            //using (HostingEnvironment.Impersonate())
            //{

            //    if (sid == null)
            //        sid = WindowsIdentity.GetCurrent();
            //    if (JogosultsagCacheFunctions.AktualisIntezet != null)
            //    {
            //        if (JogosultsagCacheFunctions.Inicializalatlan())
            //        {
            //            Belepes(sid, AlkalmazasKontextus.Kontextus.ClientHostName);
            //            muveletTargyBvIntId = AlkalmazasKontextus.Kontextus.RogzitoIntezetId;
            //        }
            //        jogosultsagInformacio = JogosultsagCacheFunctions.KeresesJogosultsagInformacioCacheben(sid.User.Value,
            //                                                                                      jogosultsagAzonosito,
            //                                                                                      muveletTargyBvIntId);
            //    }
            //    if (jogosultsagInformacio == null)
            //    {
            //        jogosultsagInformacio = new JogosultsagInformacio(sid.User.Value, null, null, jogosultsagAzonosito,
            //                                                          muveletTargyBvIntId, false, false);
            //        JogosultsagCacheFunctions.HozzaadJogosultsagInformacioCachehez(jogosultsagInformacio);
            //    }


            //}
#if DEBUG
            bool vanJogosultsag = true;
#else
            
            bool vanJogosultsag = false;
            jogosultsagAzonosito = jogosultsagAzonosito.ToLower();
            if (JogosultsagCacheFunctions.UserJogosultsagok != null)
            {
                if (JogosultsagCacheFunctions.UserJogosultsagok.ContainsKey(jogosultsagAzonosito))
                {
                    vanJogosultsag = JogosultsagCacheFunctions.UserJogosultsagok[jogosultsagAzonosito].Contains(AlkalmazasKontextus.Kontextus.RogzitoIntezetId);
                }
            }
#endif
            //Log.Debug(String.Format("Vanjogosultsaga szemelyzetId: {0}, jogosultság: {1}, eredmény: {2}", AlkalmazasKontextus.Kontextus.SzemelyzetId, jogosultsagAzonosito, vanJogosultsag));




            return vanJogosultsag;
        }
        public bool VanJogosultsaga(Jogosultsagok jogosultsagAzonosito, int bvIntId)
        {

#if DEBUG
            bool vanJogosultsag = true;
#else

            bool vanJogosultsag = false;
            var jogosultsagAzonositoStr = jogosultsagAzonosito.ToString().ToLower();
            if (JogosultsagCacheFunctions.UserJogosultsagok != null)
            {
                if (JogosultsagCacheFunctions.UserJogosultsagok.ContainsKey(jogosultsagAzonositoStr))
                {
                    vanJogosultsag = JogosultsagCacheFunctions.UserJogosultsagok[jogosultsagAzonositoStr].Contains(bvIntId);
                }
            }
#endif
            //Log.Debug(String.Format("Vanjogosultsaga szemelyzetId: {0}, jogosultság: {1}, eredmény: {2}", AlkalmazasKontextus.Kontextus.SzemelyzetId, jogosultsagAzonosito, vanJogosultsag));




            return vanJogosultsag;
        }

        public bool VanJogosultsaga(string sid, Jogosultsagok jogosultsagAzonosito, int? bvIntId)
        {
            var vanJogosultsaga = false;
#if DEBUG 
            vanJogosultsaga = true;
#else
            int intezetId;
            string azonosito;
            if (bvIntId.HasValue)
            {
                var intezet = IntezetFunctions.FindById(bvIntId.Value);
                intezetId = intezet.Id;
                azonosito = intezet.Azonosito2.ToUpper();
            } 
            else
            {
                intezetId = JogosultsagCacheFunctions.AktualisIntezet.Id;
                azonosito = JogosultsagCacheFunctions.AktualisIntezet.Azonosito2.ToUpper();
            }
            var jogosultsagAzonositoStr = jogosultsagAzonosito.ToString();
            if (JogosultsagCacheFunctions.Inicializalatlan())
            {
                JogosultsagCacheFunctions.Torol();
            }
            var informacio = JogosultsagCacheFunctions.KeresesJogosultsagInformacioCacheben(sid, jogosultsagAzonositoStr, bvIntId);
            if (informacio == null)
            {
                var adGroupList = this.FelhasznaloAdGroupLista(sid);
                var groupName = $"{azonosito}-FN-{jogosultsagAzonositoStr.Replace('_', '-')}";
                var group = adGroupList.FirstOrDefault(x => x.Name == groupName);
                vanJogosultsaga = group != null;
                JogosultsagCacheFunctions.HozzaadJogosultsagInformacioCachehez(new JogosultsagInformacio(sid, group?.Sid?.ToString() ?? "", "", jogosultsagAzonositoStr, intezetId, vanJogosultsaga, false));
            }
            else
            {
                vanJogosultsaga = informacio.VanJogosultsaga;
            }
#endif
            return vanJogosultsaga;
        }



        private IDictionary<string, int> IntezetAzonosito2IdMegfeleltetes(string logToFile)
        {
            IDictionary<string, int> intezetAzonosito2IdMegfeleltetes = new Dictionary<string, int>();
            var table = IntezetTarolo.Table.AsNoTracking().ToList();
            foreach (var item in table)
            {
                if (item.Azonosito2 != null)
                    item.Azonosito2 = item.Azonosito2.ToLower();
            }
            foreach (var elem in table.Where(i => i.Azonosito2 != null))
            {
                if (!intezetAzonosito2IdMegfeleltetes.ContainsKey(elem.Azonosito2))
                    intezetAzonosito2IdMegfeleltetes.Add(elem.Azonosito2, elem.Id);
            }
            return intezetAzonosito2IdMegfeleltetes;
        }

        public void JogosultsagBeallitasokTorlese()
        {
            JogosultsagCacheFunctions.Torol();
        }

        public void Belepes(WindowsIdentity sid, string clientHostName)
        {
            try
            {
                var sidStr = sid.User.Value;
                //-LogGenerator.LogToFile("Belepes: " + sidStr, 1);
#if !DEBUG
                // User host-jának ellenőrzése
                clientHostName = clientHostName.Contains(".")
                                      ? clientHostName.Substring(0, clientHostName.IndexOf('.'))
                                      : clientHostName;
                //-LogGenerator.LogToFile("clientHostName: " + clientHostName, 1);
                //if ( !ADTarolo.IsDcHost(clientHostName) )
                //{
                //    throw new AzonosithatatlanKliensGepKivetel(clientHostName);
                //}

                // User ellenőrzése


                // User AD csoporttagságai                
#endif
                var kontextus = AlkalmazasKontextus.Kontextus ?? new AlkalmazasKontextus();
                kontextus.RogzitoIntezetId = JogosultsagCacheFunctions.AktualisIntezet.Id;

                AlkalmazasKontextus.Kontextus = kontextus;
                List<GroupPrincipal> groupPrincipalLista = ADTarolo.ListazasUserGroupPrincipal(sid.User.Value);

                Szemelyzet szemelyzet = this.SzemelyzetTarolo.SzemelyzetLekeresVagyLetrehozas(sidStr, sid.Name);
                kontextus.AdUserIdentity = sid;
                kontextus.SzemelyzetId = szemelyzet.Id;
                kontextus.SzemelyzetSid = szemelyzet.AdSid;
                kontextus.SzemelyzetNev = szemelyzet.Nev;

                var trKontextus = new TranzakcioAdatkontextus();
                trKontextus.RogzitoIntezetId = JogosultsagCacheFunctions.AktualisIntezet.Id;
                trKontextus.SzemelyzetId = szemelyzet.Id;
                //TranzakcioAdatKontextus.Kontextus.SzemelyzetId = szemelyzet.Id;
                //TranzakcioAdatKontextus.Kontextus.RogzitoIntezetId = JogosultsagCacheFunctions.AktualisIntezet.Id;

                TranzakcioAdatKontextus.Kontextus = trKontextus;

                //-LogGenerator.LogToFile("RogzitoIntezetId: " + AlkalmazasKontextus.RogzitoIntezetId.ToString(), 1);

                // Jogosultság cache feltöltése, belépési jogosultságok logolása

                ListazasJogosultsagInformacioAdatbazisbol(sid.User, groupPrincipalLista);

                //BelepesJogosultsagTarolo.Save();
                // Üzenetküldés a belépésről
                //AlkalmazasUzenetKezeles.UzenetKuldesSzemelynek(AlkalmazasKontextus.SzemelyzetId,
                //                               AlkalmazasKontextus.SzemelyzetId,
                //                               AlkalmazasKontextus.RogzitoIntezetId, 1,
                //                               "Belépés:"+sid.Name+";"+szemelyzet.Nev,
                //                               uzenet);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void BelepesCsakActiveDirectoryOlvasassal(WindowsIdentity sid, string clientHostName, int intezetId)
        {
            try
            {
                var intezetLista = JogosultsagCacheFunctions.JogosultIntezetek;

                var sidStr = sid.User.Value;

                var kontextus = AlkalmazasKontextus.Kontextus ?? new AlkalmazasKontextus();
                kontextus.RogzitoIntezetId = intezetId;
                kontextus.AdUserIdentity = sid;
                JogosultsagCacheFunctions.AktualisIntezet = intezetLista.Single(f => f.Id == intezetId);
                var szemelyzet = InjectionKernel.Instance.GetInstance<ISzemelyzetFunctions>().SzemelyzetLekeresVagyLetrehozas(sidStr, sid.Name, intezetId);
                kontextus.SzemelyzetId = szemelyzet.Id;
                kontextus.SzemelyzetSid = szemelyzet.AdSid;
                kontextus.SzemelyzetLoginNev = ActiveDirectoryFunctions.KeresSzemelyzetLoginNev(sidStr);
                kontextus.SzemelyzetNev = szemelyzet.Nev;
              
                var trKontextus = new TranzakcioAdatkontextus
                {
                    RogzitoIntezetId = intezetId,
                    SzemelyzetId = szemelyzet.Id,
                    RogzitoSzemelyLoginNev = kontextus.SzemelyzetLoginNev,
                    RogzitoSzemelyNev = szemelyzet.Nev,
                    RogzitoSzemelySid = szemelyzet.AdSid
                };

                AlkalmazasKontextus.Kontextus = kontextus;
                TranzakcioAdatKontextus.Kontextus = trKontextus;
            }
            catch (Exception e)
            {
                Log.Error("BelepesCsakActiveDirectoryOlvasassal ERROR: ", e);
                throw;
            }
        }

       
        public void BelepesActiveDirectoryOlvasassal(UserPrincipal user, string clientHostName, int intezetId)
        {
            try
            {
                var intezetLista = JogosultsagCacheFunctions.JogosultIntezetek;

                var sidStr = user.Sid.Value;

                var kontextus = AlkalmazasKontextus.Kontextus ?? new AlkalmazasKontextus();
                kontextus.RogzitoIntezetId = intezetId;
                //kontextus.AdUserIdentity = user.;
                JogosultsagCacheFunctions.AktualisIntezet = intezetLista.Single(f => f.Id == intezetId);
                var szemelyzet = SzemelyzetTarolo.SzemelyzetLekeresVagyLetrehozas(sidStr, user.Name, intezetId);
                kontextus.SzemelyzetId = szemelyzet.Id;
                kontextus.SzemelyzetSid = szemelyzet.AdSid;
                kontextus.SzemelyzetLoginNev = user.SamAccountName;
                kontextus.SzemelyzetNev = szemelyzet.Nev;
                
                var trKontextus = new TranzakcioAdatkontextus
                {
                    RogzitoIntezetId = intezetId,
                    SzemelyzetId = szemelyzet.Id,
                    RogzitoSzemelyLoginNev = kontextus.SzemelyzetLoginNev,
                    RogzitoSzemelyNev = szemelyzet.Nev,
                    RogzitoSzemelySid = szemelyzet.AdSid
                };

                AlkalmazasKontextus.Kontextus = kontextus;
                TranzakcioAdatKontextus.Kontextus = trKontextus;
            }
            catch (Exception e)
            {
                Log.Error("BelepesCsakActiveDirectoryOlvasassal ERROR: ", e);
                throw;
            }
        }

        public List<Intezet> IntezetListaLegyujtes(string sid)
        {
            List<Intezet> intezetLista = new List<Intezet>();
#if DEBUG
            var groupLista = FelhasznaloAdGroupLista(sid);
            List<DebugGroupPrincipal> debugGroupLista = groupLista.Cast<DebugGroupPrincipal>().ToList();
            return IntezetListaLegyujtesDebug(debugGroupLista);
#else
            var groupLista = FelhasznaloAdGroupLista(sid);
            return IntezetListaLegyujtes(groupLista);
#endif
            return intezetLista;
        }

        List<Intezet> tmpIntezetek;

        private List<Intezet> IntezetListaLegyujtesDebug(List<DebugGroupPrincipal> groupLista)
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (JogosultsagCacheFunctions.EngedelyezettIntezetek != null)
                return tmpIntezetek;
            var engedelyezettIntezetek = new Dictionary<string, int>();
            List<Intezet> intezetLista = new List<Intezet>();

            var gl2 = groupLista.Where(gl => gl.Name.ToLower() != "bvop-fonix" && gl.Name.ToLower() != "bvop-fn" && gl.Name.ToLower() != "bvop-fno"
                                             && (gl.Name.ToLower().Contains("-fn") || gl.Name.ToLower().Contains("-fonix")));
            List<string> intezetAzon2Lista =
                groupLista.Where(gl => gl.Name.ToLower() != "bvop-fonix" && gl.Name.ToLower() != "bvop-fn" && gl.Name.ToLower() != "bvop-fno"
                                       && (gl.Name.ToLower().Contains("-fn") || gl.Name.ToLower().Contains("-fonix"))).Select(
                                           gl => gl.Name.ToLower().Substring(0, 4)).Distinct().ToList();

            JogosultsagCacheFunctions.Szerepkorok = new Dictionary<string, HashSet<string>>();
            foreach (var item in groupLista)
            {
                string[] list = item.Name.ToLower().Split(new string[] { "-fn-" }, StringSplitOptions.RemoveEmptyEntries);
                if (list.Length == 2)
                {
                    if (!JogosultsagCacheFunctions.Szerepkorok.ContainsKey(list[1]))
                        JogosultsagCacheFunctions.Szerepkorok[list[1]] = new HashSet<string>();
                    JogosultsagCacheFunctions.Szerepkorok[list[1]].Add(list[0]);
                }
            }

            var table = IntezetTarolo.Table.AsNoTracking().ToList();
            foreach (var item in table)
            {
                if (item.Azonosito2 != null)
                    item.Azonosito2 = item.Azonosito2.ToLower();
            }
            foreach (string azon in intezetAzon2Lista)
            {
                foreach (var elem in table.Where(x => x.Azonosito2 == azon).Where(e => e.MegszunesDatum == null))
                {
                    intezetLista.Add(elem);
                    engedelyezettIntezetek[elem.Azonosito2] = elem.Id;
                }
            }
            JogosultsagCacheFunctions.EngedelyezettIntezetek = engedelyezettIntezetek;
            sw.Stop();
            return intezetLista;
        }

        private List<Intezet> IntezetListaLegyujtes(List<GroupPrincipal> groupLista)
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (JogosultsagCacheFunctions.EngedelyezettIntezetek != null)
                return tmpIntezetek;
            var jogosultIntezetek = new Dictionary<string, int>();
            List<Intezet> intezetLista = new List<Intezet>();
            //IList<string> intezetAzon2Lista =
            //    groupLista.Where(gl => gl.Name != "BVOP-FONIX" && gl.Name != "BVOP-FN" && gl.Name != "BVOP-FNO"
            //                           && (gl.Name.Contains("-FN"))).Select(
            //                               gl => gl.Name.Substring(0, 4)).Distinct().ToList();
            HashSet<string> intezetAzon2Lista = new HashSet<string>();
            JogosultsagCacheFunctions.Szerepkorok = new Dictionary<string, HashSet<string>>();

            string szerepkorConfig = ConfigurationManager.AppSettings["modulSzerepkorPrefix"];

            string[] prefixes = szerepkorConfig == null ? new string[0] : szerepkorConfig.ToLower().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in groupLista)
            {
                string[] list = item.Name.ToLower().Split(new string[] { "-fn-" }, StringSplitOptions.RemoveEmptyEntries);
                if (list.Length == 2)
                {

                    if (prefixes.Length == 0 || prefixes.Any(x => list[1].StartsWith(x)))
                    {
                        if (!JogosultsagCacheFunctions.Szerepkorok.ContainsKey(list[1]))
                        {
                            JogosultsagCacheFunctions.Szerepkorok.Add(list[1], new HashSet<string>());

                        }
                        JogosultsagCacheFunctions.Szerepkorok[list[1]].Add(list[0]);
                        if (!intezetAzon2Lista.Contains(list[0]))
                        {
                            intezetAzon2Lista.Add(list[0]);
                        }

                    }
                }
            }

            var table = IntezetTarolo.Table.AsNoTracking().ToList();
            foreach (var item in table)
            {
                if (item.Azonosito2 != null)
                    item.Azonosito2 = item.Azonosito2.ToLower();
            }
            foreach (string azon in intezetAzon2Lista)
            {
                foreach (var elem in table.Where(x => x.Azonosito2 == azon).Where(e => e.MegszunesDatum == null))
                {
                    intezetLista.Add(elem);

                    jogosultIntezetek.Add(elem.Azonosito2, elem.Id);
                }
            }


            JogosultsagCacheFunctions.EngedelyezettIntezetek = jogosultIntezetek;
            tmpIntezetek = (List<Intezet>)intezetLista;
            sw.Stop();
            return intezetLista;
        }

        /*public IList<SzemelyzetCsoport> ListazasSzemelyzetCsoportok(WindowsIdentity sid)
        {
            IEnumerable<IdentityReference> groupPrincipals = sid.Groups.ToList();
            return groupPrincipals.Select(groupPrincipal => CsoportTarolo.KeresesByAdSid(groupPrincipal.Value))
                                  .Where(szemelyzetiCsoport => szemelyzetiCsoport != null).ToList();
        }*/

        #region ActiveDirectory szolgáltatások
        public UserPrincipal BetoltesUserPrincipal(string sid)
        {
            return ADTarolo.BetoltesUserPrincipal(sid);
        }

        public List<GroupPrincipal> FelhasznaloAdGroupLista(string sid)
        {
            return ADTarolo.ListazasUserGroupPrincipal(sid);
        }

        public List<GroupPrincipal> ListazasGroupPrincipal()
        {
            var jogosultsagInformacio = VanJogosultsaga(Jogosultsagok.ARIndex.ToString());
            var intezetId = AlkalmazasKontextus.Kontextus.RogzitoIntezetId;
            var ouString = (intezetId != null) ? IntezetTarolo.Table.Where(x => x.Id == (int)intezetId).FirstOrDefault().Azonosito2 : null;
            var groupLista = ADTarolo.ListazasGroupPrincipal(ouString);
            return groupLista;
        }
        #endregion ActiveDirectory szolgáltatások

        #region FANY adatbázis szolgáltatások
        //public void TranzakcioInditas()
        //{
        //    TranzakcioNyitas();
        //    TranzakcioTarolo.AdatbazisTranzakcioNyitas();
        //}

        //public void TranzakcioMentes()
        //{
        //    SzerepkorTarolo.Save();
        //    CsoportTarolo.Save();
        //    BelepesJogosultsagTarolo.Save();
        //    TranzakcioTarolo.AdatbazisTranzakcioCommit();
        //}

        //public IList<SzemelyzetCsoport> ListazasIntezetSzemelyzetCsoportjai(int intezetId)
        //{
        //    var intezet = IntezetTarolo.Kereses(intezetId);
        //    return CsoportTarolo.Table.Where(e => e.Azonosito.StartsWith(intezet.Azonosito + '_')).ToList();
        //}

        //public IList<SzemelyzetSzerepkor> ListazasIntezetSzerepkorei(int intezetId)
        //{
        //    var intezet = IntezetTarolo.Kereses(intezetId);
        //    return SzerepkorTarolo.Table.Where(e => e.Azonosito.StartsWith(intezet.Azonosito + '_')).ToList();
        //}

        //public IList<SzemelyzetSzerepkor> ListazasCsoportSzerepkorei(string sid)
        //{
        //    var csoport = CsoportTarolo.KeresesByAdSid(sid);
        //    var csoportSzerepkorok = csoport.CsoportSzerepkorok;
        //    return csoportSzerepkorok.Select(csoportSzerepkor => csoportSzerepkor.SzemelyzetSzerepkor).ToList();
        //}

        //public IList<SzemelyzetJogosultsag> ListazasSzerepkorJogosultsagai(int szerepkorId)
        //{
        //    var szerepkor = SzerepkorTarolo.Kereses(szerepkorId);
        //    var szerepkorJogosultsagok = szerepkor.SzerepkorJogosultsagok;
        //    return szerepkorJogosultsagok.Select(csoportSzerepkor => csoportSzerepkor.SzemelyzetJogosultsag).ToList();
        //}

        public void ListazasJogosultsagInformacioAdatbazisbol(WindowsIdentity sid)
        {
            ListazasJogosultsagInformacioAdatbazisbol(sid.User, ADTarolo.ListazasUserGroupPrincipal(sid.User.Value));
        }

        List<JogosultsagInformacio> tmpJogosultsag;

        private void ListazasJogosultsagInformacioAdatbazisbol(SecurityIdentifier sid, IList<GroupPrincipal> groupPrincipalok)
        {

            //IDictionary<string, int> intezetAzonosito2IdMegfeleltetes = new Dictionary<string, int>();

            //foreach (var elem in IntezetTarolo.Table.Where(i => i.Azonosito2 != null))
            //{
            //    if (!intezetAzonosito2IdMegfeleltetes.ContainsKey(elem.Azonosito2))
            //        intezetAzonosito2IdMegfeleltetes.Add(elem.Azonosito2, elem.Id);
            //}

            //var josoultsagIterator = new SzemelyzetJogosultsagok(SzemelyzetTarolo.FindBySid(sid.Value),
            //                                                     groupPrincipalok,
            //                                                     SzerepkorTarolo.Table.ToList(), intezetAzonosito2IdMegfeleltetes, false, JogosultsagCacheFunctions.AktualisIntezet.Azonosito2);

            //var list = josoultsagIterator.ToList();

            Stopwatch sw = Stopwatch.StartNew();
            if (JogosultsagCacheFunctions.UserJogosultsagok != null)
                return;

            var userJogosultsagok = new Dictionary<string, HashSet<int>>();

            var jogosultsagok = KonasoftBVFonixContext.Database.SqlQuery<SzerepkorJogosultsag>
                 ("GET_SZEREPKOR_JOGOSULTSAG ").ToList();
            foreach (var item in jogosultsagok)
            {
                item.Jogosultsag = item.Jogosultsag.ToLower();
                item.Szerepkor = item.Szerepkor.ToLower();
            }
            //Log.Debug("szerepkörök");
            //foreach (var item in JogosultsagCacheFunctions.Szerepkorok)
            //{
            //    Log.Debug(item.Value + "->" + item.Value);
            //}

            // jogosultság lista feltöltése
            foreach (var item in jogosultsagok)
            {
                // csak olyan jogosultságot tárolunk amihez van is szerepköre a felhasználónak
                if (JogosultsagCacheFunctions.Szerepkorok.ContainsKey(item.Szerepkor))
                {
                    if (!userJogosultsagok.ContainsKey(item.Jogosultsag))
                    {
                        userJogosultsagok.Add(item.Jogosultsag, new HashSet<int>());
                    }
                    // a jogosultsághoz minden intézet id-t másolunk
                    foreach (var azonkod in JogosultsagCacheFunctions.Szerepkorok[item.Szerepkor])
                    {
                        if (JogosultsagCacheFunctions.EngedelyezettIntezetek.ContainsKey(azonkod))
                        {
                            userJogosultsagok[item.Jogosultsag].Add(JogosultsagCacheFunctions.EngedelyezettIntezetek[azonkod]);
                        }
                    }
                    //Log.Debug(item.Szerepkor + " " + item.Jogosultsag);
                }
            }
            JogosultsagCacheFunctions.UserJogosultsagok = userJogosultsagok;
            sw.Stop();
            //Log.Info("Jogosultság felépítése:" + sw.ElapsedMilliseconds + "ms");


        }
        #endregion FANY adatbázis szolgáltatások

        //#region Fany DB eljárások

        //#region Szemelyzet eljárások
        //private void SzemelyzetLetrehozas(Szemelyzet szemelyzet)
        //{
        //    BeallitLetrehozashoz(szemelyzet);
        //    SzemelyzetTarolo.Uj(szemelyzet);
        //}

        //private void SzemelyzetModositas(Szemelyzet regiSzemelyzet, Szemelyzet ujSzemelyzet)
        //{
        //    BeallitModositashoz(regiSzemelyzet);
        //    SzemelyzetTarolo.Modosit(regiSzemelyzet, ujSzemelyzet);
        //}

        //public Szemelyzet UjSzemelyzetPeldanyKeres()
        //{
        //    return SzemelyzetTarolo.UjLetrehozasa();
        //}
        //#endregion Szemelyzet eljárások

        //#region SzemelyzetCsoport eljárások
        //public IList<SzemelyzetCsoport> SzemelyzetCsoportListazas()
        //{
        //    return CsoportTarolo.Mind;
        //}

        //public SzemelyzetCsoport SzemelyzetCsoportKereses(int id)
        //{
        //    return CsoportTarolo.Kereses(id);
        //}

        //public void SzemelyzetCsoportLetrehozas(SzemelyzetCsoport szemelyzetCsoport)
        //{
        //    szemelyzetCsoport.ErvenyessegKezdete = DateTime.Now;
        //    szemelyzetCsoport.Torolve = 0;
        //    szemelyzetCsoport.FelvevoTranzakcioId = AlkalmazasKontextus.TranzakcioId;
        //    szemelyzetCsoport.ModositoTranzakcioId = AlkalmazasKontextus.TranzakcioId;
        //    CsoportTarolo.Uj(szemelyzetCsoport);
        //}

        //public void SzemelyzetCsoportModositas(SzemelyzetCsoport szemelyzetCsoport)
        //{
        //    szemelyzetCsoport.ErvenyessegKezdete = DateTime.Now;
        //    szemelyzetCsoport.ModositoTranzakcioId = AlkalmazasKontextus.TranzakcioId;
        //    CsoportTarolo.Modosit(szemelyzetCsoport);
        //}

        //public SzemelyzetCsoport UjSzemelyzetCsoportPeldanyKeres()
        //{
        //    return CsoportTarolo.UjLetrehozasa();
        //}

        //public void SzemelyzetCsoportTorles(SzemelyzetCsoport szemelyzetCsoport)
        //{
        //    szemelyzetCsoport.ErvenyessegKezdete = DateTime.Now;
        //    szemelyzetCsoport.ModositoTranzakcioId = AlkalmazasKontextus.TranzakcioId;
        //    szemelyzetCsoport.Torolve = 1;
        //    CsoportTarolo.Torol(szemelyzetCsoport);
        //}

        //public void SzemelyzetCsoportTorlesVisszavonas(SzemelyzetCsoport szemelyzetCsoport)
        //{
        //    szemelyzetCsoport.ErvenyessegKezdete = DateTime.Now;
        //    szemelyzetCsoport.ModositoTranzakcioId = AlkalmazasKontextus.TranzakcioId;
        //    szemelyzetCsoport.Torolve = 0;
        //    CsoportTarolo.Modosit(szemelyzetCsoport);
        //}
        //#endregion SzemelyzetCsoport eljárások


        //#region szemelyzetCsoportSzerepkor eljárások
        //public void SzemelyzetCsoportSzerepkorLetrehozas(SzemelyzetCsoportSzerepkor szemelyzetCsoportSzerepkor)
        //{
        //    szemelyzetCsoportSzerepkor.ErvenyessegKezdete = DateTime.Now;
        //    szemelyzetCsoportSzerepkor.Torolve = 0;
        //    szemelyzetCsoportSzerepkor.FelvevoTranzakcioId = AlkalmazasKontextus.TranzakcioId;
        //    szemelyzetCsoportSzerepkor.ModositoTranzakcioId = AlkalmazasKontextus.TranzakcioId;
        //    CsoportTarolo.Uj(szemelyzetCsoportSzerepkor);
        //}

        //public void SzemelyzetCsoportSzerepkorModositas(SzemelyzetCsoportSzerepkor szemelyzetCsoportSzerepkor)
        //{
        //    szemelyzetCsoportSzerepkor.ErvenyessegKezdete = DateTime.Now;
        //    szemelyzetCsoportSzerepkor.ModositoTranzakcioId = AlkalmazasKontextus.TranzakcioId;
        //    CsoportTarolo.Modosit(szemelyzetCsoportSzerepkor);
        //}

        //public void SzemelyzetCsoportSzerepkorTorles(SzemelyzetCsoportSzerepkor szemelyzetCsoportSzerepkor)
        //{
        //    szemelyzetCsoportSzerepkor.ErvenyessegKezdete = DateTime.Now;
        //    szemelyzetCsoportSzerepkor.ModositoTranzakcioId = AlkalmazasKontextus.TranzakcioId;
        //    szemelyzetCsoportSzerepkor.Torolve = 1;
        //    CsoportTarolo.Torol(szemelyzetCsoportSzerepkor);
        //}

        //public void SzemelyzetCsoportSzerepkorTorlesVisszavonas(SzemelyzetCsoportSzerepkor szemelyzetCsoportSzerepkor)
        //{
        //    szemelyzetCsoportSzerepkor.ErvenyessegKezdete = DateTime.Now;
        //    szemelyzetCsoportSzerepkor.ModositoTranzakcioId = AlkalmazasKontextus.TranzakcioId;
        //    szemelyzetCsoportSzerepkor.Torolve = 0;
        //    CsoportTarolo.Modosit(szemelyzetCsoportSzerepkor);
        //}

        //public SzemelyzetCsoportSzerepkor UjSzemelyzetCsoportSzerepkorPeldanyKeres()
        //{
        //    return CsoportTarolo.UjSzemelyzetCsoportSzerepkorLetrehozasa();
        //}
        //#endregion szemelyzetCsoportSzerepkor eljárások


        //#region SzemelyzetSzerepkor eljárások
        //public IList<SzemelyzetSzerepkor> SzemelyzetSzerepkorListazas()
        //{
        //    return SzerepkorTarolo.Mind;
        //}

        //public SzemelyzetSzerepkor SzemelyzetSzerepkorKereses(int id)
        //{
        //    return SzerepkorTarolo.Kereses(id);
        //}

        //public void SzemelyzetSzerepkorLetrehozas(SzemelyzetSzerepkor szemelyzetSzerepkor)
        //{
        //    SzerepkorTarolo.Uj(szemelyzetSzerepkor);
        //}

        //public void SzemelyzetSzerepkorModositas(SzemelyzetSzerepkor szemelyzetSzerepkor)
        //{
        //    szemelyzetSzerepkor.ModositoTranzakcioId = AlkalmazasKontextus.TranzakcioId;
        //    SzerepkorTarolo.Modosit(szemelyzetSzerepkor);
        //}

        //public SzemelyzetSzerepkor UjSzemelyzetSzerepkorPeldanyKeres()
        //{
        //    return SzerepkorTarolo.UjLetrehozasa();
        //}

        //public void SzemelyzetSzerepkorTorles(SzemelyzetSzerepkor szemelyzetSzerepkor)
        //{
        //    szemelyzetSzerepkor.Torolve = 1;
        //    SzerepkorTarolo.Torol(szemelyzetSzerepkor);
        //}

        //public void SzemelyzetSzerepkorTorlesVisszavonas(SzemelyzetSzerepkor szemelyzetSzerepkor)
        //{
        //    szemelyzetSzerepkor.Torolve = 0;
        //    SzerepkorTarolo.Modosit(szemelyzetSzerepkor);
        //}
        //#endregion SzemelyzetSzerepkor eljárások


        //#region SzemelyzetSzerepkorJogosultsag eljárások


        //public void SzemelyzetSzerepkorJogosultsagLetrehozas(SzemelyzetSzerepkorJogosultsag szemelyzetSzerepkorJogosultsag)
        //{
        //    if (szemelyzetSzerepkorJogosultsag == null)
        //        return;

        //    SzerepkorTarolo.Uj(szemelyzetSzerepkorJogosultsag);
        //}

        //public void SzemelyzetSzerepkorJogosultsagModositas(SzemelyzetSzerepkorJogosultsag szemelyzetSzerepkorJogosultsag)
        //{
        //    SzerepkorTarolo.Modosit(szemelyzetSzerepkorJogosultsag);
        //}

        //public void SzemelyzetSzerepkorJogosultsagTorles(SzemelyzetSzerepkorJogosultsag szemelyzetSzerepkorJogosultsag)
        //{
        //    szemelyzetSzerepkorJogosultsag.Torolve = 1;
        //    SzerepkorTarolo.Torol(szemelyzetSzerepkorJogosultsag);
        //}

        //public void SzemelyzetSzerepkorJogosultsagTorlesVisszavonas(SzemelyzetSzerepkorJogosultsag szemelyzetSzerepkorJogosultsag)
        //{
        //    szemelyzetSzerepkorJogosultsag.Torolve = 0;
        //    SzerepkorTarolo.Modosit(szemelyzetSzerepkorJogosultsag);
        //}

        //public SzemelyzetSzerepkorJogosultsag UjSzemelyzetSzerepkorJogosultsagPeldanyKeres()
        //{
        //    return SzerepkorTarolo.UjSzemelyzetSzerepkorJogosultsagLetrehozasa();
        //}
        //#endregion SzemelyzetSzerepkorJogosultsag eljárások


        //#region SzemelyzetJogosultsag eljárások
        //public IList<SzemelyzetJogosultsag> SzemelyzetJogosultsagListazas()
        //{
        //    return JogosultsagTarolo.Mind;
        //}
        //#endregion SzemelyzetJogosultsag eljárások

        //#region SzemelyzetJogosultsagCsoportok eljárások
        //public IList<SzemelyzetJogosultsagCsoportok> SzemelyzetJogosultsagCsoportokListazas()
        //{
        //    return JogosultsagTarolo.ListazSzemelyzetJogosultsagCsoportok();
        //}
        //#endregion SzemelyzetJogosultsagCsoportok eljárások


        //#region AlkalmazasBelepesJogosultsagNaplo eljárások
        //public void AlkalmazasBelepesJogosultsagNaploLetrehozas(AlkalmazasBelepesJogosultsagNaplo AlkalmazasBelepesJogosultsagNaplo)
        //{

        //    AlkalmazasBelepesJogosultsagNaplo.AlkalmazasNaploId = AlkalmazasKontextus.AlkalmazasNaploId;
        //    BelepesJogosultsagTarolo.Uj(AlkalmazasBelepesJogosultsagNaplo);
        //}

        //public AlkalmazasBelepesJogosultsagNaplo UjAlkalmazasBelepesJogosultsagNaploPeldanyKeres()
        //{
        //    return BelepesJogosultsagTarolo.UjLetrehozasa();
        //}
        //#endregion AlkalmazasBelepesJogosultsagNaplo eljárások

        //#endregion Fany DB eljárások

        //#region segéd eljárások
        //#region adatbázis eljárások
        //private JogosultsagInformacio KeresesJogosultsagInformacioAdatbazisban(WindowsIdentity userAdSid, string jogosultsagAzonosito,
        //                                                                        int? muveletTargyBvIntId)
        //{
        //    Szemelyzet bvAlkalmazott = SzemelyzetTarolo.KeresesBySid(userAdSid.User.Value);
        //    /*     foreach (SzemelyzetCsoport szemelyzetCsoport in ListazasSzemelyzetCsoportok(userAdSid))
        //         {
        //             foreach (SzemelyzetCsoportSzerepkor szemelyzetCsoportSzerepkor in szemelyzetCsoport.CsoportSzerepkorok)
        //             {
        //                 SzemelyzetSzerepkor szerepkor = szemelyzetCsoportSzerepkor.SzemelyzetSzerepkor;
        //                 foreach (SzemelyzetSzerepkorJogosultsag szerepkorJogosultsag in szerepkor.SzerepkorJogosultsagok)
        //                 {
        //                     SzemelyzetJogosultsag jogosultsag = szerepkorJogosultsag.SzemelyzetJogosultsag;
        //                     if (jogosultsag.Azonosito == jogosultsagAzonosito
        //                         && (szerepkor.Globalis == 1                                  // Globális jogosultsága van
        //                             || jogosultsag.SzervezetiEgysegAlapu == 0                // Vagy ez nem szervezeti egyseg alapú jogosultsag
        //                             || muveletTargyBvIntId == bvAlkalmazott.IntezetId) )   // Vagy egyezik a személy és a művelet tárgyának az intézet azonosítója
        //                     {
        //                         return new JogosultsagInformacio(userAdSid.User.Value, szemelyzetCsoport.AdSid, szerepkor.Azonosito, 
        //                                                          jogosultsagAzonosito,
        //                                                          (szerepkor.Globalis == 1 || jogosultsag.SzervezetiEgysegAlapu == 0)? null : muveletTargyBvIntId,
        //                                                          true, szerepkor.Globalis == 1);
        //                     }
        //                 }
        //             }
        //         }*/
        //    return new JogosultsagInformacio(userAdSid.User.Value, null, null, jogosultsagAzonosito, muveletTargyBvIntId, false, false);
        //}

        //#endregion adatbázis eljárások
        //#endregion segéd eljárások




        #endregion eljárások

        public bool IsDebugPermissions(string sid)
        {
            //if (
            //    (ConfigurationManager.ConnectionStrings["KonasoftBVFonixContext"]?.ConnectionString?.ToLower().Contains("_teszt") ?? false)
            //    &&
            //    ConfigurationManager.AppSettings["IsOktatoRendszer"]?.ToLower() == "true"
            //    )
            //{
            //    return true;
            //}
            //var context = new KonasoftBVFonixContext();
            return true;//context.DebugSidek.Any(a => a.Sid == sid);
        }


        public Dictionary<string, HashSet<int>> GetUserJogosultsagokHaVanFeluldefinialttal(string userName)
        {
            var user = ActiveDirectoryFunctions.GetUserFromSamAccountName(userName);

#if RELEASE
            var jogSzuresEnabled = WebConfigurationManager.AppSettings["jogosultsagSzuresEnabled"] == "true";
            if (jogSzuresEnabled)
            {
                var feluldefinialtJogosultsagok = new F3JogosultsagFeluldefinialasFunctions().GetFeluldefinialtUserJogosultsagok(user.Sid.ToString());
                if (feluldefinialtJogosultsagok.Any())
                {
                    return feluldefinialtJogosultsagok;
                }
            }
#endif

            var userGroups = ActiveDirectoryFunctions.GetUserGroups(user.Sid.Value);
            return GetUserJogosultsagok(userGroups);
        }



        public Dictionary<string, HashSet<int>> GetUserJogosultsagok(List<GroupPrincipal> groupLista)
        {
            try
            {
                var intezetTable = KonasoftBVFonixContext.Intezet.ToList();

                var jogosultIntezetek = new Dictionary<string, int>();
                List<Entities.Fany.Intezet> intezetLista = new List<Entities.Fany.Intezet>();
                HashSet<string> intezetAzon2Lista = new HashSet<string>();

                var szerepkorok = new Dictionary<string, HashSet<string>>();
                var groupNameLista = groupLista.Select(s => s.Name).ToList();


                foreach (var item in groupNameLista)
                {
                    string[] listFn = item.ToLower().Split(new string[] { "-fn-" }, StringSplitOptions.RemoveEmptyEntries);
                    if (listFn.Length != 2) continue;

                    string[] list;
                    if (listFn.Length == 2)
                        list = listFn;
                    else
                        throw new Exception("Hiba a jogosultság feldolgozása közben");

                    list[1] = list[1].Replace("-", "_");
                    if (!szerepkorok.ContainsKey(list[1]))
                    {
                        szerepkorok.Add(list[1], new HashSet<string>());
                    }

                    szerepkorok[list[1]].Add(list[0]);
                    if (!intezetAzon2Lista.Contains(list[0]))
                    {
                        intezetAzon2Lista.Add(list[0]);
                    }
                }
                foreach (var item in intezetTable)
                {
                    if (item.Azonosito2 != null)
                        item.Azonosito2 = item.Azonosito2.ToLower();
                }

                foreach (var azon in intezetAzon2Lista)
                {
                    foreach (var elem in intezetTable.Where(x => x.Azonosito2 == azon))
                    {
                        intezetLista.Add(elem);

                        jogosultIntezetek.Add(elem.Azonosito2, elem.Id);
                    }
                }

                var jogosultsagok = new Dictionary<string, HashSet<int>>();

                foreach (var szerepkor in szerepkorok)
                {
                    jogosultsagok.Add(szerepkor.Key, new HashSet<int>());

                    foreach (var intezetAzon in szerepkor.Value)
                    {
                        var intezet = intezetTable.SingleOrDefault(x => x.Azonosito2 == intezetAzon);
                        if (intezet != null)
                        {
                            jogosultsagok[szerepkor.Key].Add(intezet.Id);
                        }
                    }
                }
                var modul = ConfigurationManager.AppSettings["modulSzerepkorPrefix"];

                //if (jogosultsagok.ContainsKey("f2020")) 
                //{
                //    intezetTable.ForEach(f => jogosultsagok["f2020"].Add(f.Id));                    
                //}
                if (modul == null)
                    return jogosultsagok;

                //szerepkörök
                JogosultsagCacheFunctions.Szerepkorok = new Dictionary<string, HashSet<string>>();
                foreach (var item in groupLista)
                {
                    string[] list = item.Name.ToLower().Split(new string[] { "-fn-" }, StringSplitOptions.RemoveEmptyEntries);
                    if (list.Length == 2)
                    {
                        if (!JogosultsagCacheFunctions.Szerepkorok.ContainsKey(list[1]))
                            JogosultsagCacheFunctions.Szerepkorok[list[1]] = new HashSet<string>();
                        JogosultsagCacheFunctions.Szerepkorok[list[1]].Add(list[0]);
                    }
                }

                var moduls = modul.ToLower().Split(',');


                //engedélyezett intézetek
                var engedelyezettIntezetek = new Dictionary<string, int>();

                var intezetTaroloTable = KonasoftBVFonixContext.Intezet.ToList().Select(x => new Intezet()
                {
                    Id = x.Id,
                    Azonosito = x.Azonosito.ToString(),
                    Azonosito2 = x.Azonosito2,
                    RovidNev = x.RovidNev,
                    KozepesNev = x.KozepesNev,
                    Nev = x.Nev
                });


                foreach (var item in intezetTaroloTable)
                {
                    if (item.Azonosito2 != null)
                        item.Azonosito2 = item.Azonosito2.ToLower();
                }
                foreach (string azon in intezetAzon2Lista)
                {
                    foreach (var elem in intezetTaroloTable.Where(x => x.Azonosito2 == azon).Where(e => e.MegszunesDatum == null))
                    {
                        //intezetLista.Add(elem);
                        engedelyezettIntezetek[elem.Azonosito2] = elem.Id;
                    }
                }
                JogosultsagCacheFunctions.EngedelyezettIntezetek = engedelyezettIntezetek;

                return jogosultsagok.Where(w => moduls.Any(x => w.Key.StartsWith(x))).ToDictionary(p => p.Key, p => p.Value);
            }
            catch (Exception e)
            {
                Log.Error("UserJogosultsagokFeldolgozasa - error", e);
                throw;
            }

        }
    }

}
