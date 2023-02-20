using Edis.Fenyites.Controllers.Base;
using System.Web;
using System.Web.Mvc;

namespace Edis.Fenyites
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleAppErrorAttribute());
        }
    }
}
