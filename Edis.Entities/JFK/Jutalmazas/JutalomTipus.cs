using Edis.Entities.Base;
using Edis.Entities.Fany;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edis.Entities.JFK.Jutalmazas
{
    [Table("Jutalmazas.JutalomTipusok")]
    public class JutalomTipus : KeziJavitoEntity
    {
        #region oszlopok

        [Column("Nev")]
        public string Nev { get; set; }

        [Column("JogkorCimId")]
        public int JogkorCimId { get; set; }

        [Column("MaxIdotartam")]
        public int? MaxIdotartam { get; set; }

        [Column("MennyisegiEgyseg")]
        public string MennyisegiEgyseg { get; set; }

        [Column("CsakFiatalkoruaknalFl")]
        public bool CsakFiatalkoruaknalFl { get; set; }

        [Column("LetartoztatottIgenybevehetiFl")]
        public bool LetartoztatottIgenybevehetiFl { get; set; }

        [Column("Megjegyzes")]
        public string Megjegyzes { get; set; }

        #endregion oszlopok

        #region kapcsolódó objektumok
       

        #endregion kapcsolódó objektumok
    }
}
