using Edis.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Edis.ViewModels.JFK.FENY.FormModel
{
    public class KozvetitoiEljarasGaranciaHataridoModositasaModel
    {
        public List<int> fegyelmiUgyIds { get; set; }
        public List<int> naplobejegyzesIds { get; set; }
        [Required(ErrorMessage = "Kötelező mező")]
        public DateTime Datum { get; set; }
        public DateTime? MaxDatum { get; set; }
        public DateTime? MinDatum { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Kötelező mező")]
        [MaxLength(4000)]
        public string Leiras { get; set; }
        //public DateTime BehelyezesTenylegesIdeje { get; set; }
        //public bool Vegleges { get; set; }
    }
}
