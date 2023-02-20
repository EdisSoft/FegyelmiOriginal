using Edis.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums
{
    public enum Muszak
    { 
        [Title("Nappali")]
        Nappali = 1,
        [Title("Éjszakai")]
        Ejszakai = 2
    }
}
