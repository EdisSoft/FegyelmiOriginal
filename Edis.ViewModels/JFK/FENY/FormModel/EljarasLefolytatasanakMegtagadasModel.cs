using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Edis.ViewModels.JFK.FENY.FormModel
{
    public class EljarasLefolytatasanakMegtagadasModel
    {
        public List<int> FegyelmiUgyIds { get; set; }
        public List<KSelect2ItemModel> DonteshozoSzemelyek { get; set; }

        [Required(ErrorMessage = "Megtagadás indoklásának kitöltése kötelező")]
        public string DonteshozoSzemelySid { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Leírás kitöltése kötelező")]
        public string Leiras { get; set; }
    }
}
