using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Cimke
{
    public static partial class CimkeEnums
    {
        public enum IktatottDokumentumTipusok
        {
            ErtesitoLap = 201,
            ElkulonitesiLap=204,
            FegyelmiLap = 207,
            TajekoztatasNemKer = 208,
            TajekozatasKer = 209,
            EljarasAlaVontMeghallgatasa= 211,
            TanuMeghallgatasa= 212,
            SzemelyiAllomanyiTanuMeghallgatasa = 214,
            TargyalasiJegyzokonyv = 225,
            FegyelmiHatarozat = 249,
            FegyelmiHatarozatMegszuntetese = 250,
            ReintegraciosTisztiHatarozat = 253,
            ReintegraciosTisztiKioktatas = 280,
            KirendeltVedoKereseNyilatkozat = 281,
            KirendeltVedoKerese = 282,
            MeghatalmazottVedoKereseNyilatkozat = 283,
            MeghatalmazottVedoKerese = 284,
            VedoTelefonosTajekoztatasa = 285,
            MasodfokuTargyalasiJegyzokonyv = 287,
            MasodfokuHatarozat = 294,
            MasodfokuHatarozatMegszuntetese = 295,
            SzembesitesiJegyzokonyv = 296,
            MaganelzarasMegkezdese = 303,
            FeljegyzesMegbeszelesrolMegallapodasNyomtatvany = 310,
            KozvetitoiEljarasKezdemenyezese = 326,
            AlairasMegtagadasa = 330,
            OsszefoglaloJelentes = 331,
            OsszefoglaloJelentesDocx = 340,
            KarjelentoLap = 345,
            BvBankKarjelentoLap = 347,
            BuntetoFeljelentes = 348
        }
    }
}
