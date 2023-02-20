using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        /// <summary>
        /// Étkezés típus kódszótár csoport: 1607
        /// </summary>
        public enum BfbDontesTipus
        {
            /// <summary>
            /// Rezsim besorolás
            /// </summary>
            RezsimBeso = 70000,
            /// <summary>
            /// Biztonsági besorolás
            /// </summary>
            BiztBeso = 70001,
            /// <summary>
            /// Zárkába helyezés előjegyzés
            /// </summary>
            ZarkHelyEl = 70002,
            /// <summary>
            /// Foglalkoztatás beállítás
            /// </summary>
            FoglBeall = 70003,
            /// <summary>
            /// Speciális részlegbe helyezés
            /// </summary>
            SpecReszHe = 70004,
            /// <summary>
            /// Munkára kötelezettség felülvizsgálat
            /// </summary>
            MunkKotFel = 70005,
            /// <summary>
            /// Étkezési norma beállítás
            /// </summary>
            EtkNorBeal = 70006,
            /// <summary>
            /// Oktatás beállítás
            /// </summary>
            OktBeall = 70007,
            /// <summary>
            /// Reintegrációs csoportba helyezés
            /// </summary>
            ReintCsop = 70008,
            /// <summary>
            /// EFOP projektbe bevonás
            /// </summary>
            EFOProj = 70009,
            /// <summary>
            /// BFB ülésen megjelent
            /// </summary>
            BfbUlMeg = 70010,
            /// <summary>
            /// Vélemény készült
            /// </summary>
            Velemeny = 70011,

            /// <summary>
            /// Vélemény készült
            /// </summary>
            Indoklas = 70015,

            /// <summary>
            /// Vélemény készült
            /// </summary>
            ReintOrizet = 70016
        }
    }
}
