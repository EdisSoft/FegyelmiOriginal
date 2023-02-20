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
        public enum CelzottPenzMogzasTipus
        {
            [Display(Name = "Feloldás eltáv miatt")]
            FeloldasEltavMiatt = 11520,

            [Display(Name = "Feloldás helyettesítő telefon miatt")]
            FeloldasHelyettesitoTelefonMiatt = 11521,

            [Display(Name = "Feloldás téves könyvelés miatt")]
            FeloldasTevesKonyvelesMiatt = 11522,

            [Display(Name = "Feloldás fogvatartott kérésére")]
            FeloldasFogvatartottKeresere = 11523,

            AthelyezesACelzottPenzbeTevesKonyvelesMiatt = 11524,

            CelzottPenzkentBeerkezoJovairas = 11525,

            FeloldasSzabaditasMiatt = 11526,

            AthelyezesSzabaditasVisszavonasMiatt = 11527,

            TelefonVisszateritesJovairas= 11528,

            SzuksegletiCikkVasarlas = 11529,

            TelefonVisszateritesTerheles = 11530,

            MobilOvadek = 11541,

            MnbUgyfelJavaraErkezoFizetesiDij = 11542,
            ElojegyzesTorlese = 11546
        }
    }
}
