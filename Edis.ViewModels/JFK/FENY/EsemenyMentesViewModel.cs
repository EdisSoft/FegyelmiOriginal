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

    public class EsemenyMentesViewModel
    {

        public int? EsemenyId { get; set; }
        

        public DateTime EsemenyDatuma { get; set; }

        public List<KSelect2ItemModel> EszleloOptions { get; set; }

        public string EszleloId { get; set; }

        public List<KSelect2ItemModel> EsemenyJellegOptions { get; set; }

        public int EsemenyJellegCimkeId { get; set; }

        public List<KSelect2ItemModel> NapszakOptions { get; set; }

        public int NapszakCimkeId { get; set; }

        public List<KSelect2ItemModel> HelyOptions { get; set; }
 
        public int EsemenyHelyCimkeId { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "K?telez? mez?")]
        [MaxLength(2000)]
        public string Leiras { get; set; }

        [AllowHtml]
        [MaxLength(2000)]
        public string Bizonyitek { get; set; }

        public List<string> Tanuk { get; set; }

        public List<string> Sertettek { get; set; }

        public List<string> Elkovetok { get; set; }

        public List<string> AllomanyiTanuk { get; set; }

        public List<KSelect2ItemModel> TanukDefault { get; set; }
        public List<KSelect2ItemModel> SertettekDefault { get; set; }
        public List<KSelect2ItemModel> AllomanyiTanukDefault { get; set; }
        public List<KSelect2ItemModel> ElkovetokDefault { get; set; }
        public List<string> UploadedFiles { get; set; }
        public List<FeltoltesekViewModel> PrevUploadedFiles { get; set; }
        public bool? KarjelentoLapIktatas { get; set; }
        public bool? BvBankbol { get; set; }
        public int? IntezetId { get; set; }

    }
}
