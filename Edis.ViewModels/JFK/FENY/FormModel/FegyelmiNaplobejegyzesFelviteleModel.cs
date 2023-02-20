using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Edis.ViewModels.JFK.FENY.FormModel
{
    public class FegyelmiNaplobejegyzesFelviteleModel
    {
        public List<int> FegyelmiUgyIds { get; set; }
        public List<int> NaplobejegyzesIds { get; set; }
        
        [AllowHtml]
        [Required(ErrorMessage = "Szövegmező kitöltése kötelező")]
        public string Leiras { get; set; } = string.Empty;
    }
}
