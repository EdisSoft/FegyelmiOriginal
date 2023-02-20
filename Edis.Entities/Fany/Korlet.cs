using Edis.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edis.Entities.Fany
{
    [Table("KORLET")]
    public class Korlet : ExtendedBaseEntity, IAzonositovalRendelkezo
    {
        #region mezők
        #endregion

        #region jellemzők

        [Column("AZON_KOD")]
        public  string Azonosito { get; set; }

        [Column("NEV")]
        public  string Nev { get; set; }

      
        [Column("BV_INTEZET_ID")]
        public  int IntezetId { get; set; }

      
        [Column("INTEZETI_OBJEKTUM_ID")]
        public  int IntezetiObjektumId { get; set; }


        //[KodszotarCsoport(Azonosito = "074")]
        //[KulsoKulcs(KesleltetettBetoltesuJellemzoNeve = "KorletTipus")]
        [Column("KORLET_TIP_KSZ_ID")]
        public  int KorletTipusKszId { get; set; }

       
        public virtual Intezet Intezet { get; set; }

        
        public virtual IntezetiObjektum IntezetiObjektum { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "KorletTipusKszId")]
        [ForeignKey("KorletTipusKszId")]
        public virtual Kodszotar KorletTipus { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "Id")]
       
        public virtual IList<Zarka> Zarkak { get; set; }

        public string AzonositoNevTipus
        {
            get { return string.Format("{0} - {1} - {2}", Azonosito, Nev, KorletTipus.Nev); }
        }

        #endregion jellemzők

        #region konstruktor

        public Korlet() : base() { }

        #endregion
    }
}
