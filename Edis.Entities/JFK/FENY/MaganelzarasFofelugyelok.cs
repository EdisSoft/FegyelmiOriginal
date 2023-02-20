using Edis.Entities.Base;
using Edis.Entities.Fany;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.JFK.FENY
{
    [Table("Fegyelmi.MaganelzarasFofelugyelok")]
    public class MaganelzarasFofelugyelok : KeziJavitoEntity
    {
        [Column("FofelugyeloId")]
        public int FofelugyeloId { get; set; }

        [ForeignKey(nameof(FofelugyeloId))]
        public virtual Szemelyzet FelugyeloSzemely { get; set; }

        [Column("FegyelmiUgyId")]
        public int FegyelmiUgyId { get; set; }

        [ForeignKey(nameof(FegyelmiUgyId))]
        public virtual FegyelmiUgy FegyelmiUgy { get; set; }

        [Column("NaploId")]
        public int NaploId { get; set; }

        [ForeignKey(nameof(NaploId))]
        public virtual Naplo Naplo { get; set; }
    }
}
