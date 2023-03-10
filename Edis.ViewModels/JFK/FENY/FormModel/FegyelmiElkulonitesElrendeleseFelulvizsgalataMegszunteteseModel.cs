//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Edis.ViewModels.JFK.FENY
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Edis.Utilities;
    using Edis.ViewModels.Base;
    using System.ComponentModel.DataAnnotations;
    using Edis.Entities.JFK.FENY;
    using System.Web.Mvc;
    using System.Web.Helpers;
    using Newtonsoft.Json.Linq;
    using Edis.ViewModels.Common;

    public class FegyelmiElkulonitesElrendeleseFelulvizsgalataMegszunteteseModel
    {

        public List<int> FegyelmiUgyIds { get; set; }
        public List<int> NaplobejegyzesIds { get; set; }
        public string ElrendeloId { get; set; }
        public List<KSelect2ItemModel> ElrendeloOptions { get; set; }

        [AllowHtml]
        public string ElkulonitesOka { get; set; }
        public bool ElkulonitesOkaSzerkesztheto { get; set; }
        public DateTime? ElrendelesIdeje { get; set; }
        public DateTime? ElrendelesIdejeSzerkesztheto { get; set; }

        [AllowHtml]
        public string Rendelkezesek { get; set; }
        public bool IsFelulvizsgalva { get; set; }
        public DateTime? MegszuntetesIdeje { get; set; }

        public string ZarkabaHelyezes { get; set; }
        public List<IdLabelWithChildren> ZarkabaHelyezesOptions { get; set; }
        public DateTime BehelyezesTenylegesIdeje { get; set; }

        public string ZarkabaHelyezesObjektumNev { get; set; }
        public string ZarkabaHelyezesReszlegNev { get; set; }
        public string ZarkabaHelyezesZarkaNev { get; set; }

        public bool NocheckVegrehajtasiFok { get; set; }



    }
}
