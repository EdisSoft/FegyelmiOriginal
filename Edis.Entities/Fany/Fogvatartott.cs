using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.Entities.Base;
using System.ComponentModel.DataAnnotations;


namespace Edis.Entities.Fany
{

    [Table("Fogvatartott")]
    public class Fogvatartott : ExtendedBaseEntity
    {
        [Column("AKT_FOGV_AZON_KOD")]
        public string AktualisAzonosito { get; set; }

        [Column("NYILV_FOGV_AZON_KOD")]
        public string NyilvantartasiAzonosito { get; set; }

        [Column("BEFOGAD_INDOK")]
        public string BefogadasIndoka { get; set; }

        [Column("ORIZETBE_DATUM")]
        public DateTime OrizetbevetelDatuma { get; set; }

        [Column("ELSO_BEFOG_DATUM")]
        public DateTime ElsoBefogadasDatuma { get; set; }

        [Column("BEFOGADAS_DATUM")]
        public DateTime BefogadasDatuma { get; set; }

        [Column("TAVOLLET_KEZD")]
        public DateTime? TavolletKezdete { get; set; }

        [Column("TAVOZAS_DATUM")]
        public DateTime? TavozasDatuma { get; set; }

        [Column("AKT_SZABAD_DATUM")]
        public DateTime? AktualisSzabadulasDatuma { get; set; }

        [Column("ZARKA_AGY")]
        public int? ZarkaAgy { get; set; }

        [Column("NYILVANTARTO_BVINT_ID")]
        public int NyilvantartoIntezetId { get; set; }

        [Column("ELSO_BEFOG_BVINT_ID")]
        public int ElsoBefogadoIntezetId { get; set; }

        [Column("AKTUALIS_BVINT_ID")]
        public int AktualisIntezetId { get; set; }

        [Column("BEFOGAD_SZERV_KSZ_ID")]
        public int BefogadoSzervezetKszId { get; set; }

        [Column("ORIZETBE_MOD_KSZ_ID")]
        public int OrizetbeVetelModjaKszId { get; set; }

        [Column("ELSO_BEFOG_MOD_KSZ_ID")]
        public int ElsoBefogadasModjaKszId { get; set; }

        [Column("BEFOGADAS_MOD_KSZ_ID")]
        public int BefogadasModjaKszId { get; set; }

        [Column("FOGV_JELLEG_KSZ_ID")]
        public int FogvatartasJellegeKszId { get; set; }

        [Column("VEGREHAJTASI_FOK_KSZ_ID")]
        public int VegrehajtasiFokKszId { get; set; }

        [Column("NYILV_STATUSZ_KSZ_ID")]
        public int NyilvantartasiStatuszKszId { get; set; }

        [Column("BUNCSELEKMENY_ID")]
        public int? BuncselekmenyId { get; set; }

        [Column("VISSZAESES_FOKA_KSZ_ID")]
        public int VisszaesesFokaKszId { get; set; }

        [Column("ENYH_VEGREHAJT_KSZ_ID")]
        public int? EnyhVegrehajtasKszId { get; set; }

        [Column("BIZT_CSOP_KSZ_ID")]
        public int BiztonsagiCsoportKszId { get; set; }

        [Column("NEVELESI_CSOPORT_ID")]
        public int? NevelesiCsoportId { get; set; }

        [Column("INTEZETI_OBJEKTUM_ID")]
        public int? IntezetiObjektumId { get; set; }

        [Column("KORLET_ID")]
        public int? KorletId { get; set; }

        [Column("ZARKA_ID")]
        public int? ZarkaId { get; set; }

        [Column("SPEC_BEHELYEZ_TIP_KSZ_ID")]
        public int? SpecialisBehelyezesTipusaKszId { get; set; }

        [Column("ITELET_ID")]
        public int? IteletId { get; set; }

        [Column("ELOZETES_LETART_ID")]
        public int? ElozetesLetartoztatasId { get; set; }

        [Column("ELZARASI_HATAROZAT_ID")]
        public int? ElzarasiHatarozatId { get; set; }

        [Column("ORIZET_ID")]
        public int? OrizetId { get; set; }

        [Column("ZARKABA_HELYEZES_ID")]
        public int? ZarkabaHelyezesId { get; set; }

        [Column("BEFOGADAS_ID")]
        public int BefogadasId { get; set; }

        [Column("ORIZETBEVETEL_ID")]
        public int? OrizetbeVetelId { get; set; }

        [Column("TAVOLLET_ID")]
        public int? TavolletId { get; set; }

        [Column("FOGV_FENYKEP_ID")]
        public int? FogvFenykepId { get; set; }

        [Column("SZABAD_SZOVEG_ID")]
        public int? SzabadSzovegId { get; set; }

        [Column("VEDETT_FL")]
        public bool? Vedett { get; set; }

        [Column("REZSIM_KSZ_ID")]
        public int RezsimKszId { get; set; }

        [Column("DOHANYZAS_FL")]
        public bool? IsDohanyzik { get; set; }

        [Column("BIZTKOCK_KSZ_ID")]
        public int BiztKockKszId { get; set; }

        //[Column("KERESO_AZON_KOD")]
        //public string KeresoAzonKod { get; set; }

        //[Column("KERESO_NEV")]
        //public string KeresoNev { get; set; }

        //[InverseProperty("Fogvatartott")]
        //public virtual FogvatartottSzemelyesAdatai FogvSzemAdatok { get; set; }

        [Column("F3FogvatartasId")]
        public int? F3FogvatartasId { get; set; }


        public virtual ICollection<FogvatartottSzemelyesAdatai> FogvSzemAdatok { get; set; }

        [ForeignKey("NyilvantartasiStatuszKszId")]
        public virtual Kodszotar NyilvantartasiStatusz { get; set; }

        [ForeignKey("IntezetiObjektumId")]
        public virtual IntezetiObjektum IntezetiObjektum { get; set; }

        [ForeignKey(nameof(AktualisIntezetId))]
        public virtual Intezet AktualisIntezet { get; set; }

        [ForeignKey(nameof(NyilvantartoIntezetId))]
        public virtual Intezet NyilvantartoIntezet { get; set; }

        [ForeignKey("KorletId")]
        public virtual Korlet Korlet { get; set; }

        [ForeignKey("ZarkaId")]
        public virtual Zarka Zarka { get; set; }

        [ForeignKey("VegrehajtasiFokKszId")]
        public virtual Kodszotar VegrehajtasiFokozat { get; set; }

        public virtual ICollection<FogvatartottFenykep> FogvatartottFenykep { get; set; }
        
        [ForeignKey("BiztKockKszId")]
        public virtual Kodszotar BiztonsagiKockazat { get; set; }

        [ForeignKey("RezsimKszId")]
        public virtual Kodszotar Rezsim { get; set; }


        
        //public virtual ICollection<FogvatartottSzemelyesAdatai> FogvSzemAdatok { get; set; }

        public Fogvatartott()
        {
            FogvatartottFenykep = new HashSet<FogvatartottFenykep>();
            FogvSzemAdatok = new HashSet<FogvatartottSzemelyesAdatai>();
            //FogvSzemAdatok = new FogvatartottSzemelyesAdatai();
        }

        /* [NotMapped]
         public virtual FogvSzemAdat SzemelyesAdatok
         {
             get { return this.FogvSzemAdatok.FirstOrDefault(); }
         }

         [ForeignKey("ZarkaId")]
         public virtual Zarka Zarka { get; set; }

         [ForeignKey("KorletId")]
         public virtual Korlet Korlet { get; set; }

         [ForeignKey("IntezetiObjektumId")]
         public virtual IntezetiObjektum IntezetiObjektum { get; set; }


         [ForeignKey("NevelesiCsoportId")]
         public virtual NevelesiCsoport NevelesiCsoport { get; set; }

         public virtual ICollection<Munkaltatas> Munkaltatasok { get; set; }

         [ForeignKey("AktualisIntezetId")]
         public virtual Intezet AktualisIntezet { get; set; }

         [ForeignKey("NyilvantartoIntezetId")]
         public virtual Intezet NyilvantartoIntezet { get; set; }

         [ForeignKey("RezsimKszId")]
         public virtual Kodszotar Rezsim { get; set; }

        [ForeignKey("BiztonsagiCsoportKszId")]
         public virtual Kodszotar BiztonsagiCsoport { get; set; }*/

        public override bool Equals(object obj)
        {
            if ((obj as Fogvatartott).Id == Id)
                return true;
            return false;
        }

    }
}
