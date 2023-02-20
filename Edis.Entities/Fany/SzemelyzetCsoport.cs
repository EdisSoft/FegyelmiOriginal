using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("SZEMELYZETI_CSOPORT")]
    public class SzemelyzetCsoport : ExtendedBaseEntity , IAzonositovalRendelkezo
    {
        #region jellemzők
        [Column("AZON_KOD")]
        public string Azonosito { get; set; }

        [Column("AD_SID")]
        public string AdSid { get; set; }

        [Column("NEV")]
        public string Nev { get; set; }

        [Column("GLOBALIS_FL")]
        public int Globalis { get; set; }

        [Column("BV_INTEZET_ID")]
        public int? IntezetId { get; set; }

        //[UIAnnotation(DisplayName = "Összes szerepkör csoport")]
        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "Id")]
        //public virtual IList<SzemelyzetCsoportSzerepkor> CsoportSzerepkorok { get; set; }

        //[KesleltetettBetoltesu]
        //public virtual Intezet Intezet { get; set; }
        #endregion jellemzők
    }
}
