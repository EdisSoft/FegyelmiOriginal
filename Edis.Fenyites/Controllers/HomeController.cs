using Edis.Diagnostics;
using Edis.Entities.Enums;
using Edis.Entities.JFK;
using Edis.Fenyites.Controllers.Base;
using Edis.Functions.Common;
using Edis.Functions.Fany;
using Edis.Functions.JFK;
using Edis.IoC;
using Edis.IoC.Attributes;
using Edis.ViewModels.JFK;
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
    [TrackTimeFilter]
    public class HomeController : BaseController
    {
        private readonly int[] vedoNyomtatvanyok = {
            (int)FenyitesNyomtatvanyok.NyilatkozatVedoErtesitesehez,
            (int)FenyitesNyomtatvanyok.NyilatkozatVedoKirendelesehez,
            (int)FenyitesNyomtatvanyok.VedoKiertesitese,
            (int)FenyitesNyomtatvanyok.VedoKirendelese,
            (int)FenyitesNyomtatvanyok.VedoTelefononTortenoTajekoztatasa };

        [Inject]
        private IAlkalmazasKontextusFunctions AlkalmazasKontextusFunctions { get; set; }

        [Inject]
        public INyomtatvanySablonFunction NyomtatvanySablonFunctionJFK { get; set; }

        [Inject]
        public IIntezetFunctions IntezetFunctions { get; set; } 
        
        [Inject]
        public IFegyelmiUgyFunctions FegyelmiUgyFunctions { get; set; }
        
        [Inject]
        public IJogosultsagCacheFunctions JogosultsagCacheFunctions { get; set; }
        [Inject]
        public IJogosultsagKezeloFunctions JogosultsagKezeloFunctions { get; set; }
        
       
        public ActionResult Index()
        {
            return RedirectToRoute("app");
        }
                
        public JsonResult GetAppData()
        {

            var kontext = AlkalmazasKontextusFunctions.Kontextus;

            var intezet = IntezetFunctions.FindById(kontext.RogzitoIntezetId);

            var UserData = GetUserData();
            var KonalyticsData = new
            {
                kontext.SzemelyzetNev,
                UserId = System.Web.HttpContext.Current.Request.LogonUserIdentity == null ? string.Empty : System.Web.HttpContext.Current.Request.LogonUserIdentity.Name.Replace("\\", "\\\\"),
                SessionId = kontext.SessionID,
                Beosztas = Session["szemelyzetBeosztas"],
                Product = ConfigurationManager.AppSettings["Product"],
                NoemiId = ConfigurationManager.AppSettings["ProductNoemiId"],
                KonalyticsFrameworkUrl = ConfigurationManager.AppSettings["KonalyticsFramework"],
            };

            //var list = NyomtatvanySablonFunctionJFK.GetFegyelmiNyomtatvanyokList().Select(x => new { DisplayName = x.NyomtatvanyLeirasa, Id = x.Id.ToString() });

            var Dokumentumok = new List<int>();
            var VedoDokumentumok = new List<int>();
            var AlkalmazasBeallitasok = new { 
                VirKimutatasFegyelmiUrl = ConfigurationManager.AppSettings["VirKimutatasFegyelmiUrl"],
                VirKimutatasJutalomUrl = ConfigurationManager.AppSettings["VirKimutatasJutalomUrl"],
                FonixUrl = ConfigurationManager.AppSettings["Fonix3Url"],
                FanyBaseUrl = ConfigurationManager.AppSettings["FanyBaseUrl"]
            };

            return Json(new { UserData, KonalyticsData, Dokumentumok, VedoDokumentumok, AlkalmazasBeallitasok });
        }

        public JsonResult GetToolBarData()
        {
            return Json(new { url = Session["exotoolbarUrl"] ?? "" });
        }

        [EnableCors("fonix2", "konasoft.hu")]
        public JsonResult GetUser()
        {
            var UserData = GetUserData();
            //UserData.ValaszthatoIntezetek = new List<ViewModels.Fany.IntezetModel>() { UserData.ValaszthatoIntezetek.First(), /*UserData.ValaszthatoIntezetek.Where(x=>x.Nev.ToLower().Contains("vác")).First()*/ };
            return Json(new { UserData });

        }
        public JsonResult GetRSALoginData()
        {
            var userData = GetUserData();
            var data = new RsaLoginData() {
                Cegnev = "BV",
                Sid = userData.SzemelyzetSid,
                ExpirationDate = DateTime.Now.AddMinutes(2),
            };

            var dataStr = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var rsaStr = RsaHelper.Encrypt(dataStr);
            return Json(rsaStr);
        }

        public JsonResult GetRSALoginData2()
        {
            var data = new RsaLoginData()
            {
                Cegnev = "Konasoft Kft.",
                Sid = "220",
                ExpirationDate = DateTime.Now.AddMonths(1),
            };

            var dataStr = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var rsaStr = RsaHelper.Encrypt(dataStr);
            return Json(rsaStr);
        }
    }
}