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
        // csoport id: 1021

        public enum KarteritesStatusz
        {
            [Display(Name = "Meghallgatás alatt")]
            MeghallgatasAlatt = 10297,
            [Display(Name = "Jogerős")]
            Jogeros = 10298,
            [Display(Name = "Bírósági fellebbezés alatt")]
            BirosagiFellebbezesAlatt = 10299,
            [Display(Name = "Bv kárviselés")]
            BvKarviseles = 10300,
            [Display(Name = "Eljárás megszüntetve")]
            EljarasMegszuntetve = 10301,
            [Display(Name = "Megfizetett")]
            Megfizetett = 10302,
            [Display(Name = "Határozat elkészült")]
            HatarozatElkeszult = 10749,
            [Display(Name = "Leírva")]
            Leirva = 10850,
            [Display(Name = "Vh átadva")]
            VhAtadva = 10851,
            [Display(Name = "Elévült")]
            Elevult = 10857,
            [Display(Name = "Automatikus leírás")]
            AutomatikusLeiras = 10887,
            [Display(Name = "Kérelmi lap alapján történő leírás")]
            KerelemLapAlapjanTortenoLeiras = 10888,
            [Display(Name = "Kárbejelentés alatt")]
            KarbejelentesAlatt = 10889,

            [Display(Name = "Meghallgatva")]
            Meghallgatva = 1102642,
            [Display(Name = "Téves rögzítés")]
            TevesRogzites = 1102643,
            [Display(Name = "Hosszabbítás alatt")]
            HosszabbitasAlatt = 1102644,
            [Display(Name = "Szüneteltetve")]
            Szuneteltetve = 1102645,
            [Display(Name = "Határozat ismertetése")]
            HatarozatIsmertetese = 1103950,
            [Display(Name = "NAV-nak átadva")]
            NAVnakAtadva = 1111467

        }


    }

    public static class KarteritesStatuszCsoportositasok
    {
        public static List<int> FolyamatbanLevok = new List<int>()
        {
            (int)KodszotarEnums.KarteritesStatusz.BirosagiFellebbezesAlatt
            ,(int)KodszotarEnums.KarteritesStatusz.HatarozatElkeszult
            ,(int)KodszotarEnums.KarteritesStatusz.HatarozatIsmertetese
            ,(int)KodszotarEnums.KarteritesStatusz.HosszabbitasAlatt
            ,(int)KodszotarEnums.KarteritesStatusz.KarbejelentesAlatt
            ,(int)KodszotarEnums.KarteritesStatusz.MeghallgatasAlatt
            ,(int)KodszotarEnums.KarteritesStatusz.Meghallgatva
            ,(int)KodszotarEnums.KarteritesStatusz.Szuneteltetve
            ,(int)KodszotarEnums.KarteritesStatusz.NAVnakAtadva
        };

        public static List<int> Kifutottak = new List<int>()
        {
            (int)KodszotarEnums.KarteritesStatusz.Megfizetett
            ,(int)KodszotarEnums.KarteritesStatusz.BvKarviseles
            ,(int)KodszotarEnums.KarteritesStatusz.Leirva
        };

        public static List<int> Aktivak = new List<int>()
        {
            (int)KodszotarEnums.KarteritesStatusz.Jogeros
            ,(int)KodszotarEnums.KarteritesStatusz.VhAtadva
        };

        public static List<int> Megszuntek = new List<int>()
        {
            (int)KodszotarEnums.KarteritesStatusz.BvKarviseles
            ,(int)KodszotarEnums.KarteritesStatusz.EljarasMegszuntetve
            ,(int)KodszotarEnums.KarteritesStatusz.Megfizetett
            ,(int)KodszotarEnums.KarteritesStatusz.Leirva
            ,(int)KodszotarEnums.KarteritesStatusz.Elevult
            ,(int)KodszotarEnums.KarteritesStatusz.AutomatikusLeiras
            ,(int)KodszotarEnums.KarteritesStatusz.KerelemLapAlapjanTortenoLeiras
            ,(int)KodszotarEnums.KarteritesStatusz.TevesRogzites
        };

        public static List<int> HetNaponBelulLejaroFolyamatbanLevok = new List<int>()
                                                           {
                (int)KodszotarEnums.KarteritesStatusz.BirosagiFellebbezesAlatt
                ,(int)KodszotarEnums.KarteritesStatusz.HatarozatElkeszult
                ,(int)KodszotarEnums.KarteritesStatusz.HosszabbitasAlatt
                , (int)KodszotarEnums.KarteritesStatusz.KarbejelentesAlatt
                , (int)KodszotarEnums.KarteritesStatusz.MeghallgatasAlatt
                , (int)KodszotarEnums.KarteritesStatusz.Meghallgatva
                , (int)KodszotarEnums.KarteritesStatusz.HatarozatIsmertetese
                                                           };
    }
}
