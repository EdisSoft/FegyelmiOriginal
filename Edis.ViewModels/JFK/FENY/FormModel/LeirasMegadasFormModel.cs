using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Edis.ViewModels.JFK.FENY.FormModel
{
    public class LeirasMegadasFormModel
    {
        public List<int> FegyelmiUgyIds { get; set; }

        private string _leiras;
        [AllowHtml]
        [Required(ErrorMessage = "Leírás kitöltése kötelező")]
        public string Leiras
        {
            get
            {
                return _leiras ?? string.Empty;
            }
            set { _leiras = value; }
        }
    }
}
