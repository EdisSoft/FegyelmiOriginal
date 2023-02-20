using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.Entities.Base;
using Edis.Entities.Fany;

namespace Edis.Entities.Fany
{
    [Table("FOGVATARTOTT_STATUSZ")]
    public class FogvatartottStatusz : ExtendedBaseEntity
    {
        #region oszlopok
        [Column("FOGV_SZEMELY_ID")]
        public int FogvatartottSzemelyId { get; set; }

        [Column("ELOZO_FOGV_SZEMELY_ID")]
        public int? ElozoFogvatartottSzemelyId { get; set; }

        [Column("NYILV_AZON_KOD")]
        public string NyilvAzonKod { get; set; }

        [Column("ENTITAS")]
        public string Entitas { get; set; }

        [Column("ESEMENY_TIPUS")]
        public string EsemenyTipus { get; set; }

        [Column("ESEMENY_DATUM")]
        public DateTime? EsemenyDatum { get; set; }

        [Column("ESEMENY_SZOVEG")]
        public string EsemenySzoveg { get; set; }

        [Column("ESEMENY_SZOVEG_RESZLETES")]
        public string EsemenySzovegReszletes { get; set; }

        [Column("FANY_TABLA")]
        public string FanyTabla { get; set; }

        [Column("FANY_REKORD")]
        public int FanyRekordId { get; set; }
           

        #endregion

        #region kapcsolódó objektumok
      
        #endregion
    }
}
