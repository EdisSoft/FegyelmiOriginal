using Edis.Diagnostics;
using Edis.Entities.Enums;
using Edis.Functions;
using Edis.Functions.Common;
using Edis.IoC.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Edis.Functions.Base
{
    public class AktivitasLogFilterAttribute : ActionFilterAttribute
    {
        [Inject]
        private IAlkalmazasKontextusFunctions AlkalmazasKontextusFunctions { get; set; }
        public AktivitasTipusok AktivitasTipus { get; set; }
        public bool DbModositasTortenik { get; set; }
        public bool FeluletenLathato { get; set; }
        public AktivitasTabla TableName { get; set; }
        public string IdPropName { get; set; }

        static ConcurrentDictionary<AktivitasTabla, string> tables = new ConcurrentDictionary<AktivitasTabla, string>();
        static AktivitasLogFilterAttribute()
        {
            tables.TryAdd(AktivitasTabla.Foglalkoztatas_Munkaltatas, "Foglalkoztatas.Munkaltatas");
            tables.TryAdd(AktivitasTabla.Dbo_Fogvatartott, "Dbo.Fogvatartott");
            tables.TryAdd(AktivitasTabla.Foglalkoztatas_HaviElszamolas, "Foglalkoztatas.HaviElszamolas");
            tables.TryAdd(AktivitasTabla.Foglalkoztatas_OktatasResztvevo, "Foglalkoztatas.OktatasResztvevo");
            tables.TryAdd(AktivitasTabla.Foglalkoztatas_HaviElszamolas_oktatas, "Foglalkoztatas.HaviElszamolas_oktatas");
            tables.TryAdd(AktivitasTabla.Bvbank_NaploAtfutoSzamlaTetelek, "BvBank.NaploAtfutoSzamlaTetelek");
            tables.TryAdd(AktivitasTabla.BvBank_BvTartozasok, "BvBank.BvTartozasok");
            tables.TryAdd(AktivitasTabla.BvBank_NegyedEvesZarasKarteritesek, "BvBank.NegyedEvesZarasKarteritesek");
            tables.TryAdd(AktivitasTabla.BvBank_EgyeniSzamlak, "BvBank.EgyeniSzamlak");
            tables.TryAdd(AktivitasTabla.Dbo_Szabaditas, "dbo.szabaditas");
            tables.TryAdd(AktivitasTabla.BvBank_BelsoTranzakciok, "BvBank.BelsoTranzakciok");
            tables.TryAdd(AktivitasTabla.BvBank_ElojegyzettBevetel, "BvBank.ElojegyzettBevetel");
            tables.TryAdd(AktivitasTabla.BvBank_Karteritesek, "BvBank.Karteritesek");
            tables.TryAdd(AktivitasTabla.BvBank_Letiltasok, "BvBank.Letiltasok");
            tables.TryAdd(AktivitasTabla.BvBank_UtalasiHelyek, "BvBank.vUtalasiHelyFogvatartott");
            tables.TryAdd(AktivitasTabla.BvBank_Zarolasok, "BvBank.Zarolasok");
            tables.TryAdd(AktivitasTabla.BvBank_AtfutoSzamlaTetelek, "BvBank.vAtfutoszamlaFogvatartott");
            tables.TryAdd(AktivitasTabla.BvBank_FoglalkoztatasBizonylatTetel, "BvBank.FoglalkoztatasBizonylatTetelek");

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var attributes = (ModulNameAttribute[])filterContext.Controller.GetType().GetCustomAttributes(typeof(ModulNameAttribute), true);
            var modulConfig=  ConfigurationManager.AppSettings["modulCimkeId"];
            var modulCimkeId = attributes.Length > 0 ? attributes[0].ModulName : (ModulCimke)Convert.ToInt32(modulConfig);

            LogAktivitas(filterContext, AktivitasTipus, modulCimkeId, DbModositasTortenik, FeluletenLathato, HibaFl: filterContext.Exception != null);
        }

        public void LogAktivitas(ActionExecutedContext filterContext, AktivitasTipusok aktivitasTipus, ModulCimke modul, bool DbModositasTortenik = false, bool FeluletenLathato = false, int? FogvatartasId = null, bool HibaFl = false, int? kapcsolodoEntitasAdatbazisId = null)
        {
            //var error = string.Empty;
            //try
            //{
            //    error = "Aktivitas - 0";
            //    var aktivitasFunctions = new AktivitasFunctions();

            //    error = "Aktivitas - 1";
            //    var intezetId = AlkalmazasKontextusFunctions.Kontextus.F3IntezetId > 0 ? AlkalmazasKontextusFunctions.Kontextus.F3IntezetId : (int?)null;
            //    error = "Aktivitas - 2";
            //    var baratId = AlkalmazasKontextusFunctions.Kontextus.F3BaratId > 0 ? AlkalmazasKontextusFunctions.Kontextus.F3BaratId : (int?)null;
            //    error = "Aktivitas - 3";
            //    //var parameters = filterContext.HttpContext.GetHttpContextParameterJsonString();
            //    error = "Aktivitas - 4";

            //    var sb = new Dictionary<string, object>();

            //    foreach (string item in filterContext.HttpContext.Request.Form.Keys)
            //    {
            //        if(item!="_")
            //        sb.Add(item, filterContext.HttpContext.Request.Form[item]);
            //    }

            //    foreach (string item in filterContext.HttpContext.Request.QueryString.Keys)
            //    {
            //        if (item != "_")
            //            sb.Add(item, filterContext.HttpContext.Request.QueryString[item]);
            //    }
            //    var parameters= sb.Any() ? JsonConvert.SerializeObject(sb) : null;

            //    string controllerName = filterContext.RouteData.Values["controller"].ToString();
            //    string actionName = filterContext.RouteData.Values["action"].ToString();

            //    //var fogvatartasKeresoModel = aktivitasFunctions.GetFogvatartasKeresoModelByControllerNameActionName(controllerName, actionName);
            //    error = "Aktivitas - 7";
            //    int? kapcsolodoEntitasId = null;
            //    error = "Aktivitas - 8";
            //    if (IdPropName != null)
            //    {
            //        error = "Aktivitas - 9";
            //        kapcsolodoEntitasId = JObject.Parse(parameters)?.SelectToken($"$.{IdPropName}")?.ToObject<int?>();
            //        error = "Aktivitas - 10";
            //    }
            //    error = "Aktivitas - 11";
            //    int? fogvatartasId = FogvatartasId;
            //    error = "Aktivitas - 12";
            //    if (TableName!=AktivitasTabla.Nincs && kapcsolodoEntitasId.HasValue && !fogvatartasId.HasValue && tables.ContainsKey(TableName))
            //    {
            //        error = "Aktivitas - 13";
            //        fogvatartasId = aktivitasFunctions.GetFogvatartasIdForAktivitas(tables[TableName], kapcsolodoEntitasId.Value);
            //        error = "Aktivitas - 14";
            //    }
            //    error = "Aktivitas - 15";
            //    var modulCimkeId = modul.CastToInt();
            //    error = "Aktivitas - 16";
            //    var letezoModulCimke = Enum.GetValues(typeof(ModulCimke)).Cast<int>().Any(s => s == modulCimkeId);
            //    error = "Aktivitas - 17";
            //    if (!letezoModulCimke)
            //    {
            //        error = "Aktivitas - 18";
            //        throw new Exception("Nem létező modul cimke a Modulok enumban");
            //    }
            //    error = "Aktivitas - 19";
                
            //        aktivitasFunctions.SaveAktivitasAsync(modulCimkeId, intezetId, baratId, DbModositasTortenik, FeluletenLathato, parameters, aktivitasTipus.CastToInt(), null, null, fogvatartasId, kapcsolodoEntitasId, HibaFl: HibaFl);
            //        error = "Aktivitas - 44";
                
            //}
            //catch (Exception e)
            //{
            //    Log.Warning($"Hiba az aktivitás logolásakor! AktivitasTipusCimkeId: {aktivitasTipus.CastToInt()}, error: {error}\r\nStackTrace:{e.StackTrace}\r\n", e);
            //}

        }
    }

    public enum AktivitasTabla
    {
        Nincs=0,
        Foglalkoztatas_Munkaltatas,
        Dbo_Fogvatartott,
        Dbo_Szabaditas,
        Foglalkoztatas_HaviElszamolas,
        Foglalkoztatas_OktatasResztvevo,
        Foglalkoztatas_HaviElszamolas_oktatas,
        Bvbank_NaploAtfutoSzamlaTetelek,
        BvBank_BvTartozasok,
        BvBank_NegyedEvesZarasKarteritesek,
        BvBank_EgyeniSzamlak,
        BvBank_BelsoTranzakciok,
        BvBank_ElojegyzettBevetel,
        BvBank_Karteritesek,
        BvBank_Letiltasok,
        BvBank_UtalasiHelyek,
        BvBank_Zarolasok,
        BvBank_AtfutoSzamlaTetelek,
        BvBank_FoglalkoztatasBizonylatTetel,
    }
}
