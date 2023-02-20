using Edis.Entities.Enums;
using Edis.Fenyites.Controllers.Base;
using Edis.Functions.Base;
using Edis.Functions.JFK;
using Edis.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edis.Fenyites.Controllers
{
    [EnableCors]
    [TrackTimeFilter]
    public class FenyitesController : BaseController
    {
        FenyitesDashboardFunctions FenyitesDashboardFunctions = new FenyitesDashboardFunctions();

        //[RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto,
        //Jogosultsagok.Fegyelmi_egyeb_szakterulet,
        //Jogosultsagok.Fegyelmi_fofelugyelo,
        //Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
        //Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        //public JsonResult GetFenyitesek()
        //{
        //    var fenyitesek = FenyitesDashboardFunctions.GetFenyitesDashboardList();
        //    return Json(fenyitesek);
        //}

        //[EnableCors("fonix2", "konasoft.hu")]
        //[RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto,
        //Jogosultsagok.Fegyelmi_egyeb_szakterulet,
        //Jogosultsagok.Fegyelmi_fofelugyelo,
        //Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
        //Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        //public JsonResult GetTweetFenyitesek()
        //{
        //    var fenyitesek = FenyitesDashboardFunctions.GetFenyitesTop5List();
        //    return Json(fenyitesek);
        //}

        //[RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto,
        //Jogosultsagok.Fegyelmi_egyeb_szakterulet,
        //Jogosultsagok.Fegyelmi_fofelugyelo,
        //Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
        //Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        //public JsonResult GetFenyitesekByFegyelmiUgyIds(List<int> fegyelmiUgyIds)
        //{
        //    var fenyitesek = FenyitesDashboardFunctions.GetFenyitesDashboardListByFegyelmiUgyIds(fegyelmiUgyIds);
        //    return Json(fenyitesek);
        //}

        //[RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto,
        //Jogosultsagok.Fegyelmi_egyeb_szakterulet,
        //Jogosultsagok.Fegyelmi_fofelugyelo,
        //Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
        //Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        //public EmptyResult GetFanyFenyitesByFegyelmiUgyId(int fegyelmiUgyId)
        //{
        //    FenyitesDashboardFunctions.UpdateFenyitesListaByFegyelmiUgyId(fegyelmiUgyId);

        //    return new EmptyResult();
        //}

        //// Intézetek főre vonatkoztatott fegyelmi ügyei
        //[EnableCors("fonix2", "konasoft.hu")]
        //[RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto,
        //Jogosultsagok.Fegyelmi_egyeb_szakterulet,
        //Jogosultsagok.Fegyelmi_fofelugyelo,
        //Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
        //Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        //public JsonResult GetIntezetekForeVonatkozottFegyelmiUgyei()
        //{
        //    var fenyitesek = FenyitesDashboardFunctions.GetIntezetekForeVonatkozottFegyelmiUgyei();
        //    return Json(fenyitesek);
        //}

        // Intézetenként a végrehajtásra váró fegyelmi ügyek száma
        [EnableCors("fonix2", "konasoft.hu")]
        [RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto,
        Jogosultsagok.Fegyelmi_egyeb_szakterulet,
        Jogosultsagok.Fegyelmi_fofelugyelo,
        Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
        Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetIntezetenkentVegrehajtasraVaroFegyelmiUgyek()
        {
            var fenyitesek = FenyitesDashboardFunctions.GetIntezetenkentVegrehajtasraVaroFegyelmiUgyek();
            return Json(fenyitesek);
        }

        // Intézetenként a héten határidős ügyek száma
        [EnableCors("fonix2", "konasoft.hu")]
        [RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto,
        Jogosultsagok.Fegyelmi_egyeb_szakterulet,
        Jogosultsagok.Fegyelmi_fofelugyelo,
        Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
        Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetIntezetenkentHetenHataridosUgyekSzama()
        {
            var fenyitesek = FenyitesDashboardFunctions.GetIntezetenkentHetenHataridosUgyekSzama();
            return Json(fenyitesek);
        }

        [EnableCors("fonix2", "konasoft.hu")]
        [RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto,
        Jogosultsagok.Fegyelmi_egyeb_szakterulet,
        Jogosultsagok.Fegyelmi_fofelugyelo,
        Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
        Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetIntezetenkentLejartUgyekSzama()
        {
            var fenyitesek = FenyitesDashboardFunctions.GetIntezetenkentLejartUgyekSzama();
            return Json(fenyitesek);
        }

        // Fenyítés típusok arányai
        [EnableCors("fonix2", "konasoft.hu")]
        [RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto,
        Jogosultsagok.Fegyelmi_egyeb_szakterulet,
        Jogosultsagok.Fegyelmi_fofelugyelo,
        Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
        Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetFenyitesTipusokAranyai()
        {
            var fenyitesek = FenyitesDashboardFunctions.GetFenyitesTipusokAranyai();
            return Json(fenyitesek);
        }
    }
}