using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
       
        public enum BelsoAtvezetesiTranzakcioJogcim
        {
            BeazonositottIsmeretlenTetel = 10401,
            Munkadij = 10402,
            Osztondij = 10403,
            SzolgaltatoiDij = 10404,
            Utankuldes = 10405,
            Utalvany = 10406,
            Egyeb = 10407,
            Postakoltseg = 10408,
            PenzkuldesMasFogvatartottnak = 10409,
            PenzkuldesMasFogvatartottnakCelzottan = 10443,
            ElojegyzettBevetel = 10410,
            Penzmaradvany = 10411,
            TevesenRogzitettTelefonViszaterites = 10412,
            TevesKonyvelesRendezese = 10415,
            Rabtartasdij = 10427,
            TerapiasMunkadij = 10428,

            Adomany = 10438,
            Vasarlas = 10439,
            Karterites=10445,
            BvTartozas=10446,
            IntezetekKozottiHitelezes=10499,
            TelefonVisszaterites= 10530,
            LetiltasVegrehajtoiSikerdij= 10531
        }
    }
}
