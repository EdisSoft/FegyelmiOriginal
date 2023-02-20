using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        /// <summary>
        /// Egészségügyi állapot típus kódszótár csoport: 142
        /// </summary>
        public enum EgeszsegugyiAllapot
        {
           Egeszseges=294,
           GondozottBeteg=295,
           FertozoBeteg=296,
           DietaraSzorul=297,
           KezeltBeteg=298,
           KronikusBeteg=299,
           Fogyatekos=300
        }
    }
}
