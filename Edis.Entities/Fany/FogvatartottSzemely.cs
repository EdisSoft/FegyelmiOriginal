using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("FOGV_SZEMELY")]
    public class FogvatartottSzemely : ExtendedBaseEntity
    {

        #region jellemzők
        [Column("SZUL_CSALADI_NEV")]
        public string SzuletesiCsaladiNev { get; set; }

        [Column("SZUL_UTONEV")]
        public string SzuletesiUtonev { get; set; }

        [Column("ANYJA_NEVE")]
        public string AnyjaNeve { get; set; }

        [Column("SZULETESI_DATUM")]
        public DateTime SzuletesiDatum { get; set; }

        [Column("IRATTAR_ATVEZETVE_FL")]
        public bool? IrattarAtvezetve { get; set; }

        [Column("NEME_KSZ_ID")]
        public int NemId { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "NemId")]
        //public virtual Kodszotar Nem { get; set; }

        //[InverseProperty("FogvatartottSzemely")]
        public virtual ICollection<FogvatartottSzemelyesAdatai> FogvatartottSzemelyesAdatai { get; set; }

        #endregion jellemzők

        public FogvatartottSzemely()
        {
            FogvatartottSzemelyesAdatai = new HashSet<FogvatartottSzemelyesAdatai>();
        }
    }
}
