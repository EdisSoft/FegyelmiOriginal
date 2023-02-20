using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public enum TavolletOka
        {
            KulkorhazbaKihelyezve = 2781,
            AdaptaciosSzabadsag = 2782,
            BuntetesFelbeszakitason = 2793,
            Eloallitason = 2778,
            Eltavon = 2785,
            Kimaradason = 2786,
            NyomozasraKiadva = 2779,
            RovidtartamuEltavon = 2784,
            VeglegAtszallitva = 2794
        }

        public static List<int> JogszeruenTavollevok
        {
            get
            {
                return new List<int>() {
                      (int)TavolletOka.AdaptaciosSzabadsag,
                      (int)TavolletOka.BuntetesFelbeszakitason,
                      (int)TavolletOka.Eloallitason,
                      (int)TavolletOka.Eltavon,
                      (int)TavolletOka.Kimaradason,
                      (int)TavolletOka.NyomozasraKiadva,
                      (int)TavolletOka.RovidtartamuEltavon,
                      (int)TavolletOka.VeglegAtszallitva,
                };
            }
        }
    }
}
