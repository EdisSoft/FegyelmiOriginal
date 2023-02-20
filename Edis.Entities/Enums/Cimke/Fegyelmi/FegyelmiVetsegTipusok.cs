using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums.Cimke
{
    public static partial class CimkeEnums
    {
        public enum FegyelmiVetsegTipusok
        {
            AllamiTulajdonRongalasa = 98,
            AllamiTulajdonRongalasaMobil = 99,
            BfrolVisszateresElmulasztasa = 101,
            EltavozasrolVisszateresElmulasztasa = 104,
            FajtalansagraKenyszerites = 107,
            Fogolyszokes = 109,
            FogolyszokesKiserleteElokeszulete = 110,
            Fogolyzendules = 112,
            FogvatartottTarsBantalmazasa8NaponBelulGyogyulo = 113,
            FogvatartottTarsBantalmazasa8NaponTulGyogyulo = 114,
            FogvatartottTarsKihasznalasaZsarolasa = 115,
            FogvatartottTarsSanyargatasa = 116,
            KimaradasrolVisszateresElmulasztasa = 122,
            MeghatarozottFeladatElvegzesenekMegtagadasa = 125,
            //RovidtartamuEltavrolVisszateresElmulasztasa = ???,
            SzemelyzetTagjaElleniEroszak = 132,
            SzemelyzetTagjanakMegsertese = 133,
            SzemelyzetUtasitasaVegrehajtasanakMegtagadasa = 134,
            SzeszesitalBoditoszerFogyasztasaHasznalata = 137,
            TiltottTargyKesziteseTartasaCsereje = 179,
            Verekedes = 146,
            Vesztegetes = 177
        }

        public static List<int> BfbElojegyzesFegyelmiVetsegTipusAlapjan
        {
            get
            {
                return new List<int>() {
                    (int)FegyelmiVetsegTipusok.BfrolVisszateresElmulasztasa,
                    (int)FegyelmiVetsegTipusok.EltavozasrolVisszateresElmulasztasa,
                    (int)FegyelmiVetsegTipusok.FajtalansagraKenyszerites,
                    (int)FegyelmiVetsegTipusok.Fogolyszokes,
                    (int)FegyelmiVetsegTipusok.FogolyszokesKiserleteElokeszulete,
                    (int)FegyelmiVetsegTipusok.Fogolyzendules,
                    (int)FegyelmiVetsegTipusok.FogvatartottTarsBantalmazasa8NaponBelulGyogyulo,
                    (int)FegyelmiVetsegTipusok.FogvatartottTarsBantalmazasa8NaponTulGyogyulo,
                    (int)FegyelmiVetsegTipusok.FogvatartottTarsKihasznalasaZsarolasa,
                    (int)FegyelmiVetsegTipusok.FogvatartottTarsSanyargatasa,
                    (int)FegyelmiVetsegTipusok.KimaradasrolVisszateresElmulasztasa,
                    (int)FegyelmiVetsegTipusok.MeghatarozottFeladatElvegzesenekMegtagadasa,
                    (int)FegyelmiVetsegTipusok.SzemelyzetTagjaElleniEroszak,
                    (int)FegyelmiVetsegTipusok.SzemelyzetTagjanakMegsertese,
                    (int)FegyelmiVetsegTipusok.SzemelyzetUtasitasaVegrehajtasanakMegtagadasa,
                    (int)FegyelmiVetsegTipusok.SzeszesitalBoditoszerFogyasztasaHasznalata,
                    (int)FegyelmiVetsegTipusok.TiltottTargyKesziteseTartasaCsereje,
                    (int)FegyelmiVetsegTipusok.Verekedes,
                    (int)FegyelmiVetsegTipusok.Vesztegetes
                };
            }            
        }
    }
}
