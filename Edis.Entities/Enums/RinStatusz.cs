using Edis.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums
{
    public static partial class Constats
    {
        public const string Fogvatartott = "Fogvatartott";
    }
    public enum RinStatusz
    {
        [GroupName(Constats.Fogvatartott)]
        [Title("Fogvatartott")]
        Fogvatartott,
        [GroupName(Constats.Fogvatartott)]
        [Title("Hozzátartozó")]
        Hozzatartozo,
        [GroupName(Constats.Fogvatartott)]
        [Title("Tanoda")]
        Tanoda,
        [GroupName(Constats.Fogvatartott)]
        [Title("Szabadult")]
        Szabadult
    }
}
