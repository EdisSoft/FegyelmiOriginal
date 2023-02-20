using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Edis.ViewModels.JFK.FENY.FormModel
{
    public class FeljelentesRogziteseModel
    {
        public List<int> NaplobejegyzesIds { get; set; }
        public List<int> FegyelmiUgyIds { get; set; }
        public List<KSelect2ItemModel> ElintezesModjaOptions { get; set; }
        public int ElintezesModjaId { get; set; }
        public List<KSelect2ItemModel> FeljelentesMinositeseOptions { get; set; }
        public int FeljelentesMinositeseId { get; set; }
        public DateTime FenyitesKiszabasIdeje { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Leírás kitöltése kötelező")]
        public string Leiras { get; set; }
    }
}
