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
        public enum MunkadijbolSzuksegletiCikkreFodithatoPenzMogzasTipus
        {
            [Display(Name = "Feloldás téves könyvelés miatt")]
            FeloldasTevesKonyvelesMiatt = 11500,

            [Display(Name = "Feloldás fogvatartott kérésére")]
            FeloldasFogvatartottKeresere = 11501,

            [Display(Name = "Áthelyezés téves könyvelés miatt")]
            AthelyezesTevesKonyvelesMiatt = 11502,

            JovairasHokoziHaviZarasMiatt = 10503,
            AthelyezesSzabaditasVisszavonasMiatt = 11504,
            FeloldasSzabaditasMiatt = 11505,
            BizonylatonKeresztulBeerkezoJovairas = 11506,
            SzuksegletiCikkVasarlas = 11507,
            RendszeresZarolas = 11508,

            [Display(Name = "Áthelyezés fogvatartott kérésére")]
            AthelyezesFogvatartottKeresere = 11509,
            MobilOvadek=11540,
            ElojegyzesTorlese=11544
        }
    }
}
