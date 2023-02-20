using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.Nyomtatvany.Pdf
{
    public class MegallapodasEsFeljegyzesNyomtatasViewModel
    {
        public int FegyelmiUgyId { get; set; }
        public string IntezetNev { get; set; }
        public string Iktatoszam { get; set; }
        public string EljarasAlaVontFogvatartott { get; set; }
        public string EljarasAlaVontFogvAzon { get; set; }
        public string EljarasAlaVontkepviselo { get; set; }
        public string SertettFogvatartott { get; set; }
        public string SertettFogvAzon { get; set; }
        public string Sertettkepviselo { get; set; }
        public string UgySzam { get; set; }
        public string Megallapodas { get; set; }
        public string FeljegyzesMegbeszelesrol { get; set; }
        public string EljarasAlaVontatErintoKoltsegek { get; set; }
        public string SertettetErintoKoltsegek { get; set; }
        public string ReintegraciosTiszt { get; set; }
        public string Hatarido { get; set; }
        public string Kozvetito { get; set; }
        public string FegyelmiJogkorGyakorloja { get; set; }
        public string MegallapodasKelte { get; set; }
        public string MegbeszelesEv { get; set; }
        public string MegbeszelesHonap { get; set; }
        public string MegbeszelesNap { get; set; }
        public string MegbeszelesOra { get; set; }
        public string MegbeszelesPerc { get; set; }


    }
}
