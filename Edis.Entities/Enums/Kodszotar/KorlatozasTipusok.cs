using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public enum KorlatozasTipusok
        {
            Telefonalas = 3161,
            HozzatartozovalKapcsTiltas = 3144,
            HozzatartozonKivullKapcsTiltas = 3146,
        }
    }
}
