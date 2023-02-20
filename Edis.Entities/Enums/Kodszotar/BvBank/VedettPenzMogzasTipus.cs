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
        public enum VedettPenzMogzasTipus
        {
            [Display(Name = "Feloldás téves könyvelés miatt")]
            FeloldasTevesKonyvelesMiatt = 11510,

            [Display(Name = "Feloldás fogvatartott kérésére")]
            FeloldasFogvatartottKeresere = 11511,

            AthelyezesAVedettPenzbeTevesKonyvelesMiatt = 11512,

            VedettPenzkentBeerkezoJovairas = 10513,
            AthelyezesSzabaditasVisszavonasMiatt = 11514,
            FeloldasSzabaditasMiatt = 11515,
            SzuksegletiCikkVasarlas = 11516,
            HaviZaras=11517,
            HokoziZaras=11518,
            MobilOvadek=11519,
            MnbUgyfelJavaraErkezoFizetesiDij = 11543,
            ElojegyzesTorlese = 11545
        }
    }
}
