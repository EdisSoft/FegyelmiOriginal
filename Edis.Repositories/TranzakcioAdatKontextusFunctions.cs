using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Edis.Repositories
{
    public class TranzakcioAdatKontextusFunctions : ITranzakcioAdatKontextusFunctions
    {
       
        public TranzakcioAdatkontextus Kontextus
        {
            get
            {
                
                TranzakcioAdatkontextus kontextus = (TranzakcioAdatkontextus)GetSessionValue("TranzakcioKontextus");
                if (kontextus == null)
                    kontextus = new TranzakcioAdatkontextus();
                return kontextus;
            }
            set { SetSessionValue("TranzakcioKontextus", value); }
        }

        public TranzakcioAdatKontextusFunctions()
        {
            
        }

        void SetSessionValue(string key, object val)
        {
            if (HttpContext.Current.Session != null)
                HttpContext.Current.Session[key] = val;
        }

        object GetSessionValue(string key)
        {
            if (HttpContext.Current == null)
                return null;
            return HttpContext.Current.Session["TranzakcioKontextus"];
        }
    }
}
