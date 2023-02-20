using Edis.Utilities;
using Edis.ViewModels.Fany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.Nyomtatvany
{
    public class JFKFegyelmiUgyModel
    {
        public IntezetModel Intezet { get; set; }
        public int UgyIntezetiSorszam { get; set; }
        public int UgyEv { get; set; }

        public string FegyelmiUgyAzon
        {
            get
            {
                return $"{Intezet.Azonosito}{UgyIntezetiSorszam}/{UgyEv.ToString()}";
            }
        }
    }
}
