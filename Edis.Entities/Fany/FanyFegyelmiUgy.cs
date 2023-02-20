using Edis.Entities.Base;
using Edis.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("FEGYELMI_UGY")]
    public class FanyFegyelmiUgy : ExtendedBaseEntity
    {
        [Column("TOROLT_FL")]
        public bool Torolt { get; set; }

        [Column("FOGVATARTOTT_ID")]
        public int FogvatartottId { get; set; }

        [Column("BV_INTEZET_ID")]
        public int IntezetId { get; set; }

        [Column("UGY_INT_SORSZAM")]
        public int UgyIntSorszam { get; set; }

        [Column("UGY_EV")]
        public int UgyEv { get; set; }

        [Column("UGY_STATUSZ_KSZ_ID")]
        public int StatuszKszId { get; set; }


        [ForeignKey("IntezetId")]
        public virtual Intezet Intezet { get; set; }

        [ForeignKey("StatuszKszId")]
        public virtual Kodszotar Statusz { get; set; }
    }
}
