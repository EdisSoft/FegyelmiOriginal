using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Cimke.OET
{
    public static partial class CimkeEnums
    {
        public enum CikkSzallitasTipus
        {
            Okmany = 1,
            Ertek = 2,
            Targy = 3
        }

        public enum CikkSzallitasStatusz
        {
            Kuldendo = 1,
            SzallitasAlatt = 2,
            BefogadasraVaro = 3
        }
    }
}
