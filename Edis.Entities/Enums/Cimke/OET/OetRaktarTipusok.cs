using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Cimke.OET
{
    public static partial class CimkeEnums
    {
        public enum OetRaktarTipusok
        {
            [Description("Okmány")]
            OkmanyRaktar = 5470,
            [Description("Érték")]
            ErtekRaktar = 5471,
            [Description("Tárgy")]
            TargyRaktar = 5472
        }
    }
}
