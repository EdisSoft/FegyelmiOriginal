using Edis.Entities.Base;

using Edis.Entities.Fany;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Edis.Entities.Common
{
    [Table("dbo.login_beallitasok")]
    public class LoginBeallitasok : KeziJavitoEntity
    {
        #region oszlopok

        [Column("SZEMELYZET_SID")]
        public string SzemelyzetSid { get; set; }

        [Column("PROJEKT_NEV")]
        public string ProjektNev { get; set; }

        [Column("DEFAULT_INTEZET_ID")]
        public int? DefaultIntezetId { get; set; }

        [Column("UTOLSO_INTEZET_ID")]
        public int? UtolsoIntezetId { get; set; }

        #endregion kapcsolódó objektumok
    }
}

