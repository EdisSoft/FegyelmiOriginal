using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Cimke
{
    public static partial class CimkeEnums
    {
        public enum FegyelmiUgyStatusz
        {
            JutalombolTorolt = 6,
            Kezdemenyezve = 86,
            KivizsgalasFolyamatban = 87,
            Megtagadva = 88,
            ReintegraciosTisztiJogkorben = 89,
            FenyitesKiszabva = 141,
            I_FokuTargyalas = 142,
            MegszuntetveUjEljarassal = 145,
            Osszevonva = 148,
            FenyitesVegrehajtasAlatt = 149,
            FenyitesVegrehajtva = 150,
            FogvatartottKioktatva = 152,
            FenyitesVegrehajtasMegszakitva = 156,
            II_FokuTargyalas = 157,
            Megszuntetve = 161,
            VegrehajthatosagaElevult = 174,
            NemHajthatoVegre = 175,
            KozvetitoiEljarasAlatt = 177,
            I_FokuFegyelmiTargyalasElokeszitese = 219,
            II_FokuTargyalasiJegyzokonyv = 286,
            AutomatikusanLezart = 341,
            HatalyonKivulHelyezve = 343
        }

        public static List<int> AutomatikusFelfuggesztesStatuszLista
        {
            get
            {
                return new List<int>() {
                    (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban,
                    (int)FegyelmiUgyStatusz.I_FokuTargyalas,
                    (int)FegyelmiUgyStatusz.ReintegraciosTisztiJogkorben
                };
            }
        }
    }
}
