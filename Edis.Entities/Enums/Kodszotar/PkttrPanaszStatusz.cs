namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public enum PkttrPanaszStatusz
        {
            [EnumOrder(Order = 1)]
            MegalapozottsagVizsgalata = 13002,
            [EnumOrder(Order = 2)]
            EljarasSzuksegessegenekVizsgalata = 13001,
            [EnumOrder(Order = 3)]
            IntezetiKorulmenyekVizsgalata = 13003,
            [EnumOrder(Order = 4)]
            AtszallithatosagVizsgalataMasIntezetbe = 13004,
            [EnumOrder(Order = 5)]
            AtszallitasiHatarozatFogvatartottiJovahagyasa = 13005,
            [EnumOrder(Order = 6)]
            BfbJavaslatHozatal = 13006,
            [EnumOrder(Order = 7)]
            ParancsnokiDontesHozatal = 13007,
            [EnumOrder(Order = 8)]
            ParancsnokiHatarozatFogvatartottiJovahagyasa = 13008,
            [EnumOrder(Order = 9)]
            BvBironakHatarozatKuldese = 12999,
            [EnumOrder(Order = 10)]
            BvBiroDontesHozatal = 13009,
            [EnumOrder(Order = 11)]
            Lezart = 13000
        }
    }
}
