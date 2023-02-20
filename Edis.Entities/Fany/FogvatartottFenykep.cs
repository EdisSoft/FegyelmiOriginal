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
    [Table("fogv_fenykep")]
    public class FogvatartottFenykep : ExtendedBaseEntity
    {
        [Column("FOGVATARTOTT_ID")]
        public int FogvatartottId { get; set; }

        [Column("KISINDEXKEP")]
        public byte[] KisindexkepNeHasznald { get; set; }
        [NotMapped]
        public byte[] Kisindexkep
        {
            get
            {
                if (Fogvatartott != null)
                    return Fogvatartott.Vedett == true ? null : KisindexkepNeHasznald;
                else
                    return FogvatartottNezet.Vedett == true ? null : KisindexkepNeHasznald;
            }
        }

        [Column("KISINDEXKEP100")]
        public byte[] Kisindexkep100NeHasznald { get; set; }
        [NotMapped]
        public byte[] Kisindexkep100
        {
            get
            {
                if (Fogvatartott != null)
                    return Fogvatartott.Vedett == true ? null : Kisindexkep100NeHasznald;
                else
                    return FogvatartottNezet.Vedett == true ? null : Kisindexkep100NeHasznald;
            }
        }

        [Column("NAGYINDEXKEP")]
        public byte[] NagyindexKepNeHasznald { get; set; }
        [NotMapped]
        public byte[] NagyindexKep
        {
            get
            {
                if (Fogvatartott != null)
                    return Fogvatartott.Vedett == true ? null : NagyindexKepNeHasznald;
                else
                    return FogvatartottNezet.Vedett == true ? null : NagyindexKepNeHasznald;
            }
        }

        [Column("INDEXKEP_FELIRAT_NELKUL")]
        public byte[] IndexkepFeliratNelkulNeHasznald { get; set; }
        [NotMapped]
        public byte[] IndexkepFeliratNelkul
        {
            get
            {
                if (Fogvatartott != null)
                    return Fogvatartott.Vedett == true ? null : IndexkepFeliratNelkulNeHasznald;
                else
                    return FogvatartottNezet.Vedett == true ? null : IndexkepFeliratNelkulNeHasznald;
            }
        }

        [Column("FELIRATOS")]
        public bool? Feliratos { get; set; }

        [ForeignKey("FogvatartottId")]
        public virtual Fogvatartott Fogvatartott { get; set; }

        [ForeignKey("FogvatartottId")]
        public virtual FogvatartottNezet FogvatartottNezet { get; set; }
    }
}
