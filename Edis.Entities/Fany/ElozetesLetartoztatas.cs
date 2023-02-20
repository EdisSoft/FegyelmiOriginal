using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.Entities.Base;

namespace Edis.Entities.Fany
{
    [Table("elozetes_letart")]
    public class ElozetesLetartoztatas : ExtendedBaseEntity
    {

        [Column("RENDELKEZES_SZAMA")]
        public string RendelkezesSzama { get; set; }

        [Column("UGYESZI_SZAM")]
        public string UgyesziSzam { get; set; }

        [Column("MEGSZ_RENDELK_SZAMA")]
        public string MegszRendelkSzama { get; set; }

        [Column("FELUGYELET_PARSZE_ID")]
        public int? FelugyeletParszeId { get; set; }

        [Column("RENDELK_DATUM")]
        public DateTime RendelkDatum { get; set; }

        [Column("RENDELK_LEJARAT")]
        public DateTime? RendelkLejarat { get; set; }

        [Column("RENDELK_MAX_LEJARAT")]
        public DateTime? RendelkMaxLejarat { get; set; }

        [Column("ELOZET_KEZDET")]
        public DateTime? ElozetKezdet { get; set; }

        [Column("MEGSZ_RENDELK_DATUM")]
        public DateTime? MegszRendelkDatum { get; set; }

        [Column("MEGSZUNES_DATUM")]
        public DateTime? MegszunesDatum { get; set; }

        [Column("ELOZET_MAX_EV")]
        public int? ElozetMaxEv { get; set; }

        [Column("TOLTOTT_FL")]
        public bool ToltottFl { get; set; }

        [Column("FOGVATARTOTT_ID")]
        public int FogvatartottId { get; set; }

        [Column("ELRENDELO_PARSZE_ID")]
        public int ElrendeloParszeId { get; set; }

        [Column("KAPCS_ELJ_STAT_KSZ_ID")]
        public int? KapcsEljStatKszId { get; set; }

        [Column("VEGREHAJTASI_FOK_KSZ_ID")]
        public int VegrehajtasiFokKszId { get; set; }

        [Column("MEGSZUNTETO_PARSZE_ID")]
        public int? MegszuntetoParszeId { get; set; }

        [Column("MEGSZUNES_OKA_KSZ_ID")]
        public int? MegszunesOkaKszId { get; set; }

        [ForeignKey("VegrehajtasiFokKszId")]
        public virtual Kodszotar VegrehajtasiFoka { get; set; }


    }
}
