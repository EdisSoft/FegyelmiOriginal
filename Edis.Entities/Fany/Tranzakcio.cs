using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("TRANZAKCIO")]
    public class Tranzakcio 
    {
        #region mezők

        #endregion

        #region jellemzők

        #region oszlopok

        [Column("ID")]
        [Key]
        [Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("ROGZITES_DATUM")]
        public DateTime RogzitesDatum { get; set; }

        [Column("ROGZITO_SZEMELY_ID")]
        public int RogzitoSzemelyId { get; set; }

        [Column("ROGZITO_BVINT_ID")]
        public int RogzitoIntezetId { get; set; }

        [Column("ALKALMAZAS_NAPLO_ID")]
        public int? AlkalmazasNaploId { get; set; }

        [Column("ADATBAZIS_BVINT_ID")]
        public int RogzitoAdatbazisIntezetId { get; set; }

        [Column("SZINKRON_FL")]
        public int Szinkronizalva { get; set; }

        [Column("SZINKRON_DATUM")]
        public DateTime? SzinkronizalasDatuma { get; set; }
        #endregion


        //[KesleltetettBetoltesu]
        //public virtual Szemelyzet RogzitoSzemely { get; set; }

        //[KesleltetettBetoltesu]
        //public virtual Intezet RogzitoIntezet { get; set; }

        //[KesleltetettBetoltesu]
        //public virtual AlkalmazasNaplo AlkalmazasNaplo { get; set; }

        //[KesleltetettBetoltesu]
        //public virtual Intezet RogzitoAdatbazisIntezet { get; set; }


        #endregion

        #region konstruktor
        public Tranzakcio() : base() { }

        #endregion

        #region eljárások

        #endregion
    }
}
