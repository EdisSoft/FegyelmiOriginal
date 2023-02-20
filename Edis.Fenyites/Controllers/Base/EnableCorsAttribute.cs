using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edis.Fenyites.Controllers.Base
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EnableCorsAttribute : FilterAttribute, IActionFilter
    {
        private const string IncomingOriginHeader = "Origin";
        private const string OutgoingOriginHeader = "Access-Control-Allow-Origin";
        private const string OutgoingMethodsHeader = "Access-Control-Allow-Methods";
        private const string OutgoingCredentialsHeader = "Access-Control-Allow-Credentials";
        private const string OutgoingHeadersHeader = "Access-Control-Allow-Headers";

        private readonly List<string> _permissions = new List<string>();
        private bool IsSkipCheckPermissions { get; set; }

        public EnableCorsAttribute()
        {
            this.IsSkipCheckPermissions = true;
        }

        public EnableCorsAttribute(params string[] permissions)
        {
            IsSkipCheckPermissions = false;
            this._permissions.AddRange(permissions);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AddHttpHeaderToTheResponse(filterContext.HttpContext);
        }

        protected bool IsAllowedOrigin(string origin)
        {
            if (_permissions.Count() == 0) return false;
            return _permissions.Any(stringToCheck => origin.Contains(stringToCheck));
        }

        public void AddHttpHeaderToTheResponse(HttpContextBase context)
        {
            var isLocal = context.Request.IsLocal;
            var originHeader = context.Request.Headers.Get(IncomingOriginHeader);
            var response = context.Response;
            if (!String.IsNullOrWhiteSpace(originHeader) && (IsSkipCheckPermissions || isLocal || IsAllowedOrigin(originHeader)))
            {
                response.AddHeader(OutgoingOriginHeader, originHeader);
                response.AddHeader(OutgoingCredentialsHeader, "true");
                response.AddHeader(OutgoingHeadersHeader, "X-Requested-With, origin, content-type, accept");
                response.AddHeader(OutgoingMethodsHeader, "GET, POST, OPTIONS");
            }
        }
    }
}