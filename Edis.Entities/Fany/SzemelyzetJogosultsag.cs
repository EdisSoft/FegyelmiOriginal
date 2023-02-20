using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("SZEM_JOGOSULTSAG")]
    public class SzemelyzetJogosultsag : ExtendedBaseEntity, IAzonositovalRendelkezo
    {
        #region jellemzők
        [Column("AZON_KOD")]
        public string Azonosito { get; set; }

        [Column("NEV")]
        public string Nev { get; set; }

        [Column("SZEA_FL")]
        public bool SzervezetiEgysegAlapu { get; set; }

        [Column("GLOBALIS_ADHATO_FL")]
        public bool GlobalisAdhato { get; set; }
        #endregion jellemzők
    }
}
