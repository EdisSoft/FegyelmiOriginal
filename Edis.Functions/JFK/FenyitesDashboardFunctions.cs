using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.ViewModels.JFK.FENY;
using Edis.Repositories.Contexts;
using Edis.Entities.JFK.FENY;
using System.Collections.Concurrent;
using System.Threading;
using Edis.Diagnostics;
using Edis.ViewModels;
using Edis.Functions.Common;
using Edis.ViewModels.JFK.FENY.Dashboard;
using Edis.Functions.Fany;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Edis.Functions.JFK
{

    public class FenyitesDashboardFunctions : IFenyitesDashboardFunctions
    {
        public delegate void FegyelmiUgyValtozasEvent(List<int> ujUgyIdList, List<int> valtozottUgyIdList, List<int> megszuntUgyIdList);
        public static event FegyelmiUgyValtozasEvent OnFegyelmiUgyValtozasFany;

        //static Timer fenyitesFrissitesTimer;

        //private static ConcurrentDictionary<int, FenyitesDashboardListItemViewModel> Fenyitesek;

        //public static void Init()
        //{
        //    fenyitesFrissitesTimer = new Timer(UpdateFenyitesek, null, 2 * 60 * 1000, 2 * 60 * 1000);
        //    Fenyitesek = new ConcurrentDictionary<int, FenyitesDashboardListItemViewModel>();
        //    try
        //    {
        //        InitFenyitesek();
        //    }
        //    catch (Exception e)
        //    {
        //        Log.Info("Az InitFenyitesek függvény meghívása nem sikerült.", e);
        //    }
        //}

        //public static bool InitFenyitesek()
        //{
        //    var fogvatartottKepFunctions = new FogvatartottKepFunctions();
        //    bool inited = false;

        //    Fenyitesek = new ConcurrentDictionary<int, FenyitesDashboardListItemViewModel>();


        //    var list = new FanyContext().Database.SqlQuery<FenyitesDashboardListItem>("call GetFenyitesDashboardList(0)").Select(x => (FenyitesDashboardListItemViewModel)x).ToList();
        //    foreach (var item in list)
        //    {
        //        Fenyitesek.TryAdd(item.FegyelmiUgyId, item);

        //        /*if (item.FogvKepDatuma != null)
        //        {
        //            item.Kep = fogvatartottKepFunctions.GetFogvatartottKepUrl(item.FogvSzemelyId, item.FogvKepDatuma.Value);

        //            if (item.Kep == null)
        //            {
        //                item.Kep = fogvatartottKepFunctions.GenerateFogvatartottKep(item.FogvatartottId);
        //            }
        //        }*/
        //    }

        //    if (Fenyitesek != null || Fenyitesek.Count > 0)
        //    {
        //        inited = true;
        //    }
        //    return inited;
        //}
        
        public void NotifyUseresOnFegyelmiUgyValtozas(List<int> ujUgyIdList, List<int> valtozottUgyIdList, List<int> megszuntUgyIdList)
        {
            OnFegyelmiUgyValtozasFany?.Invoke(ujUgyIdList, valtozottUgyIdList, megszuntUgyIdList);
        }

        //static void UpdateFenyitesek(object state)
        //{
        //    var fogvatartottKepFunctions = new FogvatartottKepFunctions();
        //    int? fegyelmiUgyIdParam = null;

        //    if (state != null)
        //    {
        //        fegyelmiUgyIdParam = (int)state;
        //    }
        //    List<FenyitesDashboardListItemViewModel> list = new List<FenyitesDashboardListItemViewModel>();
        //    try
        //    {
        //        list = new FanyContext().Database.SqlQuery<FenyitesDashboardListItem>($"call GetFenyitesDashboardList({fegyelmiUgyIdParam ?? 0})").Select(x => (FenyitesDashboardListItemViewModel)x).ToList();
        //    }
        //    catch (Exception exc)
        //    {
        //        Log.Error("UpdateFenyitesek lista lekérése", exc);
        //    }

        //    var dic = list.ToDictionary(x => x.FegyelmiUgyId);

        //    var torlendoList = new List<int>();
        //    var valtozottList = new List<int>();

        //    if (fegyelmiUgyIdParam.HasValue)
        //    {
        //        if (list.Any() && Fenyitesek.ContainsKey(fegyelmiUgyIdParam.Value))
        //        {
        //            if (VanValtozas(dic[fegyelmiUgyIdParam.Value], Fenyitesek[fegyelmiUgyIdParam.Value]))
        //            {
        //                valtozottList.Add(fegyelmiUgyIdParam.Value);
        //            }
        //            dic.Remove(fegyelmiUgyIdParam.Value);
        //        }
        //        else
        //        {
        //            torlendoList.Add(fegyelmiUgyIdParam.Value);
        //        }
        //    }
        //    else
        //    {
        //        foreach (var item in Fenyitesek)
        //        {
        //            if (dic.ContainsKey(item.Value.FegyelmiUgyId))
        //            {
        //                if (VanValtozas(dic[item.Value.FegyelmiUgyId], item.Value))
        //                {
        //                    valtozottList.Add(item.Value.FegyelmiUgyId);
        //                }
        //                else
        //                {
        //                    dic.Remove(item.Value.FegyelmiUgyId);
        //                }
        //            }
        //            else
        //            {
        //                torlendoList.Add(item.Value.FegyelmiUgyId);
        //            }
        //        }
        //    }

        //    foreach (var fegyelmiUgyId in valtozottList)
        //    {
        //        var newItem = dic[fegyelmiUgyId];

        //        Fenyitesek.AddOrUpdate(fegyelmiUgyId, dic[fegyelmiUgyId], (k, v) => dic[fegyelmiUgyId]);

        //        /*if (newItem.FogvKepDatuma != null)
        //        {
        //            newItem.Kep = fogvatartottKepFunctions.GetFogvatartottKepUrl(newItem.FogvSzemelyId, newItem.FogvKepDatuma.Value);

        //            if (newItem.Kep == null)
        //            {
        //                newItem.Kep = fogvatartottKepFunctions.GenerateFogvatartottKep(newItem.FogvatartottId);
        //            }
        //        }*/

        //        dic.Remove(fegyelmiUgyId);
        //    }

        //    var ujList = dic.Keys.ToList();

        //    foreach (var item in dic)
        //    {
        //        Fenyitesek.TryAdd(item.Key, item.Value);

        //        var newItem = item.Value;

        //        /*if (newItem.FogvKepDatuma != null)
        //        {
        //            newItem.Kep = fogvatartottKepFunctions.GetFogvatartottKepUrl(newItem.FogvSzemelyId, newItem.FogvKepDatuma.Value);

        //            if (newItem.Kep == null)
        //            {
        //                newItem.Kep = fogvatartottKepFunctions.GenerateFogvatartottKep(newItem.FogvatartottId);
        //            }
        //        }*/
        //    }

        //    foreach (var item in torlendoList)
        //    {
        //        FenyitesDashboardListItemViewModel m;
        //        Fenyitesek.TryRemove(item, out m);
        //    }

        //    if (ujList.Count > 0 || torlendoList.Count > 0 || valtozottList.Count > 0)
        //    {
        //        OnFegyelmiUgyValtozasFany?.Invoke(ujList, valtozottList, torlendoList);
        //    }

        //}

        //public static bool VanValtozas(FenyitesDashboardListItemViewModel item1, FenyitesDashboardListItemViewModel item2)
        //{
        //    if (item1.Elhelyezes != item2.Elhelyezes)
        //        return true;

        //    if (item1.AktNyilvantartasiSzam != item2.AktNyilvantartasiSzam)
        //        return true;

        //    if (item1.NyilvantartottStatusz != item2.NyilvantartottStatusz)
        //        return true;

        //    if (item1.UtolsoModositasDatum != item2.UtolsoModositasDatum)
        //        return true;

        //    if (item1.FogvKepDatuma != item2.FogvKepDatuma)
        //        return true;

        //    if (item1.LegkozelebbiSzallitasDatuma != item2.LegkozelebbiSzallitasDatuma)
        //        return true;

        //    return false;
        //}

        //public List<FenyitesDashboardListItemViewModel> GetFenyitesDashboardList()
        //{
        //    List<FenyitesDashboardListItemViewModel> result = new List<FenyitesDashboardListItemViewModel>();
        //    result = Fenyitesek.Values.ToList();
        //    return result;
        //}

        //public List<FenyitesTweetListItemViewModel> GetFenyitesTop5List()
        //{
        //    var result = Fenyitesek.Values.OrderByDescending(o => o.UtolsoModositasDatum).Take(5).Select(s => new FenyitesTweetListItemViewModel
        //    {
        //        SzuletesiNev = s.SzuletesiNev,
        //        UgyEve = s.UgyEve,
        //        UgyIntezetAzon = s.UgyIntezetAzon,
        //        UgyStatusz = s.UgyStatusz,
        //        UgySzama = s.UgySzama,
        //        UtolsoModositasDatum = s.UtolsoModositasDatum.Value
        //    }).ToList();
        //    return result;
        //}

        //public static List<FenyitesDashboardListItemViewModel> GetFenyitesDashboardListByFegyelmiUgyIds(List<int> fegyelmiUgyIds)
        //{
        //    if (fegyelmiUgyIds == null)
        //    {
        //        return null;
        //    }
        //    List<FenyitesDashboardListItemViewModel> result = new List<FenyitesDashboardListItemViewModel>();
        //    result = Fenyitesek.Where(w => fegyelmiUgyIds.Contains(w.Key)).Select(x => x.Value).ToList();
        //    return result;
        //}

        //public void UpdateFenyitesListaByFegyelmiUgyId(int fegyelmiUgyId)
        //{
        //    //UpdateFenyitesek(new Func<int>(() => fegyelmiUgyId));
        //    Log.Info("UpdateFenyitesListaByFegyelmiUgyId - fegyelmi ügy id: " + fegyelmiUgyId);
        //    UpdateFenyitesek(fegyelmiUgyId);
        //}

        //public List<FenyitesChartItemViewModel> GetIntezetekForeVonatkozottFegyelmiUgyei()
        //{
        //    var ugyek = Fenyitesek.Values.ToList();

        //    Dictionary<string, int> intezetekUgyei = new Dictionary<string, int>();

        //    foreach (var ugy in ugyek)
        //    {
        //        if (intezetekUgyei.ContainsKey(ugy.UgyIntezet)) intezetekUgyei[ugy.UgyIntezet]++;
        //        else intezetekUgyei.Add(ugy.UgyIntezet, 1);
        //    }

        //    var result = intezetekUgyei.OrderBy(x => x.Key).Select(x => new FenyitesChartItemViewModel()
        //    {
        //        UgyIntezetAzon = x.Key,
        //        UgyekSzama = x.Value
        //    }).ToList();

        //    return result;
        //}

        public List<FenyitesChartItemViewModel> GetIntezetenkentVegrehajtasraVaroFegyelmiUgyek()
        {
            // át kéne írni kódszótárra
            //var ugyek = Fenyitesek.Values.Where(x => x.UgyStatusz.ToLower().Contains("fenyítést szabtam ki")).ToList();
            var pars = new Dictionary<string, object>();
            var fegyelmiugyeklista = new KonasoftBVFonixContext().RunStoredProcedureByNev<DashboardIntezetSzamModel>("Fegyelmi.DashboardGetIntezetenkentVegrehajtasraVaroFegyelmiUgyek", pars);


            Dictionary<string, int> intezetekUgyeiMaganelzaras = new Dictionary<string, int>();
            Dictionary<string, int> intezetekUgyei = new Dictionary<string, int>();

            //foreach (var ugy in ugyek)//Fanysok
            //{
            //    if (ugy.FenyitesTipus?.ToLower().Contains("magánelzárás") ?? false)
            //    {
            //        if (intezetekUgyeiMaganelzaras.ContainsKey(ugy.UgyIntezet)) intezetekUgyeiMaganelzaras[ugy.UgyIntezet]++;
            //        else intezetekUgyeiMaganelzaras.Add(ugy.UgyIntezet, 1);
            //    }
            //    else
            //    {
            //        if (intezetekUgyei.ContainsKey(ugy.UgyIntezet)) intezetekUgyei[ugy.UgyIntezet]++;
            //        else intezetekUgyei.Add(ugy.UgyIntezet, 1);
            //    }
            //    if (ugy.UgyIntezet != ugy.RelevansIntezet)
            //    {
            //        if (ugy.FenyitesTipus?.ToLower().Contains("magánelzárás") ?? false)
            //        {
            //            if (intezetekUgyeiMaganelzaras.ContainsKey(ugy.RelevansIntezet)) intezetekUgyeiMaganelzaras[ugy.RelevansIntezet]++;
            //            else intezetekUgyeiMaganelzaras.Add(ugy.RelevansIntezet, 1);
            //        }
            //        else
            //        {
            //            if (intezetekUgyei.ContainsKey(ugy.RelevansIntezet)) intezetekUgyei[ugy.RelevansIntezet]++;
            //            else intezetekUgyei.Add(ugy.RelevansIntezet, 1);
            //        }
            //    }
            //}
            foreach (var ugy in fegyelmiugyeklista)//Mssql-esek
            {
                if (intezetekUgyei.ContainsKey(ugy.IntezetNev)) intezetekUgyei[ugy.IntezetNev] += ugy.Szam;
                else intezetekUgyei.Add(ugy.IntezetNev, ugy.Szam);

                if (intezetekUgyeiMaganelzaras.ContainsKey(ugy.IntezetNev)) intezetekUgyeiMaganelzaras[ugy.IntezetNev] += ugy.Szam2;
                else intezetekUgyeiMaganelzaras.Add(ugy.IntezetNev, ugy.Szam2);

                if (ugy.FogvatartottIntezetNev != ugy.IntezetNev)
                {
                    if (intezetekUgyeiMaganelzaras.ContainsKey(ugy.FogvatartottIntezetNev)) intezetekUgyeiMaganelzaras[ugy.FogvatartottIntezetNev] += ugy.Szam2;
                    else intezetekUgyeiMaganelzaras.Add(ugy.FogvatartottIntezetNev, ugy.Szam2);

                    if (intezetekUgyei.ContainsKey(ugy.FogvatartottIntezetNev)) intezetekUgyei[ugy.FogvatartottIntezetNev] += ugy.Szam;
                    else intezetekUgyei.Add(ugy.FogvatartottIntezetNev, ugy.Szam);
                }
            }

            //Hozzá kell tenni azokat az intézeteket, ahol csak magánelzárás van
            foreach (var item in intezetekUgyeiMaganelzaras.Where(x => !intezetekUgyei.ContainsKey(x.Key)).ToList())
            {
                intezetekUgyei.Add(item.Key, 0);
            }

            var result = intezetekUgyei.OrderBy(x => x.Key).Select(x => new FenyitesChartItemViewModel()
            {
                UgyIntezetAzon = x.Key,
                UgyekSzama = x.Value,
                UgyekSzama2 = intezetekUgyeiMaganelzaras.ContainsKey(x.Key) ? intezetekUgyeiMaganelzaras[x.Key] : 0
            }).ToList();



            return result;
        }

        public List<FenyitesChartItemViewModel> GetIntezetenkentHetenHataridosUgyekSzama()
        {
            //var ugyek = Fenyitesek.Values.Where(x => x.AHetenJarLe).ToList();
            var pars = new Dictionary<string, object>();
            var fegyelmiugyeklista = new KonasoftBVFonixContext().RunStoredProcedureByNev<DashboardIntezetSzamModel>("Fegyelmi.DashboardGetIntezetenkentHetenHataridosUgyekSzama", pars);

            Dictionary<string, int> intezetekUgyei = new Dictionary<string, int>();

            //foreach (var ugy in ugyek)
            //{
            //    if (intezetekUgyei.ContainsKey(ugy.UgyIntezet)) intezetekUgyei[ugy.UgyIntezet]++;
            //    else intezetekUgyei.Add(ugy.UgyIntezet, 1);
            //    if (ugy.UgyIntezet != ugy.RelevansIntezet)
            //    {
            //        if (intezetekUgyei.ContainsKey(ugy.RelevansIntezet)) intezetekUgyei[ugy.RelevansIntezet]++;
            //        else intezetekUgyei.Add(ugy.RelevansIntezet, 1);
            //    }
            //}

            foreach (var ugy in fegyelmiugyeklista)//Mssql-esek
            {
                if (ugy.Szam > 0)
                {
                    if (intezetekUgyei.ContainsKey(ugy.IntezetNev)) intezetekUgyei[ugy.IntezetNev] += ugy.Szam;
                    else intezetekUgyei.Add(ugy.IntezetNev, ugy.Szam);
                    if (ugy.FogvatartottIntezetNev != ugy.IntezetNev)
                    {
                        if (intezetekUgyei.ContainsKey(ugy.FogvatartottIntezetNev)) intezetekUgyei[ugy.FogvatartottIntezetNev] += ugy.Szam;
                        else intezetekUgyei.Add(ugy.FogvatartottIntezetNev, ugy.Szam);
                    }
                }
            }

            var result = intezetekUgyei.OrderBy(x => x.Key).Select(x => new FenyitesChartItemViewModel()
            {
                UgyIntezetAzon = x.Key,
                UgyekSzama = x.Value
            }).ToList();

            return result;
        }
        public List<FenyitesChartItemViewModel> GetIntezetenkentLejartUgyekSzama()
        {
            //var ugyek = Fenyitesek.Values.Where(x => x.Lejart).ToList();
            var pars = new Dictionary<string, object>();
            var fegyelmiugyeklista = new KonasoftBVFonixContext().RunStoredProcedureByNev<DashboardIntezetSzamModel>("Fegyelmi.DashboardGetIntezetenkentLejartUgyekSzama", pars);

            Dictionary<string, int> intezetekUgyei = new Dictionary<string, int>();

            //foreach (var ugy in ugyek)
            //{
            //    if (intezetekUgyei.ContainsKey(ugy.UgyIntezet)) intezetekUgyei[ugy.UgyIntezet]++;
            //    else intezetekUgyei.Add(ugy.UgyIntezet, 1);
            //    if (ugy.UgyIntezet != ugy.RelevansIntezet)
            //    {
            //        if (intezetekUgyei.ContainsKey(ugy.RelevansIntezet)) intezetekUgyei[ugy.RelevansIntezet]++;
            //        else intezetekUgyei.Add(ugy.RelevansIntezet, 1);
            //    }
            //}

            // var jsonSerialiser = new JavaScriptSerializer();
            // Log.Info($"GetIntezetenkentLejartUgyekSzama mysql {jsonSerialiser.Serialize(intezetekUgyei)} mssql {jsonSerialiser.Serialize(fegyelmiugyeklista)}");

            foreach (var ugy in fegyelmiugyeklista)//Mssql-esek
            {
                if (ugy.Szam > 0)
                {
                    if (intezetekUgyei.ContainsKey(ugy.IntezetNev)) intezetekUgyei[ugy.IntezetNev] += ugy.Szam;
                    else intezetekUgyei.Add(ugy.IntezetNev, ugy.Szam);
                    if (ugy.FogvatartottIntezetNev != ugy.IntezetNev)
                    {
                        if (intezetekUgyei.ContainsKey(ugy.FogvatartottIntezetNev)) intezetekUgyei[ugy.FogvatartottIntezetNev] += ugy.Szam;
                        else intezetekUgyei.Add(ugy.FogvatartottIntezetNev, ugy.Szam);
                    }
                }
            }

            var result = intezetekUgyei.OrderBy(x => x.Key).Select(x => new FenyitesChartItemViewModel()
            {
                UgyIntezetAzon = x.Key,
                UgyekSzama = x.Value
            }).ToList();

            return result;
        }

        public List<FenyitesChartItemViewModel> GetFenyitesTipusokAranyai()
        {
            var alkalmazasKontextus = new AlkalmazasKontextusFunctions();
            // át kéne írni kódszótárra
            List<FenyitesDashboardListItemViewModel> ugyek;
            var jogosultsagCacheFunctions = new JogosultsagCacheFunctions();
            var jogosultIntezetek = jogosultsagCacheFunctions.JogosultIntezetek;
            var intezetekString = string.Join(",", jogosultIntezetek.Select(x => x.Id).ToArray());
            var intezetekList = jogosultIntezetek.Select(x => x.Id).ToList();
            //if (jogosultIntezetek.Any(x => x.Id == 135))
            //{
            //    ugyek = Fenyitesek.Values.Where(x => x.UgyStatusz.ToLower().Contains("fenyítést szabtam ki") && x.FenyitesTipus != "-").ToList();
            //}
            //else
            //{
            //    ugyek = Fenyitesek.Values.Where(x => intezetekList.Contains(x.UgyIntezetId) && x.UgyStatusz.ToLower().Contains("fenyítést szabtam ki") && x.FenyitesTipus != "-").ToList();
            //}

            var pars = new Dictionary<string, object>
            {
                { "@IntezetIds", intezetekString }
            };
            var fegyelmiugyeklista = new KonasoftBVFonixContext().RunStoredProcedureByNev<DashboardIntezetSzamModel>("Fegyelmi.DashboardGetFenyitesTipusokAranyaiByIntezetLista", pars);


            Dictionary<string, int> intezetekUgyei = new Dictionary<string, int>();

            //foreach (var ugy in ugyek)
            //{
            //    if (intezetekUgyei.ContainsKey(ugy.FenyitesTipus)) intezetekUgyei[ugy.FenyitesTipus]++;
            //    else intezetekUgyei.Add(ugy.FenyitesTipus, 1);
            //}

            foreach (var ugy in fegyelmiugyeklista)//Mssql-esek
            {
                if (intezetekUgyei.ContainsKey(ugy.IntezetNev)) intezetekUgyei[ugy.IntezetNev] += ugy.Szam;
                else intezetekUgyei.Add(ugy.IntezetNev, ugy.Szam);
            }

            var result = intezetekUgyei.OrderBy(x => x.Key).Select(x => new FenyitesChartItemViewModel()
            {
                UgyIntezetAzon = x.Key,
                UgyekSzama = x.Value
            }).ToList();

            return result;
        }
    }
}
