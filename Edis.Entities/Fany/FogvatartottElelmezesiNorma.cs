using Edis.Entities.Base;
using Edis.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edis.Entities.Fany
{
    [Table("dbo.fogv_elelmez_norma")]
    public class FogvatartottElelmezesiNorma : ExtendedBaseEntity
    {

        #region oszlopok
        [Column("NORMA_KEZD")]
        public DateTime NormaKezd { get; set; }

        [Column("NORMA_VEGE")]
        public DateTime? NormaVege { get; set; }

        [Column("FOGVATARTOTT_ID")]
        public int FogvatartottId { get; set; }

        [Column("ELELMEZ_NORMA_ARCH_KSZ_ID")]
        public int? ElelmezesiNormaArchKszId { get; set; }

        [Column("ELELMEZ_NORMA_KSZ_ID")]
        public int? ElelmezesiNormaKszId { get; set; }

        [ForeignKey(nameof(FogvatartottId))]
        public virtual FogvatartottNezet Fogvatartott { get; set; }

        [ForeignKey(nameof(ElelmezesiNormaKszId))]
        public virtual Kodszotar ElelmezesiNormaKodszotar { get; set; }

        #endregion kapcsolódó objektumok

        #region eljárások
        public FogvatartottElelmezesiNorma()
        {
        }
        #endregion eljárások
    }
}
