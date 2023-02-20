using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edis.Fenyites.Controllers.Base
{
    public class JsonNetResult : JsonResult
    {
        private const string _dateFormat = "yyyy-MM-ddTHH:mm:ss.sssZ";
        public new object Data { get; set; }
        public JsonNetResult(object data)
        {
            this.Data = data;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {
                var isoConvert = new IsoDateTimeConverter();
                isoConvert.DateTimeFormat = _dateFormat;
                isoConvert.DateTimeStyles = System.Globalization.DateTimeStyles.AdjustToUniversal;
                response.Write(JsonConvert.SerializeObject(Data, isoConvert));
            }

        }
    }
}