using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Edis.Entities.Enums.Kodszotar
{
    public static partial class KodszotarEnums
    {
        public enum NaploTipus
        {
            ImeiKfSzamMegadas = 1,
            CsakMegjegyzes = 16,
            UjraAktivalas = 17,
            SzolgaltatasHozzaadas = 48,
            TajekoztatasDatuma=49,
            Lezaras=50,
            EsetAtadas=51,
            EszaKerdoivKitoltes = 55,
            AdatvedelmiNyilatkozatAlairas=56,
            KfIgazolastBMtolMegkapta=57,
            EFTFelvetele = 68,
            EFTModositasa=69,
            FelulvizsgalatFelvetele=70,
            FelulvizsgalatModositasa=71,
            CsoportosTajekoztatasNyujtas=79,
            EredmenyIndikatorAfszIgazolas=-2,
            EredmenyIndikatorMunkaszerzodes=-3,
            SzakkepesitestSzerzett = -4,
            BizonyitvanytMegkapta = -5,
            VizsgaraBocsatasFelteteleitTeljesitette = -6,
            ElmeletiVizsgaSikeres = -7,
            GyakorlatiVizsgaSikeres = -8,
            TargoncaHatosagiVizsgaSikeres = -9,
            KFEFTFelvetele = 5074,
            KFLezarasa = 5076,
            KFEFTModositasa = 5080,
            KFFelulvizsgalatFelvetele = 5081,
            KFFelulvizsgalatModositasa = 5082,
            KFEsetatadas = 5100,
            PszichologusiEFTfelvetele = 5101,
            PszichologusiEFTModositas = 5102,
            AdatvedelmiNyilatkozatAlairasKF = 5103,
            HazirendFelvetele = 5104,
            PozitivCovidTeszt = 5105,
            NegativCovidTeszt = 5106
        }
    }
}
