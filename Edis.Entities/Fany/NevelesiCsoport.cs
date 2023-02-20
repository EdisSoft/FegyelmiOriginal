using Edis.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edis.Entities.Fany
{
    [Table("NEVELESI_CSOPORT")]
    public class NevelesiCsoport : ExtendedBaseEntity, IAzonositovalRendelkezo
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


        [Column("NEVELO_SZEMELY_ID")]
        public int NeveloSzemelyId { get; set; }

        [Column("NEVELO_SZEMELY_SID")]
        public string NeveloSzemelySid { get; set; }


        [Column("HELYETTES_NEV_SZEMELY_ID")]
        public int? HelyettesNeveloSzemelyId { get; set; }
        [Column("HELYETTES_NEV_SZEMELY_SID")]
        public string HelyettesNeveloSzemelySid { get; set; }


        public virtual Intezet Intezet { get; set; }

       
        public virtual IntezetiObjektum IntezetiObjektum { get; set; }

        
        public virtual Szemelyzet NeveloSzemely { get; set; }

       
        public virtual Szemelyzet HelyettesNeveloSzemely { get; set; }

        #endregion jellemzők

        #region konstruktor

        #endregion

        public override string ToString()
        {
            return $"Név: {Nev}, Intézet: {IntezetId}";
        }

        public static string NevelesiCsoportFormazas(NevelesiCsoport entitas)
        {
            return entitas == null
                ? null
                : $"{entitas.Azonosito} {entitas.Nev} {Intezet.IntezetFormazas(entitas.Intezet)} {Szemelyzet.SzemelyzetFormazas(entitas.NeveloSzemely)}";
        }
    }
}
