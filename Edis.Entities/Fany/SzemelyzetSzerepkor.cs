using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("SZEM_SZEREPKOR")]
    public class SzemelyzetSzerepkor : ExtendedBaseEntity, IAzonositovalRendelkezo
    {
        #region jellemzők
        [Column("AZON_KOD")]
        public string Azonosito { get; set; }

        [Column("NEV")]
        public string Nev { get; set; }

        [Column("GLOBALIS_FL")]
        public bool Globalis { get; set; }

        [Column("BV_INTEZET_ID")]
        public int? IntezetId { get; set; }

        //[KesleltetettBetoltesu]
        //[ForeignKey("IntezetId")]
        public virtual Intezet Intezet { get; set; }

        //[UIAnnotation(DisplayName = "Összes szerepköri jogosultság")]
        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "Id")]
        //[ForeignKey("SzerepkorId")]
        public virtual ICollection<SzemelyzetSzerepkorJogosultsag> SzerepkorJogosultsagok { get; set; }
        #endregion jellemzők

        public SzemelyzetSzerepkor()
        {
            SzerepkorJogosultsagok = new List<SzemelyzetSzerepkorJogosultsag>();
        }

    }
}
