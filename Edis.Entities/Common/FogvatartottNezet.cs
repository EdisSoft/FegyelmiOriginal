using Edis.Entities.Base;

using Edis.Entities.Fany;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Edis.Entities.Common
{
    [Table("dbo.vFogvatartott")]
    public class FogvatartottNezet : ExtendedBaseEntity
    {
        #region oszlopok
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
        public bool Vedett { get; set; }

        [Column("REZSIM_KSZ_ID")]
        public int RezsimKszId { get; set; }

        [Column("KERESO_AZON_KOD")]
        public string KeresoAzonKod { get; set; }

        [Column("KERESO_NEV")]
        public string KeresoNev { get; set; }

        [Column("NevelesiCsoportNev")]
        public string NevelesiCsoportNev { get; set; }

        [Column("CSALADI_NEV")]
        public string CsaladiNev { get; set; }

        [Column("UTONEV")]
        public string Utonev { get; set; }

        [Column("SZUL_CSALADI_NEV")]
        public string SzuletesiCsaladiNev { get; set; }

        [Column("SZUL_UTONEV")]
        public string SzuletesiUtonev { get; set; }

        [Column("SZUL_HELY_NEV")]
        public string SzuletesiHelyNeve { get; set; }

        [Column("SZULETESI_DATUM")]
        public DateTime? SzuletesiDatum { get; set; }

        [Column("ANYJA_NEVE")]
        public string AnyjaNeve { get; set; }

        [Column("TAJ_SZAM")]
        public string TajSzam { get; set; }

        [Column("ALL_CIM_IRSZAM")]
        public int? AllandoLakcimIranyitoszam { get; set; }

        [Column("ALL_CIM_HELYSEG_NEV")]
        public string AllandoLakcimHelysegnev { get; set; }

        [Column("ALL_CIM_UTCA")]
        public string AllandoLakcimUtca { get; set; }

        [Column("ALL_CIM_HAZSZAM")]
        public string AllandoLakcimHazszam { get; set; }

        [Column("SZAKKEPZ_KSZ_ID")]
        public int? SzakkepzettsegKszId { get; set; }

        [Column("BIZTKOCK_KSZ_ID")]
        public int? BiztonsagiKockazatKszId { get; set; }

        [Column("NEVELESI_CSOPORT")]
        public string NevelesiCsoport { get; set; }

        [Column("DOHANYZAS_FL")]
        public bool? IsDohanyzik { get; set; }
        [Column("ALLAMPOLGARSAG")]
        public string Allampolgarsag { get; set; }
        [Column("FOGV_SZEMELY_ID")]
        public int FogvSzemelyId { get; set; }
        #endregion oszlopok

        #region kapcsolódó objektumok
        [ForeignKey("NyilvantartasiStatuszKszId")]
        public virtual Kodszotar NyilvantartasiStatusz { get; set; }

        [ForeignKey("NyilvantartoIntezetId")]
        public virtual Intezet NyilvantartoIntezet { get; set; }

        [ForeignKey("AktualisIntezetId")]
        public virtual Intezet AktualisIntezet { get; set; }

        [ForeignKey("IntezetiObjektumId")]
        public virtual IntezetiObjektum IntezetiObjektum { get; set; }

        [ForeignKey("KorletId")]
        public virtual Korlet Korlet { get; set; }

        [ForeignKey("ZarkaId")]
        public virtual Zarka Zarka { get; set; }

        [ForeignKey("VegrehajtasiFokKszId")]
        public virtual Kodszotar VegrehajtasiFok { get; set; }

        [ForeignKey("SzakkepzettsegKszId")]
        public virtual Kodszotar Szakkepzettseg { get; set; }
        [ForeignKey("OrizetbeVetelModjaKszId")]
        public virtual Kodszotar OrizetbeVetelModja { get; set; }

        [ForeignKey("RezsimKszId")]
        public virtual Kodszotar Rezsim { get; set; }

        [ForeignKey("BiztonsagiKockazatKszId")]
        public virtual Kodszotar BiztonsagiKockazat { get; set; }

        [ForeignKey("VisszaesesFokaKszId")]
        public virtual Kodszotar VisszaesesFoka { get; set; }

        [ForeignKey("FogvatartasJellegeKszId")]
        public virtual Kodszotar FogvatartasJellege { get; set; }


        [ForeignKey("ElozetesLetartoztatasId")]
        public virtual ElozetesLetartoztatas ElozetesLetartoztatas { get; set; }

        [ForeignKey("EnyhVegrehajtasKszId")]
        public virtual Kodszotar EnyhVegrehajtas { get; set; }


        [NotMapped]
        public Kodszotar Neme => this.FogvSzemAdatok?.LastOrDefault()?.Nem;

        [NotMapped]
        public string TeljesNev => $"{this.SzuletesiCsaladiNev} {this.SzuletesiUtonev}";

        [NotMapped]
        public string Elhelyezes => $"{IntezetiObjektum?.Nev} /{Korlet?.Nev}/ {Zarka?.Azonosito}";

        [NotMapped]
        public FogvatartottFenykep FogvatartottFenykep => this.FogvatartottFenykepek != null && this.FogvatartottFenykepek.Count > 0 && !this.Vedett ? this.FogvatartottFenykepek.Last() : null;

        //[ForeignKey("NevelesiCsoportId")]
        //public virtual Kodszotar NevelesiCsoport { get; set; }

        public virtual ICollection<FogvatartottSzemelyesAdatai> FogvSzemAdatok { get; set; }
        public virtual ICollection<FogvatartottFenykep> FogvatartottFenykepek { get; set; }
       
        //public virtual ICollection<Foglalkoztatas.Foglalkoztatas> Foglalkoztatasok { get; set; }
      
        public virtual ICollection<FogvatartottElelmezesiNorma> FogvatartottElelmezesiNormak { get; set; }

        #endregion kapcsolódó objektumok

        #region eljárások
        public FogvatartottNezet()
        {
            FogvatartottFenykepek = new HashSet<FogvatartottFenykep>();
            FogvSzemAdatok = new HashSet<FogvatartottSzemelyesAdatai>();
            //Foglalkoztatasok = new HashSet<Foglalkoztatas.Foglalkoztatas>();
          
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (obj as FogvatartottNezet == null) return false;

            if ((obj as FogvatartottNezet).Id == Id)
                return true;
            return false;
        }
        #endregion eljárások
    }
}
