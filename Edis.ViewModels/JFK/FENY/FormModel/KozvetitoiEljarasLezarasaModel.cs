using System.Collections.Generic;

namespace Edis.ViewModels.JFK.FENY
{
    public class KozvetitoiEljarasLezarasaModel
    {
        public List<int> FegyelmiUgyIds { get; set; }

        public List<int> NaplobejegyzesIds { get; set; }
        public List<KSelect2ItemModel> EljarasEredmenyeOptions { get; set; }
        public bool? EljarasEredmenye { get; set; }
        public int FegyelmiUgyStatusz { get; set; }
    }
}
