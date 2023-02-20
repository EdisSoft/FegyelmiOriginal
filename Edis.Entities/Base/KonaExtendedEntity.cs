using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Base
{
    using Edis.Entities.Fany;

    public class KonaExtendedEntity : BaseEntity
    {
        [Column("ROGZITO_SZEMELY_ID")]
        public int RogzitoSzemelyId { get; set; }
        [NotMapped()]
        /// <summary>  
        ///  Ha azt akarjuk, hogy ne töltse ki a kontextus automatikusan a rögzítőintézetet és személyt, állítsuk true-ra. (Szervízeknél hasznos).  
        /// </summary>  
        public bool KeziRogzitoAdatok { get; set; }

        [Column("ROGZITO_INTEZET_ID")]
        public int RogzitoIntezetId { get; set; }

        [Column("ERVENYESSEG_KEZD")]
        public DateTime ErvenyessegKezdete { get; set; }

        [ForeignKey(nameof(RogzitoIntezetId))]
        public virtual Intezet RogzitoIntezet { get; set; }

        [ForeignKey(nameof(RogzitoSzemelyId))]
        public virtual Szemelyzet RogzitoSzemely { get; set; }

    }
}
