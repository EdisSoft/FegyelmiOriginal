using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("KODSZOTAR")]
    public class Kodszotar : ExtendedBaseEntity, IAzonositovalRendelkezo
    {
        #region mezők
        #endregion

        #region jellemzők
        [Column("AZON_KOD")]
        public string Azonosito { get; set; }

        [Column("KODCSOP_AZON_KOD")]
        public string KodszotarCsoportAzonosito { get; set; }

        [Column("NEV")]
        public string Nev { get; set; }

        [Column("ROVID_NEV")]
        public string RovidNev { get; set; }

        [Column("KODSZOTAR_CSOPORT_ID")]
        public int KodszotarCsoportId { get; set; }

        //[KesleltetettBetoltesu]
        //public virtual KodszotarCsoport KodszotarCsoport { get; set; }
        //#endregion

        //#region metodusok

        //public string KodNev { get { return KodszotarFormazas(this); } }

        //public static string KodszotarFormazas(Kodszotar kodszotar)
        //{
        //    return kodszotar != null ? KodszotarFormazas(kodszotar.Azonosito, kodszotar.Nev) : null;
        //}

        //public static string KodszotarFormazas(string azonosito, string nev)
        //{
        //    return string.Format("{0}, {1}", azonosito, nev);
        //}

        //public override string ToString()
        //{
        //    return Nev;
        //}
        #endregion
    }
}
