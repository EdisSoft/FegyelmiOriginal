using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Base
{
    public class SoftDeleteEntity
    {
        [NotMapped]
        public bool Torolt
        {
            get { return this.TOROLT_FL; }
        }

        [Column("TOROLT_FL")]
        public bool TOROLT_FL { get; set; }
    }
}
