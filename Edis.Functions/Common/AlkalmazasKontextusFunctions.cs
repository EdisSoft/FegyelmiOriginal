using Edis.Entities.Fany;
using Edis.Functions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Edis.Functions.Common
{
    public class AlkalmazasKontextusFunctions : /*FunctionsBase<object, object>,*/ IAlkalmazasKontextusFunctions
    {
        public AlkalmazasKontextus Kontextus
        {
            get
            {
                AlkalmazasKontextus kontextus = (AlkalmazasKontextus)HttpContext.Current.Session["AlkalmazasKontextus"];
                return kontextus;
            }
            set { SetSessionValue("AlkalmazasKontextus", value); }
        }

        public AlkalmazasKontextusFunctions()
        {
            //Kontextus = new AlkalmazasKontextus();
        }

        void SetSessionValue(string key, object val)
        {
            if (HttpContext.Current.Session != null)
                HttpContext.Current.Session[key] = val;
        }
    }


}
