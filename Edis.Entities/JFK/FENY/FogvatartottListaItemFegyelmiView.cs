using Edis.Entities.Fany;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edis.Entities.JFK.FENY
{
    [Table("Fegyelmi.vFogvatartottLista")]
    public class FogvatartottListaItemFegyelmiView
    {
      public int Id { get; set; }



        [Column("KERESO_AZON_KOD")]
        public string KeresoAzonKod { get; set; }

        [Column("KERESO_NEV")]
        public string KeresoNev { get; set; }

        [Column("NYILV_FOGV_AZON_KOD")]
        public string NyilvantartasiAzonosito { get; set; }

        [Column("SZULETESI_DATUM")]
        public DateTime SzuletesiDatum { get; set; }

        [Column("SZUL_CSALADI_NEV")]
        public string SzuletesiCsaladiNev { get; set; }

        [Column("SZUL_UTONEV")]
        public string SzuletesiUtonev { get; set; }

        [Column("VISELT_CSALADI_NEV")]
        public string ViseltCsaladiNev { get; set; }

        [Column("VISELTL_UTONEV")]
        public string ViseltUtonev { get; set; }

        [Column("ANYJA_NEVE")]
        public string AnyjaNeve { get; set; }


        [Column("NYILVANTARTASI_STATUSZ")]
        public string NyilvantartasiStatusz { get; set; }


        [Column("IntezetId")]
        public int IntezetId { get; set; }

        [Column("IntezetAzon2")]
        public string IntezetAzon { get; set; }

        [Column("ObjektumId")]
        public int? ObjektumId { get; set; }

        [Column("KorletId")]
        public int? KorletId { get; set; }

        [Column("ZarkaId")]
        public int? ZarkaId { get; set; }

        [Column("ObjektumNev")]
        public string ObjektumNev { get; set; }

        [Column("KorletNev")]
        public string KorletNev { get; set; }

        [Column("ZarkaAzon")]
        public string ZarkaAzon { get; set; }
        //[Column("GyogyitoCsoportok")]
        //public string GyogyitoCsoportok { get; set; }

        [Column("AKT_SZABAD_DATUM")]
        public DateTime? AktSzabadDatum { get; set; }

        

    }
}
