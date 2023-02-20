using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Edis.Diagnostics;
using Edis.Entities.Enums;
using Edis.Functions.Common;
using Edis.Functions.Fany;
using Edis.IoC.Attributes;

namespace Edis.Fenyites.Controllers.Base
{
    public class RequirePermissionAttribute : AuthorizeAttribute
    {
        [Inject]
        private IJogosultsagKezeloFunctions JogosultsagKezeloFunctions { get; set; }

        [Inject]
        private IAlkalmazasKontextusFunctions AlkalmazasKontextusFunctions { get; set; }

        private readonly List<Jogosultsagok> _permissions = new List<Jogosultsagok>();

        public override void OnAuthorization(AuthorizationContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (this._permissions.Count == 0)
                return;
#if DEBUG
            var securityIdentifier = WindowsIdentity.GetCurrent().User;
            if (securityIdentifier != null)
            {
                var sid = securityIdentifier.Value;
                if (JogosultsagKezeloFunctions.IsDebugPermissions(sid)) return;
            }
            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                action = nameof(ErrorController.NoPermission),
                controller = "Error",
                area = "",
                code = HttpStatusCode.Forbidden,
                returnurl = context.HttpContext.Request.UrlReferrer == null ? context.HttpContext.Request.ApplicationPath : context.HttpContext.Request.UrlReferrer.AbsolutePath + context.HttpContext.Request.UrlReferrer.Query
            }));
            return;
#endif
            foreach (var jogosultsag in _permissions)
            {
                if (!_permissions.Any(x => JogosultsagKezeloFunctions.VanJogosultsaga(x)))
                {

                    //string actionName = context.RouteData.Values["action"].ToString();
                    //Type controllerType = context.Controller.GetType();
                    //MethodInfo method = null;
                    //if (context.HttpContext.Request.HttpMethod == "POST")
                    //{
                    //    method = controllerType.GetMethods().FirstOrDefault(x => String.Equals(x.Name, actionName, StringComparison.CurrentCultureIgnoreCase) && x.GetCustomAttributes(typeof(HttpPostAttribute), false).Length > 0);
                    //    if (method == null)
                    //        method = controllerType.GetMethods().First(x => String.Equals(x.Name, actionName, StringComparison.CurrentCultureIgnoreCase));
                    //}
                    //else
                    //{
                    //    method = controllerType.GetMethods().First(x => String.Equals(x.Name, actionName, StringComparison.CurrentCultureIgnoreCase));
                    //}
                    //var returnType = method.ReturnType;

                    //if (returnType == typeof(JsonResult) || context.HttpContext.Request.IsAjaxRequest())
                    //{
                    Log.Info($"Nincs jogosultsága szemelyId: {AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId} intezetId: {AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId} url: {context.HttpContext.Request.RawUrl}");


                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        action = nameof(ErrorController.NoPermissionJson),
                        controller = typeof(ErrorController).ControllerName(),
                        //returnurl = context.HttpContext.Request.UrlReferrer == null ? context.HttpContext.Request.ApplicationPath : context.HttpContext.Request.UrlReferrer.AbsolutePath + context.HttpContext.Request.UrlReferrer.Query
                    }));
                    //}
                    //else //if (returnType == typeof(ActionResult) || (returnType).IsSubclassOf(typeof(ActionResult)))
                    //{
                    //    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    //    {
                    //        action = nameof(ErrorController.NoPermission),
                    //        controller = typeof(ErrorController).ControllerName(),
                    //        //returnurl = context.HttpContext.Request.UrlReferrer == null ? context.HttpContext.Request.ApplicationPath : context.HttpContext.Request.UrlReferrer.AbsolutePath + context.HttpContext.Request.UrlReferrer.Query
                    //    }));
                    //}


                    new EnableCorsAttribute().AddHttpHeaderToTheResponse(context.HttpContext);
                    return;
                }
            }
        }

        public RequirePermissionAttribute(params Jogosultsagok[] permissions)
        {
            this._permissions.AddRange(permissions);
        }
    }
}