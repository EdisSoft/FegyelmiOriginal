using Edis.Fenyites.Controllers.Base;
using Edis.Functions.Fany;
using Edis.Functions.JFK;
using Edis.IoC.Attributes;
using Edis.ViewModels.JFK.FENY.Email;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RazorEngine;
using RazorEngine.Templating;

namespace Edis.Fenyites.Controllers
{
    [EnableCors]
    [TrackTimeFilter]
    public class TesztController : BaseController
    {
        FenyitesDashboardFunctions FenyitesDashboardFunctions = new FenyitesDashboardFunctions();

        [Inject]
        IFegyelmiUgyFunctions FegyelmiUgyFunctions { get; set; }

        public JsonResult NotifyUseresOnFegyelmiUgyValtozasFany(List<int> ujUgyIdList, List<int> valtozottUgyIdList, List<int> megszuntUgyIdList)
        {
            FenyitesDashboardFunctions.NotifyUseresOnFegyelmiUgyValtozas(ujUgyIdList, valtozottUgyIdList, megszuntUgyIdList);
            return Json(new {success = true });
        }

        public JsonResult NotifyUseresOnFegyelmiUgyValtozas(List<int> ujUgyIdList, List<int> valtozottUgyIdList, List<int> megszuntUgyIdList)
        {         
            FegyelmiUgyFunctions.NotifyUseresOnFegyelmiUgyValtozas(ujUgyIdList, valtozottUgyIdList, megszuntUgyIdList);
            return Json(new { success = true });
        } 
        public JsonResult GetOsszefoglalojelentesNyomtatasAdat(int fegyelmiUgyId)
        {
           var result = FegyelmiUgyFunctions.GetOsszefoglalojelentesNyomtatasAdat(fegyelmiUgyId);
            return Json(result);
        }

        public JsonResult EmailData()
        {
            var fegyelmiUgyFunctions = new FegyelmiUgyFunctions();

            List<ElkulonitesEmailData> elkulonitesEmailDatas = fegyelmiUgyFunctions.FegyelmiUgyElkulonitesErtesitoAdatok();
            List<RendezvenyErtesitesEmailData> rendezvenyEmailDatas = fegyelmiUgyFunctions.FegyelmiUgyRendezvenyErtesitesEmailAdatok();
            List<TargyiErtesitesEmailData> targyiKorlatozasEmailDatas = fegyelmiUgyFunctions.FegyelmiUgyTargyiErtesitesEmailAdatok();
            List<TargyiErtesitesEmailData> tobbletszolgaltatasEmailDatas = fegyelmiUgyFunctions.FegyelmiUgyTobbletszolgaltatasEmailAdatok();


            return Json(new { elkulonitesEmailDatas, rendezvenyEmailDatas, targyiKorlatozasEmailDatas, tobbletszolgaltatasEmailDatas });
        }

    }
}