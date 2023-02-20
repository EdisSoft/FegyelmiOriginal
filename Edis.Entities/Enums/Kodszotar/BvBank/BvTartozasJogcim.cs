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
        public enum BvTartozasJogcim
        {
            [Display(Name = "Fizetett szállítás")]
            FizetettSzallitas = 10290,

            [Display(Name = "Egyészségügyi segédeszköz")]
            EgeszsegugyiSegedeszkoz = 10291,

            [Display(Name = "Rabtartásdíj")]
            Rabtartasdij = 10292,

            [Display(Name = "Gyógyszer")]
            Gyogyszer = 10293,

            [Display(Name = "Tisztasági szer")]
            TisztasagiSzer = 10422,

            [Display(Name = "Fénymásolás")]
            Fenymasolas = 10423,

            [Display(Name = "EÜ. szolgáltatatás")]
            EuSzolgaltatatas = 10424,

            [Display(Name = "Telefonkártya pótlás")]
            TelefonkartyaPotlas = 10425,

            [Display(Name = "Tartásdíj")]
            Tartasdij = 10442,

            [Display(Name = "Egyéb")]
            Egyeb = 10440,

            [Display(Name = "Második tisztasági csomag")]
            MasodikTisztasagiCsomag = 10441,
            [Display(Name = "Konditerem, hűtőgép, vízmelegítő")]
               Kulonszolgaltatas = 1122242,
            [Display(Name = "Kimaradás, eltávozás")]
              KimaradasEltavozas =  1122243,
        }
    }
}
