using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public enum ElojegyzesOkok
        {
            RezsimbeSorolas = 1002622,
            RezsimbeSorolasFelulvizsgalat = 1002623,
            BiztKockBesFelulvizsgalat = 4814,
            AlapvetoJogiPanasz = 1105217,
            // ???
            MunkahelyreElojegyzes = 3020,
            MunkahelyValtozas = 3015,
            MunkahelyTorles = 3021,
            MunkaltatassalKapcsolatosKerlem = 4,
            BiztonsagiKockazatBesorolasValtozas = 4813,
            BiztonsagiCsoportFelulvizsgalas = 3026,
            BiztonsagiCsoportValtozas = 3011,
            FokozatValtas = 1105216,
            UjBefogadas = 3009,
            MegorzesreErkezett = 1105220,
            MegorzesrolVisszaterok = 1105219,
            Beiskolazas = 3029,
            AlacsonyBiztKockReszlegenElhelyezes = 4853,
            ElelmezesiNormaValtozas = 3013,
            KabitoszerPrevenciosReszlegesElhelyezes = 3030,
            GyogyitoCsoport = 3019,
            MunkahelyTorlesHaromHonaponTuliBetegseg = 3022,
            UjIteletetKapott = 3028,
            PszichoSzocialisReszlegenElhelyezes = 4820,
            EgeszsegugyiAllapotValtozas = 3014
        }
    }
}
