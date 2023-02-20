namespace Edis.ViewModels.JFK.FENY
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class FegyelmiUgyMasodFokuTargyalasiJegyzokonyvModel
    {
        public List<int> FegyelmiUgyIds { get; set; }        
        public List<string> TovabbiSzemelyekList { get; set; }
        public string JegyzokonyvVezetoSid { get; set; }
        public string FegyelmiJogkorGyakorlojaSid { get; set; }
        public int? HatarozatHozoJogkoreCimkeId { get; set; }

        public List<KSelect2ItemModel> JegyzokonyvVezetoOptions { get; set; }
        public List<KSelect2ItemModel> TovabbiSzemelyekOptions { get; set; }        
        public List<KSelect2ItemModel> FegyelmiJogkorGyakorlojaOptions { get; set; }

        [AllowHtml]
        public string Leiras { get; set; }
        public List<int> NaplobejegyzesIds { get; set; }        
    }
}
