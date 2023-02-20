using Edis.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edis.Entities.Fany
{
    [Table("dbo.vZarkaLetszam")]
    public class vZarkaLetszam : SoftDeleteEntity
    {
        #region oszlopok
        [Column("ID")]
        public int Id { get; set; }

        [Column("DATUM")]
        public DateTime Datum { get; set; }

        [Column("ZARKA_ID")]
        public int ZarkaId { get; set; }

        [Column("BV_INTEZET_ID")]
        public int BvIntezetId { get; set; }

        [Column("BEFOGADOKEP")]
        public int Befogadokep { get; set; }

        [Column("LETSZAM")]
        public int? Letszam { get; set; }

        #endregion oszlopok

        #region kapcsolódó objektumok

        [ForeignKey(nameof(ZarkaId))]
        public virtual Zarka Zarka { get; set; }

        #endregion kapcsolódó objektumok
    }
}
