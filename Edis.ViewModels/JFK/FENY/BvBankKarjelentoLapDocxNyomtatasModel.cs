using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY
{
   public class BvBankKarjelentoLapDocxNyomtatasModel
    {
        public string IntezetNev { get; set; }
        
        public string KKeltDatum { get; set; }
        public string KKeltDatumEv { get; set; }
        public string KKeltDatumHo { get; set; }
        public string KKeltDatumNap { get; set; }

        public string KKarbejelentoDatum { get; set; }
        public string KKarbejelentoDatumEv { get; set; }
        public string KKarbejelentoDatumHo { get; set; }
        public string KKarbejelentoDatumNap { get; set; }

        public string KAzon { get; set; }
        public string FogvatartottNeve { get; set; }
        public string FogvatartottAnyjaNeve { get; set; }
        public string FogvatartottSzuletesiHely { get; set; }
        public string FogvatartottSzuletesiDatum { get; set; }
        public string FogvatartottSzuletesiEv { get; set; }
        public string FogvatartottSzuletesiHo { get; set; }
        public string FogvatartottSzuletesiNap { get; set; }
        public string FogvatartottNyilvszam { get; set; }
        public string FSzabadulDatum { get; set; }
        public string FogvatartottLakcim { get; set; }

        public string KDatum { get; set; }
        public string KDatumEv { get; set; }
        public string KDatumHo { get; set; }
        public string KDatumNap { get; set; }
        public string KHatralekSzovegesen { get; set; }
        public string KHatralek { get; set; }
        public string KEredetiSzovegesen { get; set; }
        public string KEredeti { get; set; }

        public string KIndoklas { get; set; }
        public string KaresetLeirasa { get; set; }
        public string KaresetHelye { get; set; }

        public string KaresetDatum { get; set; }
        public string KaresetDatumEv { get; set; }
        public string KaresetDatumHo { get; set; }
        public string KaresetDatumNap { get; set; }

        public string Hely { get; set; }
        public string Szekhely { get; set; }

        public string GazdVez { get; set; }
        public string GazVezBeoszt { get; set; }
        public string BirosagNeve { get; set; }

        public List<BvBankKarjelentoLapTetelModel> Tetelek { get; set; }
        public List<BvBankKarjelentoLapTetelMunkalappalModel> TetelekMunkalappal { get; set; }
    }
}
