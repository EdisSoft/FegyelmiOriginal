using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Edis.ViewModels.JFK.FENY
{
    public class FeljegyzesEsMegallapodasModel
    {
        public List<int> FegyelmiUgyIds { get; set; }
        public List<int> NaplobejegyzesIds { get; set; }
        public List<KSelect2ItemModel> ReintegraciosTisztOptions { get; set; }
        public string SertettKepviseloje { get; set; }
        public string ReintegraciosTisztSid { get; set; }
        public string EljarasAlaVontKepviseloje { get; set; }
        [AllowHtml]
        public string FeljegyzesMegbeszelesrol { get; set; }
        [AllowHtml]
        public string Megallapodas { get; set; }
        public DateTime? Hatarido { get; set; }
        public DateTime MaxDatum { get; set; }
        public DateTime MinDatum { get; set; }
        public int? EljarasAlaVontatErintoKoltsegek { get; set; }
        public int? SertettetErintoKoltsegek { get; set; }
        public bool Vegleges { get; set; }
    }
}
