using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Base
{
    public class NotFilteredBaseEntity : SoftDeleteEntity
    {
        [Column("ID")]
        [Key]
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ROGZITO_SZEMELY_ID")]
        public int RogzitoSzemelyId { get; set; }

        [Column("ROGZITO_INTEZET_ID")]
        public int RogzitoIntezetId { get; set; }

        [Column("ERVENYESSEG_KEZD")]
        public DateTime ErvenyessegKezdete { get; set; }

    }
}
