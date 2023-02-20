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
        public enum SzolgaltatoTipusok
        {
            Kietkezes = 10860,
            Telefonalas = 10861,
            Italautomata = 10862,
            Kur = 10863,
            Gyogyszerautomata = 10864,
            Telefonglobal = 61004,
            TSystem = 61005,
            HTTelefon = 61006
        }
    }
}
