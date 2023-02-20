using Edis.Diagnostics;
using Edis.Entities.Enums;
using Edis.Fenyites.Controllers.Base;
using Edis.Functions.Common;
using Edis.Functions.Fany;
using Edis.IoC.Attributes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Edis.Fenyites.Controllers
{
   
    public class DiagnosticsController : BaseController
    {
        [Inject]
        private IAlkalmazasKontextusFunctions AlkalmazasKontextusFunctions { get; set; }
        
        public JsonResult InfoLog(string data)
        {
            var log = "Személy:" + AlkalmazasKontextusFunctions.Kontextus.SzemelyzetNev + "\n ";
            log+= "Személy Id:" + AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId + "\n ";
            log+= "Személy sid:" + AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid + "\n ";
            log+= "IntezetId:" + AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId + "\n ";
            log+= "Log:" + data;
            Log.Info(log);
            return Json(new { success=true });
        }
    }
}