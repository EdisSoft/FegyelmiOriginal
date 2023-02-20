using Edis.ViewModels.JFK.FENY;
using System;
using System.Collections.Generic;

namespace Edis.ViewModels
{
    public class EsemenyListItemViewModel
    {
        public int EsemenyId { get; set; }
        public DateTime EsemenyDatuma { get; set; }
        public List<EsemenyResztvevoAdataiViewModel> Resztvevok { get; set; }
        public string Jelleg { get; set; }
        public string Napszak { get; set; }
        public string Hely { get; set; }
        public string Leiras { get; set; }
        public string RogzitoSzemely { get; set; }
        public string RogzitoIntezet { get; set; }
    }
}
