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

    public class FelfuggesztesModel
    {
        public List<int> FegyelmiUgyIds { get; set; }

        [Required(ErrorMessage = "Felfüggesztés okának megadása kötelező")]
        public int? FelfuggesztesOkaCimkeId { get; set; }

        public List<KSelect2ItemModel> FelfuggesztesOkaOptions { get; set; }
    }
}
