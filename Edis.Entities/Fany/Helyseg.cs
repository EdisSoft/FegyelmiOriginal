using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("HELYSEG")]
    public class Helyseg : ExtendedBaseEntity, IAzonositovalRendelkezo
    {
        #region mezők
        #endregion mezők

        #region jellemzők
        #region oszlopok
        [Column("AZON_KOD")]
        public string Azonosito { get; set; }

        [Column("NEV")]
        public string Nev { get; set; }

        [Column("IRANYITOSZAM")]
        public int? Iranyitoszam { get; set; }

        ////[KulsoKulcs(KesleltetettBetoltesuJellemzoNeve = "Megye")]
        //[Column("MEGYE_KSZ_ID")]
        //public int? MegyeKszId { get; set; }
        #endregion oszlopok
        
        #region kapcsolódó objektumok
        
        ////[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "MegyeKszId")]
        //public virtual Kodszotar Megye { get; set; }


        #endregion kapcsolódó objektumok

        #endregion jellemzők

        #region eljárások
        public override string ToString()
        {
            return Nev;
        }

        public string KodNev { get { return HelysegFormazas(this); } }

        public static string HelysegFormazas(Helyseg helyseg)
        {
            return helyseg != null ? HelysegFormazas(helyseg.Azonosito, helyseg.Nev) : null;
        }

        public static string HelysegFormazas(string azonosito, string nev)
        {
            return string.Format("{0}, {1}", azonosito, nev);
        }
        #endregion eljárások

    }
}
