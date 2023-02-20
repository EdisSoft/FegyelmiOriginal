using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("Felhok")]
    public class Felho : KeziJavitoEntity
    {
        #region jellemzők
      
        [Column("NEV")]
        public string Nev { get; set; }

        [Column("MEGJEGYZES")]
        public string Megjegyzes { get; set; }

        #endregion

    }
}
