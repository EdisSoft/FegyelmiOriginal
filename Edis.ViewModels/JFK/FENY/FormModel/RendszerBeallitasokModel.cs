namespace Edis.ViewModels.JFK.FENY
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class RendszerBeallitasokModel
    {
        public List<KSelect2ItemModel> LetetesekOptions { get; set; }
        public List<KSelect2ItemModel> RendezvenySzervezokOptions { get; set; }

        [AllowHtml]
        public List<string> LetetesekIds { get; set; }
        [AllowHtml]
        public List<string> RendezvenySzervezokIds { get; set; }
    }
}
