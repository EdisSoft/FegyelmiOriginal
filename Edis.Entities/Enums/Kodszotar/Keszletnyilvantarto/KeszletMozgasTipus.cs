using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public enum KeszletMozgasTipus
        {
            Leltar = 1105524,
            Selejtezes = 1105548,
            Vasarlas = 1105526,
            Visszaru = 1105550,
            RaktarkoziMozgas = 1105552,    
            SajatFelhasznalas = 1105549,
            KulsoPartnerFeleTortenoErtekesites = 1105551,
            Csomag = 1105553,
            //VisszaruBeszSzamlaAlapjan = 1105554,
            Bevetelezes = 1105555,
            Osszevezetes = 1105556,
            FelNemHasznaltAnyagVisszavetele = 1105557,
            EladasKorrekcio = 1105558,
            VisszaruBeszallitoiSzamlaAlapjan = 1105554,
            IdokoziLeltar = 1107061,
            DolgozoiCsomag = 1105559,

        }

        public enum KimutatasBevetelezes
        {
            Bevetelezes = KeszletMozgasTipus.Bevetelezes,
            FelNemHasznaltAnyagVisszavetele = KeszletMozgasTipus.FelNemHasznaltAnyagVisszavetele,
            EladasKorrekcio = KeszletMozgasTipus.EladasKorrekcio,
            RaktarkoziMozgas = KeszletMozgasTipus.RaktarkoziMozgas,
            Osszevezetes = KeszletMozgasTipus.Osszevezetes,
            Leltar = KeszletMozgasTipus.Leltar,
            IdokoziLeltar = KeszletMozgasTipus.IdokoziLeltar
        }

        public enum KimutatasKiadas
        {
            Selejtezes = KeszletMozgasTipus.Selejtezes,
            Vasarlas = KeszletMozgasTipus.Vasarlas,
            Visszaru = KeszletMozgasTipus.Visszaru,
            RaktarkoziMozgas = KeszletMozgasTipus.RaktarkoziMozgas,
            SajatFelhasznalas = KeszletMozgasTipus.SajatFelhasznalas,
            KulsoPartnerFeleTortenoErtekesites = KeszletMozgasTipus.KulsoPartnerFeleTortenoErtekesites,
            Csomag = KeszletMozgasTipus.Csomag,
            VisszaruBeszSzamlaAlapjan = KeszletMozgasTipus.VisszaruBeszallitoiSzamlaAlapjan,
            Osszevezetes = KeszletMozgasTipus.Osszevezetes,
            Leltar = KeszletMozgasTipus.Leltar,
            IdokoziLeltar = KeszletMozgasTipus.IdokoziLeltar,
            DolgozoiCsomag = 1105559,
        }
    }
}
