using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("dbo.fogvatartotti_kartya")]
    public class FogvatartottiKartya : ExtendedBaseEntity
    {

        #region oszlopok
        [Column("AZON_KOD")]
        public string KartyaKod { get; set; }

        [Column("FOGVATARTOTT_ID")]
        public int FogvatartottId { get; set; }

        [Column("ERVENYESSEG_KEZDETE_DATUM")]
        public DateTime KartyaErvenyessegKezdete { get; set; }

        [Column("ERVENYESSEG_VEGE_DATUM")]
        public DateTime? KartyaErvenyessegVege { get; set; }

        [Column("LETILTAS_DATUM")]
        public DateTime? LetiltasDatuma { get; set; }

        [Column("SORSZAM")]
        public int Sorszam { get; set; }
        
        [Column("MEGJEGYZES")]
        public string Megjegyzes { get; set; }

        [ForeignKey("FogvatartottId")]
        public virtual Fogvatartott Fogvatartott { get; set; }
        #endregion kapcsolódó objektumok

        #region eljárások
        public FogvatartottiKartya()
        {
        }

        public override bool Equals(object obj)
        {
            if ((obj as FogvatartottiKartya).Id == Id)
                return true;
            return false;
        }
        #endregion eljárások
    }
}
