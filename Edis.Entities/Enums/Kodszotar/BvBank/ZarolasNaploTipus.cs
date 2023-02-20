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
        public enum ZarolasNaploTipus
        {
            ZarolasUgyintezoAltal = 11570,

            Feloldas = 11571,
            ZarolasHaviZarasAltal = 11572,
            SzabaditasMiattFeloldas = 11573,
            AthelyezesSzabaditasVisszavonasMiatt = 11574,
            FeloldasKarteritesMiatt = 11575,

        }
    }
}
