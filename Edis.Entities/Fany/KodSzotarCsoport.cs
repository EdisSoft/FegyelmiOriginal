using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("KODSZOTAR_CSOPORT")]
    public class KodszotarCsoport : ExtendedBaseEntity, IAzonositovalRendelkezo
    {

        #region jellemzők
        [Column("AZON_KOD")]
        public string Azonosito { get; set; }

        [Column("NEV")]
        public string Nev { get; set; }

        [Column("MEGJEGYZES")]
        public string Megjegyzes { get; set; }

        //[UIAnnotation(DisplayName = "Összes kódszótár eleme")]
        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "Id")]
        //public virtual IList<Kodszotar> KodszotarElemek { get; set; }
        #endregion

    }
}
