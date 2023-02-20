
using Edis.Entities.Enums;
using Edis.Fenyites.Controllers.Base;
using Edis.Functions.Base;
using Edis.Functions.Common;
using Edis.Functions.Fany;
using Edis.Functions.JFK;
using Edis.IoC.Attributes;
using System.Web.Mvc;

namespace Edis.Fenyites.Controllers
{
    [EnableCors]
    [TrackTimeFilter]
    public class FogvatartottController : BaseController
    {
        [Inject]
        public IFogvatartottNezetFunctions FogvatartottNezetFunctions { get; set; }

        [Inject]
        public IFegyelmiUgyFunctions FegyelmiUgyFunctions { get; set; }
       

        [Inject]
        IJogosultsagCacheFunctions JogosultsagCacheFunctions { get; set; }

        [HttpPost]
        //[AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFogvatartottById, DbModositasTortenik = false, FeluletenLathato = true)]
        public JsonResult GetFogvatartottById(int fogvatartottId)
        {
            var result = FogvatartottNezetFunctions.FindById(fogvatartottId);
            return Json(result);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetIntezetiFogvatartottak, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto,
        Jogosultsagok.Fegyelmi_egyeb_szakterulet,
        Jogosultsagok.Fegyelmi_fofelugyelo,
        Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
        Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetIntezetiFogvatartottak(string term)
        {
            int intezetId = JogosultsagCacheFunctions.AktualisIntezet.Id;
            var result = FegyelmiUgyFunctions.GetJelenlevoIntezetiFogvatartottak(intezetId, term);
            return Json(result);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFogvatartottFegyelmiUgyei, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto,
        Jogosultsagok.Fegyelmi_egyeb_szakterulet,
        Jogosultsagok.Fegyelmi_fofelugyelo,
        Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
        Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetFogvatartottFegyelmiUgyei(int fogvatartottId)
        {
            var result = FegyelmiUgyFunctions.GetFegyelmiUgyekByFogvatartottId(fogvatartottId);
            return Json(result);
        }

        //[HttpPost]
        //[AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFogvatartottJutalmai, DbModositasTortenik = false, FeluletenLathato = true)]
        //public JsonResult GetFogvatartottJutalmai(int fogvatartottId)
        //{
        //    var result = F3JutalomFunctions.GetJutalmakByFogvatartottId(fogvatartottId);
        //    return Json(result);
        //}
    }
}