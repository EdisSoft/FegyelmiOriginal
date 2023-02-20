using Edis.Entities.Enums;
using Edis.Entities.Enums.Cimke;
using Edis.Fenyites.Controllers.Base;
using Edis.Functions.Base;
using Edis.Functions.Common;
using Edis.Functions.JFK;
using Edis.Functions.JFK.FENY;
using Edis.IoC.Attributes;
using Edis.Utilities;
using Edis.ViewModels;
using Edis.ViewModels.JFK;
using Edis.ViewModels.JFK.FENY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edis.Fenyites.Controllers
{
    [EnableCors]
    [TrackTimeFilter]
    public class EsemenyController : BaseController
    {
        [Inject]
        IEsemenyekFunctions EsemenyekFunctions { get; set; }

        [Inject]
        ICimkeFunctions CimkeFunctions { get; set; }

        [Inject]
        IAlkalmazasKontextusFunctions AlkalmazasKontextusFunctions { get; set; }

        [Inject]
        IEsemenyResztvevoFunctions EsemenyResztvevoFunctions { get; set; }

        [Inject]
        IFogvatartottNezetFunctions FogvatartottNezetFunctions { get; set; }

        [Inject]
        IFegyelmiUgyFunctions FegyelmiUgyFunctions { get; set; }
        //[HttpGet]

        //public JsonResult GetEsemeny(int esemenyId)
        //{
        //    var result = EsemenyekFunctions.GetEsemeny(esemenyId);
        //    return Json(result);
        //}

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetEsemenyRogzitesModalData, DbModositasTortenik = false, FeluletenLathato = true)] // Esemény szerkesztése
        public JsonResult GetEsemenyRogzitesModalData(int? esemenyId)
        {
            var result = EsemenyekFunctions.GetEsemeny(esemenyId);
            return Json(result);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetEsemenyReszletek, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto, Jogosultsagok.Fegyelmi_egyeb_szakterulet, Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetEsemenyReszletek(int esemenyId)
        {
            var result = EsemenyekFunctions.GetEsemenyReszletek(esemenyId);
            return Json(result);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetEsemenyek, DbModositasTortenik = false, FeluletenLathato = true)]
        public JsonResult GetEsemenyek()
        {
            var results = EsemenyekFunctions.GetEsemenyek();
            return Json(results);
        }

        //[AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetEsemenyResztvevok, DbModositasTortenik = false, FeluletenLathato = true)]
        public JsonResult GetEsemenyResztvevok(int esemenyId)
        {
            var list = EsemenyResztvevoFunctions.GetEsemenyResztvevok(esemenyId);
            return Json(list);
        }

        //public JsonResult GetEsemenyResztvevoAdatai(int fogvatartottId, int? esemenyId)
        //{
        //    var model = EsemenyResztvevoFunctions.GetEsemenyResztvevoAdatai(fogvatartottId, esemenyId);
        //    return Json(model);
        //}
        
        public JsonResult FindFogvatartottakForSelect(string term, List<int> fogvatartottIds, bool? kellIntezet)
        {
            int? intezetId = null;
            if (kellIntezet.GetValueOrDefault(false) == true)
            {
                intezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId;
            }
            List<KSelect2ItemModel> queryResult = EsemenyekFunctions.FindFogvatartottakForSelect(term, intezetId);
            List<KSelect2ItemModel> result = new List<KSelect2ItemModel>();
            if (fogvatartottIds != null)
            {
                var list = new HashSet<string>(fogvatartottIds.Select(x => x.ToString()));
                foreach (var item in queryResult)
                {
                    if (!list.Contains(item.id))
                        result.Add(item);
                }
            }
            else
            {
                result = queryResult;
            }
            return Json(result);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.EsemenyRogzitesModalMentes, DbModositasTortenik = true, FeluletenLathato = true)]
        //Jogosultság nélkül elérhető
        public JsonResult EsemenyRogzitesModalMentes(EsemenyMentesViewModel model)
        {
            ThrowValidationExceptionIfNotValid();
            model.Leiras = model.Leiras.Replace("\n", "<br />");

            List<string> sertettIsEsTanuIsEsElkovetoIs = new List<string>();
            if (model.Sertettek != null) sertettIsEsTanuIsEsElkovetoIs = sertettIsEsTanuIsEsElkovetoIs.Concat(model.Sertettek).ToList();
            if (model.Tanuk != null) sertettIsEsTanuIsEsElkovetoIs = sertettIsEsTanuIsEsElkovetoIs.Concat(model.Tanuk).ToList();
            if (model.Elkovetok != null) sertettIsEsTanuIsEsElkovetoIs = sertettIsEsTanuIsEsElkovetoIs.Concat(model.Elkovetok).ToList();

            var fogvatartottNevek = new List<string>();
            foreach (var item in sertettIsEsTanuIsEsElkovetoIs.GroupBy(s => s).Where(g => g.Count() > 1).Select(g => g.Key).ToList())
            {
                //var fogvNev = FogvatartottNezetFunctions.FindFogvatartottNezetById(Convert.ToInt32(item));
                //fogvatartottNevek.Add(fogvNev.SzuletesiNev);
            }

            if (fogvatartottNevek.Any())
                throw new WarningException($"A következő fogvatartottak szerepelnek sértett, tanú vagy elkövetőnél is, ami nem lehetséges: {string.Join(", ", fogvatartottNevek)}");

            if (model.EsemenyId > 0)
            {
                var ujFegyelmiUgyek = EsemenyekFunctions.ModifyEsemeny(model);
                return Json(new { isSuccess = true, esemenyId = model.EsemenyId, ujFegyelmiUgyek, message = "Az esemény módosításra került." +
                    (ujFegyelmiUgyek.Count > 0 ? $" {ujFegyelmiUgyek.Count} új elkövető hozzáadásra került, emiatt ellenük automatikusan fegyelmi ügy indult." : "")});
            }
            else
            {
                var ujFegyelmiUgyek = EsemenyekFunctions.CreateEsemeny(model);
                model.EsemenyId = FegyelmiUgyFunctions.FindById(ujFegyelmiUgyek[0]).EsemenyId;
                return Json(new { isSuccess = true, esemenyId = model.EsemenyId, ujFegyelmiUgyek, message = "Az esemény rögzítésre került." +
                    (ujFegyelmiUgyek.Count>0 ? $" {ujFegyelmiUgyek.Count} új elkövető hozzáadásra került, emiatt ellenük automatikusan fegyelmi ügy indult." : "")});
            }
        }

        //[HttpPost]
        //public JsonResult AddResztvevo(EsemenyResztvevoViewModel resztvevo)
        //{
        //    var result = EsemenyResztvevoFunctions.SaveEsemenyResztvevo(resztvevo);
        //    return Json(result);
        //}

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.DeleteResztvevo, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
                           Jogosultsagok.Fegyelmi_egyeb_szakterulet,
                           Jogosultsagok.Fegyelmi_fofelugyelo,
                           Jogosultsagok.Fegyelmi_reintegracios_tiszt,
                           Jogosultsagok.Jfk_fegyjutmegtekinto)]
        public JsonResult DeleteResztvevo(int esemenyId, int fogvatartottId)
        {
            EsemenyResztvevoFunctions.DeleteEsemenyResztvevo(esemenyId, fogvatartottId);
            return Json(new { isSuccess = true });
        }
        
        [HttpGet]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.DeleteEsemeny, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto)]
        public JsonResult DeleteEsemeny(int id)
        {

            EsemenyekFunctions.Delete(id);

            return Json(new { isSuccess = true, message = "Az esemény törlésre került" });
        }

        //[AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetEsemenyResztvevoById, DbModositasTortenik = false, FeluletenLathato = true)]
        public JsonResult GetEsemenyResztvevoById(int resztvevoId)
        {
            var resztvevo = EsemenyResztvevoFunctions.FindById(resztvevoId);
            return Json(resztvevo);
        }
    }
}