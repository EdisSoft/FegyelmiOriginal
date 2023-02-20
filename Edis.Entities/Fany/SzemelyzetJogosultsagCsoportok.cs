using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("SZEM_JOGOSULTSAG_CSOPORTOK")]
    public class SzemelyzetJogosultsagCsoportok : ExtendedBaseEntity
    {

        [Column("MENU_VALUE")]
        public string MenuValue { get; set; }

        [Column("MENU_NEV")]
        public string MenuNev { get; set; }

        [Column("MENU_ID")]
        public int MenuId { get; set; }

        [Column("MENU_PARENT_ID")]
        public int? MenuParentId { get; set; }

        [Column("JOGOSULTSAG_AZON_KOD")]
        public string JogosultsagAzonosito { get; set; }

        [Column("JOGOSULTSAG_NEV")]
        public string JogosultsagNev { get; set; }

        [Column("JOGOSULTSAG_ID")]
        public int? JogosultsagId { get; set; }

        //[KesleltetettBetoltesu]
        //public virtual SzemelyzetJogosultsag Jogosultsag { get; set; }

    }
}
