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
    [Table("SZABAD_SZOVEG")]
    public class SzabadSzoveg: ExtendedBaseEntity
    {
        #region oszlopok
        [Column("KAPCS_TABLA_NEV")]
        public string KapcsTablaNev { get; set; }

        [Column("KAPCS_OSZLOP_NEV")]
        public string KapcsOszlopNev { get; set; }

        [Column("KAPCS_TETEL_ID")]
        public int KapcsTetelId { get; set; }

        [Column("BV_INTEZET_ID")]
        public int? BvIntezetId { get; set; }

        [Column("FOGVATARTOTT_ID")]
        public int? FogvatartottId { get; set; }

        [Column("SZOVEG")]
        public string Szoveg { get; set; }
        #endregion

        #region kapcsolódó objektumok

        #endregion
    }
}
