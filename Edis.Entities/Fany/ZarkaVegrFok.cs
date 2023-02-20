using Edis.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edis.Entities.Fany
{
    [Table("ZARKA_VEGR_FOK")]
    public class ZarkaVegrFok : ExtendedBaseEntity
    {
        #region jellemzők

        [Column("ZARKA_ID")]
        public  int ZarkaId { get; set; }

        [Column("VEGREHAJTASI_FOK_KSZ_ID")]
        public  int VegrehajtasiFokKszId { get; set; }

        [ForeignKey(nameof(VegrehajtasiFokKszId))]
        public virtual Kodszotar VegrehajtasiFok { get; set; }

        [ForeignKey(nameof(ZarkaId))]
        public virtual Zarka Zarka { get; set; }
        

        #endregion jellemzők
    }
}
