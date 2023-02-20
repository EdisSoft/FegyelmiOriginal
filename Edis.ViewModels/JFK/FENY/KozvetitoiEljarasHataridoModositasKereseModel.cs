using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Edis.ViewModels.JFK.FENY
{
    public class KozvetitoiEljarasHataridoModositasKereseModel
    {
        public List<int> FegyelmiUgyIds { get; set; }

        public List<int> NaplobejegyzesIds { get; set; }

        [AllowHtml]
        [MaxLength(4000)]
        public string Leiras { get; set; }
    }
}
