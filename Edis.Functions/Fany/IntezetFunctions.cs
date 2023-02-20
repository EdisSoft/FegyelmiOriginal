using Edis.Diagnostics;
using Edis.Entities.Fany;
using Edis.Functions.Base;
using Edis.IoC.Attributes;
using Edis.Utilities;
using Edis.ViewModels.Fany;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Linq;

namespace Edis.Functions.Fany
{
    public class IntezetFunctions : KonasoftBVFonixFunctionsBase<IntezetModel, Intezet>, IIntezetFunctions
    {

        [Inject]
        public IActiveDirectoryFunctions ActiveDirectoryFunctions { get; set; }

        public DbSet<Intezet> Table
        {
            get { return this.KonasoftBVFonixContext.Intezet; }
        }

        public Intezet KeresesById(int id)
        {
            return Table.Single(x => x.Id == id);
        }



        private int CountDaysOfWeek(DayOfWeek dayOfWeek, DateTime startDate, DateTime endDate)
        {

            while (startDate.DayOfWeek != dayOfWeek)
                startDate = startDate.AddDays(1);

            if (startDate > endDate) return 0;

            return endDate.Subtract(startDate).Days / 7 + 1;
        }

        public List<Intezet> IntezetListaLegyujtes(string sid, bool isNincsJogosolutsaga = false)
        {
            List<Intezet> intezetLista = new List<Intezet>();
#if DEBUG
            intezetLista = IntezetListaLegyujtes(new List<GroupPrincipal>(), sid);
#else
            var groupList = ActiveDirectoryFunctions.GetUserGroups(sid);
            if(isNincsJogosolutsaga)
            {
                intezetLista = IntezetListaLegyujtesJogosultsagokNelkul(groupList);                
            }
            else
            {
                intezetLista = IntezetListaLegyujtes(groupList, sid);
            }            
#endif
            return intezetLista;
        }

        public List<Intezet> IntezetListaLegyujtesAdCsoport(string sid)
        {
            List<Intezet> intezetLista = new List<Intezet>();
#if DEBUG
            intezetLista = IntezetListaLegyujtes(new List<GroupPrincipal>(), sid);
#else
            var groupList = new ActiveDirectoryFunctions(new ActiveDirectoryKezeloFunctions()).GetUserGroupsFromAd(sid);
            intezetLista = IntezetListaLegyujtes(groupList, sid);
#endif
            return intezetLista;
        }

        public List<Intezet> IntezetListaLegyujtesAdCsoport(UserPrincipal user)
        {
            List<Intezet> intezetLista = new List<Intezet>();
#if DEBUG
            intezetLista = IntezetListaLegyujtes(new List<GroupPrincipal>(), user.Sid.ToString());
#else
            var sid = user.Sid.ToString();
            var groupList = new ActiveDirectoryFunctions(new ActiveDirectoryKezeloFunctions()).GetUserGroupsFromAd(user);
            intezetLista = IntezetListaLegyujtes(groupList, sid);
#endif
            return intezetLista;
        }

        private List<Intezet> IntezetListaLegyujtes(List<GroupPrincipal> groupLista, string sid)
        {
            try
            {

#if DEBUG
                var intezetTable = Table
                    .Include(x => x.CimHelyseg)
                    .Include(x => x.IntezetiObjektumok.Select(i => i.CimHelyseg))
                    .ToList();
                return intezetTable;
#else
                var manipulationActiveDirectoryList = KonasoftBVFonixContext.ManipulationActiveDirectory.Where(w => w.Sid == sid).Select(s => s.Jogosultsag).ToList();
                
                if (manipulationActiveDirectoryList.Any())                    
                    return IntezetiListaLegyujtese(manipulationActiveDirectoryList);

                var groupNameLista = groupLista.Select(s => s.Name).ToList();
                return IntezetiListaLegyujtese(groupNameLista);
#endif

            }
            catch (Exception e)
            {
                Log.Error("IntezetListaLegyujtes - error", e);
                throw;
            }

        }

        private List<Intezet> IntezetiListaLegyujtese(List<string> groupNameLista)
        {
            var intezetTable = Table
                .Include(x => x.CimHelyseg)
                .Include(x => x.IntezetiObjektumok.Select(i => i.CimHelyseg))
                .AsNoTracking().ToList();
            intezetTable.Add(new Intezet()
            {
                Azonosito = "BM",
                Azonosito2 = "BM",
                Id = 500,
                Nev = "BM",
                RovidNev = "BM",
                KozepesNev = "BM"
            });

            var jogosultIntezetek = new Dictionary<string, int>();
            List<Intezet> intezetLista = new List<Intezet>();
            HashSet<string> intezetAzon2Lista = new HashSet<string>();

            var szerepkorok = new Dictionary<string, HashSet<string>>();

            string szerepkorConfig = ConfigurationManager.AppSettings["modulSzerepkorPrefix"];
            string[] prefixes = szerepkorConfig.ToLower().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in groupNameLista)
            {
                string[] list = item.ToLower().Split(new string[] { "-fn-" }, StringSplitOptions.RemoveEmptyEntries);

                if (list.Length != 2) continue;

                if (prefixes.Length != 0 && !prefixes.Any(x => list[1].StartsWith(x))) continue;

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
                foreach (var elem in intezetTable.Where(x => x.Azonosito2 == azon).Where(e => e.MegszunesDatum == null))
                {
                    intezetLista.Add(elem);

                    jogosultIntezetek.Add(elem.Azonosito2, elem.Id);
                }
            }
            var jogosultsagFunctions = new JogosultsagCacheFunctions();

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
            jogosultsagFunctions.UserJogosultsagok = jogosultsagok;
            return intezetLista;
        }

        private List<Intezet> IntezetListaLegyujtesJogosultsagokNelkul(List<GroupPrincipal> groupLista)
        {
            try
            {
                var intezetTable = Table
                    .Include(x => x.CimHelyseg)
                    .Include(x => x.IntezetiObjektumok.Select(i => i.CimHelyseg))
                    .AsNoTracking().ToList();

#if DEBUG
                return intezetTable;
#else
                var jogosultIntezetek = new Dictionary<string, int>();
                List<Intezet> intezetLista = new List<Intezet>();
                HashSet<string> intezetAzon2Lista;

                if (groupLista.Any(a => a.Name.Contains("-FN-") || a.Name.Contains("-FN3-")))
                    //intezetAzon2Lista= new HashSet<string>(groupLista.Where(w => w.Name.Contains("-FN-") || w.Name.Contains("-FN3-")).Select(s => s.Name.Substring(0,4).ToLower()).Distinct().ToList());
                    intezetAzon2Lista = new HashSet<string>(groupLista.OrderByDescending(o => o.Name.Contains("-FN-") || o.Name.Contains("-FN3-")).Select(s => s.Name.Substring(0, 4).ToLower()).Distinct().ToList());
                else
                    intezetAzon2Lista = new HashSet<string>(groupLista.Select(s => s.Name.Substring(0, 4).ToLower()).Distinct().ToList());

                var szerepkorok = new Dictionary<string, HashSet<string>>(); 
                
                foreach (var item in intezetTable)
                {
                    if (item.Azonosito2 != null)
                        item.Azonosito2 = item.Azonosito2.ToLower();
                }

                foreach (var azon in intezetAzon2Lista)
                {
                    foreach (var elem in intezetTable.Where(x => x.Azonosito2 == azon).Where(e => e.MegszunesDatum == null))
                    {
                        intezetLista.Add(elem);

                        jogosultIntezetek.Add(elem.Azonosito2, elem.Id);
                    }
                }
                var jogosultsagFunctions = new JogosultsagCacheFunctions();

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
                jogosultsagFunctions.UserJogosultsagok = jogosultsagok;
                return intezetLista;
#endif

            }
            catch (Exception e)
            {
                Log.Error("IntezetListaLegyujtes - error", e);
                throw;
            }

        }

    }
}
