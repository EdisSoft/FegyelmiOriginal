using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("SZEM_CSOPORT_SZEREPKOR")]
    public class SzemelyzetCsoportSzerepkor : ExtendedBaseEntity
    {
        #region jellemzők
        [Column("SZEM_CSOP_ID")]
        public int SzemelyzetCsoportId { get; set; }

        [Column("SZEREPKOR_ID")]
        public int SzerepkorId { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "SzemelyzetCsoportId")]
        //public virtual SzemelyzetCsoport SzemelyzetCsoport { get; set; }
        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "SzerepkorId")]
        //public virtual SzemelyzetSzerepkor SzemelyzetSzerepkor { get; set; }
        #endregion jellemzők

    }
}
