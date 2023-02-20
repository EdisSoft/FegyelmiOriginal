using Edis.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY.FormModel
{
    public class MaganelzarasElrendeleseModel
    {
        public List<int> FegyelmiUgyIds { get; set; }
        public string FegyelmiJogkorGyakorloId { get; set; }   
        public string FegyelmiJogkorGyakorloNeve { get; set; }
        [Required(ErrorMessage = "A tervezett dátum kitöltése kötelező")]        
        public DateTime TervezettDatum { get; set; }
        public DateTime HatarozatDatum { get; set; }
        public List<string> Fofelugyelok { get; set; }
        public List<KSelect2ItemModel> FofelugyelokDefault { get; set; }
        public List<KSelect2ItemModel> FofelugyelokOptions { get; set; }

    }
}
