using Edis.Entities.Base;
using Edis.Entities.JFK.FENY;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("FEGYELMI_FENYITES")]
    public class FegyelmiFenyites : ExtendedBaseEntity
    {
        #region mezők
        #endregion mezők

        #region jellemzők
        #region oszlopok
        [Column("TOROLT_FL")]
        public bool Torolt { get; set; }

        [Column("FOGVATARTOTT_ID")]
        public int FogvatartottId { get; set; }

        [Column("KIETK_CSOKK_MERTEK")]
        public int? KiertCsokkMertek { get; set; }

        [Column("KIETK_CSOKK_KEZDET")]
        public DateTime? KiertCsokkKezdet { get; set; }

        [Column("STATUSZ_KSZ_ID")]
        public int StatuszId { get; set; }

        [Column("FENYITES_TIPUS_KSZ_ID")]
        public int FenyitesTipusKszId { get; set; }

        #endregion oszlopok

        #region kapcsolódó objektumok
        
        [ForeignKey("StatuszId")]
        public virtual Kodszotar Statusz { get; set; }

        #endregion kapcsolódó objektumok

        #endregion jellemzők

        #region eljárások
        //public override string ToString()
        //{
        //    return Nev;
        //}

        //public string KodNev { get { return HelysegFormazas(this); } }

        //public static string HelysegFormazas(Helyseg helyseg)
        //{
        //    return helyseg != null ? HelysegFormazas(helyseg.Azonosito, helyseg.Nev) : null;
        //}

        //public static string HelysegFormazas(string azonosito, string nev)
        //{
        //    return string.Format("{0}, {1}", azonosito, nev);
        //}
        #endregion eljárások

    }
}
