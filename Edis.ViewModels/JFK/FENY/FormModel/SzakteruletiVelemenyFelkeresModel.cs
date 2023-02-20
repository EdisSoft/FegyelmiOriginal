namespace Edis.ViewModels.JFK.FENY
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class SzakteruletiVelemenyFelkeresModel
    {
        public List<int> FegyelmiUgyIds { get; set; }
        public List<int> NaplobejegyzesIds { get; set; }
        public DateTime MaxHatarido { get; set; }
        public List<KSelect2ItemModel> SzakteruletiVezetokdefaultValue { get; set; }

        public List<string> CimzettSzakteruletiVezetok { get; set; }
        public DateTime Hatarido { get; set; }
        [AllowHtml]
        public string Leiras { get; set; }
    }
}
