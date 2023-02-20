using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.JFK
{
    [Table("szallitas")]
    public class Szallitas : ExtendedBaseEntity
    {
        [Column("SZALLIT_ESED_DATUM")]
        public DateTime SZALLIT_ESED_DATUM { get; set; }
        [Column("SZALLIT_TENY_DATUM")]
        public DateTime? SZALLIT_TENY_DATUM { get; set; }
        [Column("SZALLIT_INDOK_1")]
        public string SZALLIT_INDOK_1 { get; set; }
        [Column("SZALLIT_INDOK_2")]
        public string SZALLIT_INDOK_2 { get; set; }
        [Column("ERTEK_CSOMAG_DB")]
        public int? ERTEK_CSOMAG_DB { get; set; }
        [Column("LETET_CSOMAG_DB")]
        public int? LETET_CSOMAG_DB { get; set; }
        [Column("FOGVATARTOTT_ID")]
        public int FOGVATARTOTT_ID { get; set; }
        [Column("KULDO_BVINT_ID")]
        public int? KULDO_BVINT_ID { get; set; }
        [Column("TERV_BVINT_ID")]
        public int? TERV_BVINT_ID { get; set; }
        [Column("BV_INTEZET_ID")]
        public int? BV_INTEZET_ID { get; set; }
        [Column("STATUSZ_KSZ_ID")]
        public int STATUSZ_KSZ_ID { get; set; }
        [Column("SZALLIT_OKA_KSZ_ID")]
        public int SZALLIT_OKA_KSZ_ID { get; set; }
        [Column("SZALLIT_TIP_KSZ_ID")]
        public int SZALLIT_TIP_KSZ_ID { get; set; }
        [Column("RENDELK_TIP_KSZ_ID")]
        public int RENDELK_TIP_KSZ_ID { get; set; }
        [Column("SZEMELYZET_ID")]
        public int? SZEMELYZET_ID { get; set; }
        [Column("IDEZES_ID")]
        public int? IDEZES_ID { get; set; }
        [Column("POTNYOMOZAS_ID")]
        public int? POTNYOMOZAS_ID { get; set; }
        [Column("KERELEM_ID")]
        public int? KERELEM_ID { get; set; }
        [Column("EU_ESEMENY")]
        public string EU_ESEMENY { get; set; }
        [Column("JELENTVE_FL")]
        public bool JELENTVE_FL { get; set; }
        [Column("BILINCS_TIP_KSZ_ID")]
        public int? BILINCS_TIP_KSZ_ID { get; set; }
    }
}
