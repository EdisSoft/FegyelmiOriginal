using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Edis.ViewModels.JFK.FENY.FormModel
{
    public class OsszefoglaloJelentesModel
    {
        public List<int> FegyelmiUgyIds { get; set; }
        public List<int> NaplobejegyzesIds { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Leírás kitöltése kötelező")]
        public string Leiras { get; set; }
        public bool? IsAlairta { get; set; }

        public OsszefoglaloJelentesModel()
        {
            NaplobejegyzesIds = new List<int>();
        }
    }
}
