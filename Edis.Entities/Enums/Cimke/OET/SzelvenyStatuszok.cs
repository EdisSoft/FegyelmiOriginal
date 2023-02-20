using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Cimke.OET
{
    public static partial class CimkeEnums
    {
      /// <summary>
      /// Okmány, érték, tárgy státuszok egyben
      /// </summary>
        public enum SzelvenyStatuszok
        {
            RogzitesAlatt = 5001,
            AtadasAlatt = 5002,
            Ervenyes = 5003,
            Szallitasban = 5004,
            Stornozott = 5005,
            Lezart = 5006
        }

        public static List<int> SzelvenyStatuszokAktiv = new List<int> {
            /**5001,5002,5003,5004*/
            (int)SzelvenyStatuszok.RogzitesAlatt,
            (int)SzelvenyStatuszok.AtadasAlatt,
            (int)SzelvenyStatuszok.Ervenyes,
            (int)SzelvenyStatuszok.Szallitasban,

        };
        

    }
}
