using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    public class AlkalmazasKontextus
    {
        public int SzemelyzetId { get; set; }
        public string SzemelyzetSid { get; set; }

        public string SzemelyzetNev { get; set; }
        public string SzemelyzetLoginNev { get; set; }
        public int RogzitoIntezetId { get; set; }
        public int RogzitesAdatbazisaIntezetId { get; set; }
        public int AlkalmazasFunkcioId { get; set; }
        public int AlkalmazasNaploId { get; set; }
        public int TranzakcioId { get; set; }
        public int? GlobalisJogosultsagIntezetId { get; set; }

        public bool LeszakadvaKozpontrol { get; set; }

        public string SessionID { get; set; }
        public string ClientHostName { get; set; }
        public WindowsIdentity AdUserIdentity { get; set; }
        public IDictionary<string, object> ListaCache { get; set; }

        public int KeresesTalalatMax { get; set; }

        //public INaplozasSeged NaplozasSeged { get; set; }

        public int? F3BaratId { get; set; }
        public int? F3IntezetId { get; set; }
        public int? F3ObjektumId { get; set; }
        public int? F3BeosztasId { get; set; }
        public List<int> F3SzakteruletIds { get; set; }
        public string PersonalHelpdeskRSA { get; set; }
       // public int? F3ReszlegId { get; set; }
    }
}
