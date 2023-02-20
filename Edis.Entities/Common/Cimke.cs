using Edis.Entities.Base;
using Edis.Entities.Fany;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edis.Entities.Common
{
    [Table("Cimkek")]
    public class Cimke : KeziJavitoEntity
    {
        #region jellemzők

        [Column("Nev")]
        public string Nev { get; set; }

        [Column("FelhoId")]
        public int FelhoId { get; set; }

        #endregion

        [ForeignKey(nameof(FelhoId))]
        public Felho Felho { get; set; }
    }
}
