using Edis.Fenyites.Controllers.Base;
using Edis.Functions.Base;
using System.Net;
using System.Web.Mvc;

namespace Edis.Fenyites.Controllers
{
    
    public class ErrorController : BaseController
    {
        public ActionResult NoPermission()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return RedirectToRoute("app/#/nopermission");
        }

        public ActionResult NoPermissionJson()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Json(new
            {
                noPermission = true,
                success = false,
                title = "Figyelmeztetés",
                message = "Nincs jogosultsága a funkcióhoz.",
                serverWarning = true
            });
        }

        public ActionResult ErrorActionResultException(string title, string message, string returnType)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (returnType == "json")
            {
                return Json(new
                {
                    title = title,
                    message = message,
                    serverError = true
                });
            }

            ViewBag.message = message;
            ViewBag.title = title;
            return View();
        }

        public ActionResult WarningException(string title, string message, WarningExceptionLevel warningExceptionLevel)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;


            return Json(new
            {
                title,
                message,
                serverWarning = true,
                warningExceptionLevel
            });

        }

        // [HttpPost]
        public ActionResult GenericException(string title, string message, string returnType)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (returnType == "json")
            {
                return Json(new
                {
                    serverError = true,
                    title = title,
                    message = message
                });
            }

            return View();
        }

        public JsonResult JsonResultException(string message)
        {
            return Json(new
            {
                success = false,
                message = message
            });
        }

        public JsonResult ValidationException(object jsonSource)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return Json(jsonSource);
        }
    }
}