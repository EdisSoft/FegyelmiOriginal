using Edis.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Idorend
{
    public enum AdatbazisTipus
    {
        [Title("Következő 1 hónap")]
        ElkovetkezoHonap = 1,
        [Title("Következő 3 hónap")]
        ElkovetkezoHaromHonap = 2,
        [Title("Elmúlt 1 hónap")]
        ElmultHonap = 3,
        [Title("Három hónapon túli")]
        HaromHonaponTuli = 4,
    }

}
