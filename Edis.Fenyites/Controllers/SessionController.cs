using Edis.Diagnostics;
using Edis.Entities.Enums;
using Edis.Fenyites.Controllers.Base;
using Edis.Functions.Base;
using Edis.Functions.Fany;
using Edis.Functions.JFK;
using Edis.Functions.JFK.FENY;
using Edis.IoC.Attributes;
using Edis.ViewModels.JFK.FENY;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace Edis.Fenyites.Controllers
{
    [EnableCors]
    [SessionState(System.Web.SessionState.SessionStateBehavior.Default)]
    [TrackTimeFilter]
    public class SessionController : BaseController
    {
        private static Object _lockObj = new Object();
        [Inject]
        public IJogosultsagCacheFunctions JogosultsagCacheFunctions { get; set; }
        [Inject]
        public IJogosultsagKezeloFunctions JogosultsagKezeloFunctions { get; set; }
        [Inject]
        public IVegrehajtoSzakteruletekFunctions VegrehajtoSzakteruletekFunctions { get; set; }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.IntezetValtas, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult IntezetValtas(int intezetId)
        {
            var sid = WindowsIdentity.GetCurrent();
            JogosultsagCacheFunctions.AktualisIntezet = JogosultsagCacheFunctions.JogosultIntezetek.Single(x => x.Id == intezetId);
            Log.Info($"changeintezet sid.Name: {sid.Name}, sid: {sid.User.Value}");
            try
            {
                lock (_lockObj)
                {
                    JogosultsagKezeloFunctions.BelepesCsakActiveDirectoryOlvasassal(sid, string.Empty, intezetId);
                }
                string cookieName = "fegyelmiIntezetId";
                var cookie = new HttpCookie(cookieName, JogosultsagCacheFunctions.AktualisIntezet.Id.ToString());
                Response.SetCookie(cookie);
            }
            catch (Exception ex)
            {
                throw new CustomException("Sikertelen intézetválasztás");

            }

            var UserData = GetUserData();
            return Json(new { UserData });

        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetRendszerbeallitasokModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [HttpPost]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetRendszerbeallitasokModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            int intezetId = JogosultsagCacheFunctions.AktualisIntezet.Id;
            var model = VegrehajtoSzakteruletekFunctions.GetRendszerbeallitasokModalData(intezetId);
            return Json(model);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveRendszerbeallitasokModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [HttpPost]
        [ValidateInput(false)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveRendszerbeallitasokModalData(RendszerBeallitasokModel model)
        {
            ThrowValidationExceptionIfNotValid();
            VegrehajtoSzakteruletekFunctions.SaveRendszerbeallitasokModalData(model);
            return Json(new {  });
        }
    }
}