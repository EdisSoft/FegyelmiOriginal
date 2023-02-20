using Edis.Entities.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edis.Entities.Fany
{
    [Table("ZARKA")]
    public class Zarka : ExtendedBaseEntity
    {
        #region mezők

        #endregion

        #region jellemzők
        [Column("AZON_KOD")]
        public string Azonosito { get; set; }

        [Column("LEGTER_M3")]
        public float LegterM3 { get; set; }

        [Column("AGY_DB")]
        public int AgyDb { get; set; }

        [Column("BEFOGADOKEP")]
        public int BefogadoKepesseg { get; set; }

        [Column("ALAPTERULET_NM")]
        public float AlapteruletM2 { get; set; }

        [Column("SZABAD_MOZGASTER_NM")]
        public float? SzabadMozgasterM2 { get; set; }

        [Column("ELETTER_NM")]
        public float? EletterM2 { get; set; }

        [Column("DOHANYZO_FL")]
        public  bool Dohanyzo { get; set; }

        [Column("BV_INTEZET_ID")]
        public int IntezetId { get; set; }

       
        [Column("INTEZETI_OBJEKTUM_ID")]
        public  int IntezetiObjektumId { get; set; }

        [Column("KORLET_ID")]
        public  int KorletId { get; set; }

       
        [Column("NEVELESI_CSOPORT_ID")]
        public  int? NevelesiCsoportId { get; set; }


        //[KodszotarCsoport(Azonosito = "195")]
        //[KulsoKulcs(KesleltetettBetoltesuJellemzoNeve = "ZarkaJelleg")]
        [Column("ZARKA_JELLEG_KSZ_ID")]
        public  int ZarkaJellegKszId { get; set; }


        //[KodszotarCsoport(Azonosito = "117")]
        //[KulsoKulcs(KesleltetettBetoltesuJellemzoNeve = "ZarkaTipus")]
        [Column("ZARKA_TIP_KSZ_ID")]
        public  int ZarkaTipusKszId { get; set; }


        [Column("NEM_KSZ_ID")]
        public int? NemeKszId { get; set; }

        [Column("MELLEKH_LEVALASZT_DATUM")]
        public DateTime? MellekhelysegLevalasztasDatuma { get; set; }
        //public int SzabadHelyekSzama
        //{
        //    get
        //    {
        //        int retval = AgyDb;
        //        if (Fogvatartottak.Any())
        //            retval = AgyDb - Fogvatartottak.Count;
        //        return retval;
        //    }
        //}

        //public int Letszam
        //{
        //    get { return Fogvatartottak.Count; }
        //}

        [ForeignKey("IntezetId")]
        public virtual Intezet Intezet { get; set; }

        [ForeignKey("IntezetiObjektumId")]
        public virtual IntezetiObjektum IntezetiObjektum { get; set; }

        [ForeignKey("KorletId")]
        public virtual Korlet Korlet { get; set; }

        [ForeignKey("NevelesiCsoportId")]
        public virtual NevelesiCsoport NevelesiCsoport { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "ZarkaJellegKszId")]
        [ForeignKey("ZarkaJellegKszId")]
        public virtual Kodszotar ZarkaJelleg { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "ZarkaTipusKszId")]
        [ForeignKey("ZarkaTipusKszId")]
        public virtual Kodszotar ZarkaTipus { get; set; }

        [ForeignKey(nameof(NemeKszId))]
        public virtual Kodszotar Neme { get; set; }


        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "Id")]
        //[ForeignKey("Id")]
        //public virtual IList<Fogvatartott> Fogvatartottak { get; set; }

        //public string AzonositoTipusDohanyzo
        //{
        //    get { return string.Format("{0} - {1}, {2}", Azonosito, ZarkaTipus.Nev, (Dohanyzo == 0) ? "Nem dohányzó" : "Dohányzó"); }
        //}

        //public string AzonositoTipusNevelesiCsoportDohanyzo
        //{
        //    get
        //    {
        //        return string.Format("{0} - {1}, ncs: {2}, {3}",
        //                               Azonosito,
        //                               ZarkaTipus.Nev,
        //                               (NevelesiCsoportId.HasValue ? NevelesiCsoport.Azonosito : " Nincs megadva"),
        //                               Dohanyzo == 0 ? "Nem dohányzó" : "Dohányzó");
        //    }
        //}

        //public string AzonositoTipusNevelesiCsoportDohanyzoAgyakSzama
        //{
        //    get
        //    {
        //        return string.Format("{0} - {1}, ncs: {2}, {3}, ágyak száma: {4}",
        //                             Azonosito,
        //                             ZarkaTipus.Nev,
        //                             NevelesiCsoportId.HasValue ? NevelesiCsoport.Azonosito : "Nincs megadva",
        //                             Dohanyzo == 0 ? "Nem dohányzó" : "Dohányzó",
        //                             AgyDb);
        //    }
        //}

        public virtual ICollection<ZarkaVegrFok> VegrehajtasiFokok { get; set; }

        public Zarka()
        {
            VegrehajtasiFokok = new HashSet<ZarkaVegrFok>();
        }

        #endregion jellemzők
    }
}
