using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Edis.Diagnostics;
using Edis.Functions.Common;
using Edis.IoC;
using Edis.Functions.Base;
using System.Reflection;
using System.Diagnostics;

namespace Edis.Fenyites.Controllers.Base
{
    public class HandleAppErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {

            RouteData routeData = new RouteData();


            string methodName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();

            string path = methodName + "." + actionName;

            if (!(filterContext.Exception is ValidationException) && !(filterContext.Exception is FileDownloadException))
            {
                StringBuilder sb = new StringBuilder();

                foreach (string item in filterContext.HttpContext.Request.Unvalidated.Form.Keys)
                {
                    sb.Append(item + "=" + filterContext.HttpContext.Request.Unvalidated.Form[item] + ";");
                }

                foreach (string item in filterContext.HttpContext.Request.Unvalidated.QueryString.Keys)
                {
                    sb.Append(item + "=" + filterContext.HttpContext.Request.Unvalidated.QueryString[item] + ";");
                }

                SaveLog(path, filterContext.HttpContext.Request.HttpMethod, filterContext.HttpContext.Session.SessionID, sb.ToString(), filterContext.HttpContext.Request.UserHostAddress, filterContext.Exception.StackTrace, filterContext.Exception);
            }
            Type controllerType = filterContext.Controller.GetType();
            MethodInfo method = null;
            if (filterContext.HttpContext.Request.HttpMethod == "POST")
            {
                method = controllerType.GetMethods().FirstOrDefault(x => String.Equals(x.Name, actionName, StringComparison.CurrentCultureIgnoreCase) && x.GetCustomAttributes(typeof(HttpPostAttribute), false).Length > 0);
                if (method == null)
                    method = controllerType.GetMethods().First(x => String.Equals(x.Name, actionName, StringComparison.CurrentCultureIgnoreCase));
            }
            else
            {
                method = controllerType.GetMethods().First(x => String.Equals(x.Name, actionName, StringComparison.CurrentCultureIgnoreCase));
            }
            var returnType = method.ReturnType;

            if (returnType == typeof(JsonResult) || filterContext.HttpContext.Request.IsAjaxRequest())
            {
                routeData.Values.Add("returnType", "json");
            }
            else if (returnType == typeof(ActionResult) || (returnType).IsSubclassOf(typeof(ActionResult)))
            {
                routeData.Values.Add("returnType", "html");
            }

            if (filterContext.Exception is ValidationException)
            {
                routeData.Values.Add("controller", "Error");
                routeData.Values.Add("action", "ValidationException");
                ValidationException ex = (ValidationException)filterContext.Exception;
                object json = new
                {
                    validationError = true,
                    errors = ex.Errors,
                    isReloadPage = ex.IsReloadPage,
                    additionalData=ex.AdditionalData
                };

                routeData.Values.Add("jsonSource", json);
            }
            else
            {
                if (filterContext.Exception is CommitFailedException)
                {
                    routeData.Values.Add("controller", "Error");
                    routeData.Values.Add("action", "ErrorActionResultException");
                    routeData.Values.Add("Message", "A művelet végrehajtása során váratlan hiba történt és az adatok nem lettek rögzítve! <br/><br/>Kérem, próbálja meg újra.");
                    routeData.Values.Add("Title", "Hiba!");
                }
                else if (filterContext.Exception is DbUpdateConcurrencyException)
                {
                    routeData.Values.Add("controller", "Error");
                    routeData.Values.Add("action", "ErrorActionResultException");
                    routeData.Values.Add("Message", "A művelet végrehajtása során váratlan hiba történt. Valószínűleg a háttérben valaki más által már frissültek az adatok! <br/><br/>Kérem, ellenőrizze az adatok helyességét!");
                    routeData.Values.Add("Title", "Hiba!");
                }
                else if (filterContext.Exception is DbUpdateException)
                {
                    routeData.Values.Add("controller", "Error");
                    routeData.Values.Add("action", "ErrorActionResultException");
                    routeData.Values.Add("Message", "A művelet végrehajtása során váratlan hiba történt, az értékek nem lettek módosítva! <br/><br/>Kérem, próbálja meg újra.");
                    routeData.Values.Add("Title", "Hiba!");
                }
                else if (filterContext.Exception is RetryLimitExceededException)
                {
                    routeData.Values.Add("controller", "Error");
                    routeData.Values.Add("action", "ErrorActionResultException");
                    routeData.Values.Add("Message", "A művelet végrehajtása során váratlan hiba történt és többszöri próbálkozásra sem sikerült végrehajtani! <br/><br/>Kérem, próbálja meg újra.");
                    routeData.Values.Add("Title", "Hiba!");
                }
                else if (filterContext.Exception is CustomException)
                {
                    routeData.Values.Add("controller", "Error");
                    routeData.Values.Add("action", "ErrorActionResultException");
                    routeData.Values.Add("Message", filterContext.Exception.Message);
                    routeData.Values.Add("Title", "Hiba!");

                }
                else if (filterContext.Exception is WarningException)
                {
                    //routeData.Values.Add("controller", "Error");
                    //routeData.Values.Add("action", "WarningActionResultException");
                    //routeData.Values.Add("Message", filterContext.Exception.Message);
                    //routeData.Values.Add("Title", "Figyelmeztetés!");

                    WarningException ex = (WarningException)filterContext.Exception;

                    switch (ex.WarningExceptionLevel)
                    {
                        case WarningExceptionLevel.Validation:
                            routeData.Values.Add("warningExceptionLevel", ex.WarningExceptionLevel);
                            routeData.Values.Add("Title", "Figyelmeztetés!");
                            break;
                        case WarningExceptionLevel.Warning:
                            routeData.Values.Add("warningExceptionLevel", ex.WarningExceptionLevel);
                            routeData.Values.Add("Title", "Figyelmeztetés!");
                            break;
                        case WarningExceptionLevel.Error:
                            routeData.Values.Add("warningExceptionLevel", ex.WarningExceptionLevel);
                            routeData.Values.Add("Title", "Hiba!");
                            break;
                    }
                    routeData.Values.Add("controller", "Error");
                    routeData.Values.Add("action", "WarningException");
                    routeData.Values.Add("Message", filterContext.Exception.Message);
                }
                else if (filterContext.Exception is FileDownloadException)
                {
                    routeData.Values.Add("controller", "Error");
                    routeData.Values.Add("action", "WarningException");
                    routeData.Values.Add("Message", filterContext.Exception.Message);
                    routeData.Values.Add("Title", "Nyomtatvány letöltés!");
                    routeData.Values.Add("warningExceptionLevel", WarningExceptionLevel.Error);
                }
                else
                {
                    // saját DB hiba kezelés
                    if (filterContext.Exception.Message.StartsWith("#SQLERROR"))
                    {
                        string usertext = filterContext.Exception.Message.Split('|').Single(x => x.StartsWith("USERTEXT:")).Replace("USERTEXT:", "");

                        routeData.Values.Add("controller", "Error");
                        routeData.Values.Add("action", "ErrorActionResultException");
                        routeData.Values.Add("Message", usertext);
                        routeData.Values.Add("Title", "Hiba!");
                    }
                    // saját DB hiba kezelés
                    else if (filterContext.Exception.Message.StartsWith("#SQLWARNING"))
                    {
                        string usertext = filterContext.Exception.Message.Split('|').Single(x => x.StartsWith("USERTEXT:")).Replace("USERTEXT:", "");

                        routeData.Values.Add("controller", "Error");
                        routeData.Values.Add("action", "WarningException");
                        routeData.Values.Add("Message", usertext);
                        routeData.Values.Add("Title", "Figyelmeztetés!");
                        routeData.Values.Add("warningExceptionLevel", WarningExceptionLevel.Error);
                    }
                    else
                    {
                        routeData.Values.Add("controller", "Error");
                        routeData.Values.Add("action", "GenericException");
                        routeData.Values.Add("Message", "Hiba történt az alkalmazásban!");
                        routeData.Values.Add("Title", "Hiba!");
                    }

                }
            }

            IController controller = new ErrorController();
            controller.Execute(new RequestContext(filterContext.HttpContext, routeData));
            filterContext.ExceptionHandled = true;
            base.OnException(filterContext);
        }

        private void SaveLog(string Where,string httpMethod, string sessionId, string data, string ip, string stacktrace, Exception e)
        {
            IAlkalmazasKontextusFunctions appsettingsFunctions = InjectionKernel.Instance.GetInstance<IAlkalmazasKontextusFunctions>();
            
            string ErrorText = string.Format("Hely:{0}\r\nSzemelyzetId:{1}\r\nIntezetId:{2}\r\nIpAddress:{3}\r\nSessionId:{4}\r\nhttpMethod:{5}\r\nValtozok:{6}\r\nStackTrace:{7}\r\n",
               Where,
               appsettingsFunctions.Kontextus.SzemelyzetId,
               appsettingsFunctions.Kontextus.RogzitoIntezetId,
               ip,
               sessionId,
               httpMethod,
               data,
               stacktrace
               );
            Log.Error(ErrorText, e);
        }
    }
}