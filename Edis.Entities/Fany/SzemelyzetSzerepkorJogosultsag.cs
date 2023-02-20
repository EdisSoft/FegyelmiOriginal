using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("SZEM_SZEREPKOR_JOGOSULTSAG")]
    public class SzemelyzetSzerepkorJogosultsag : ExtendedBaseEntity
    {
        #region jellemzők
        [Column("SZEREPKOR_ID")]
        public int SzerepkorId { get; set; }

        [Column("JOGOSULTSAG_ID")]
        public int JogosultsagId { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "JogosultsagId")]
        [ForeignKey("JogosultsagId")]
        public virtual SzemelyzetJogosultsag SzemelyzetJogosultsag { get; set; }
        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "SzerepkorId")]
        [ForeignKey("SzerepkorId")]
        public virtual SzemelyzetSzerepkor SzemelyzetSzerepkor { get; set; }


        #endregion jellemzők
    }
}
