using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.Entities.Common;

namespace Edis.Entities.Fany
{
    [Table("FOGV_SZEM_ADAT")]
    public class FogvatartottSzemelyesAdatai : ExtendedBaseEntity
    {
        #region mezők

        #endregion mezők

        #region jellemzők
        [Column("CSALADI_NEV")]
        public string CsaladiNev_NE_HASZNALD { get; set; }
        [NotMapped]
        public string CsaladiNev { get { return Fogvatartott.Vedett == true ? "-" : CsaladiNev_NE_HASZNALD; } }

        [Column("UTONEV")]
        public string Utonev_NE_HASZNALD { get; set; }
        [NotMapped]
        public string Utonev { get { return Fogvatartott.Vedett == true ? "-" : Utonev_NE_HASZNALD; } }

        [Column("SZUL_CSALADI_NEV")]
        public string SzuletesiCsaladiNev_NE_HASZNALD { get; set; }
        [NotMapped]
        public string SzuletesiCsaladiNev { get { return Fogvatartott.Vedett == true ? "-" : SzuletesiCsaladiNev_NE_HASZNALD; } }

        [Column("SZUL_UTONEV")]
        public string SzuletesiUtonev_NE_HASZNALD { get; set; }
        [NotMapped]
        public string SzuletesiUtonev { get { return Fogvatartott.Vedett == true ? "-" : SzuletesiUtonev_NE_HASZNALD; } }

        [Column("SZUL_HELY_NEV")]
        public string SzuletesiHelyNeve_NE_HASZNALD { get; set; }
        [NotMapped]
        public string SzuletesiHelyNeve { get { return Fogvatartott.Vedett == true ? "-" : SzuletesiHelyNeve_NE_HASZNALD; } }

        [Column("SZULETESI_DATUM")]
        public DateTime SzuletesiDatum_NE_HASZNALD { get; set; }
        [NotMapped]
        public DateTime SzuletesiDatum { get { return Fogvatartott.Vedett == true ? DateTime.MinValue : SzuletesiDatum_NE_HASZNALD; } }

        [Column("ANYJA_NEVE")]
        public string AnyjaNeve_NE_HASZNALD { get; set; }
        [NotMapped]
        public string AnyjaNeve { get { return Fogvatartott.Vedett == true ? "-" : AnyjaNeve_NE_HASZNALD; } }

        [Column("SZEM_AZON_JEL")]
        public string SzemelyiAzonositoJel_NE_HASZNALD { get; set; }
        [NotMapped]
        public string SzemelyiAzonositoJel { get { return Fogvatartott.Vedett == true ? "-" : SzemelyiAzonositoJel_NE_HASZNALD; } }

        [Column("SZEM_IG_SZAM")]
        public string SzemelyigazolvanySzam_NE_HASZNALD { get; set; }
        [NotMapped]
        public string SzemelyigazolvanySzam { get { return Fogvatartott.Vedett == true ? "-" : SzemelyigazolvanySzam_NE_HASZNALD; } }

        [Column("ADOAZON_JEL")]
        public string AdoazonositoJel_NE_HASZNALD { get; set; }
        [NotMapped]
        public string AdoazonositoJel { get { return Fogvatartott.Vedett == true ? "-" : AdoazonositoJel_NE_HASZNALD; } }

        [Column("TAJ_SZAM")]
        public string TajSzam_NE_HASZNALD { get; set; }
        [NotMapped]
        public string TajSzam { get { return Fogvatartott.Vedett == true ? "-" : TajSzam_NE_HASZNALD; } }

        [Column("UTLEVEL_SZAM")]
        public string UtlevelSzam_NE_HASZNALD { get; set; }
        [NotMapped]
        public string UtlevelSzam { get { return Fogvatartott.Vedett == true ? "-" : UtlevelSzam_NE_HASZNALD; } }

        [Column("ALL_CIM_IRSZAM")]
        public int? AllandoLakcimIranyitoszam { get; set; }

        [Column("ALL_CIM_HELYSEG_NEV")]
        public string AllandoLakcimHelysegnev { get; set; }

        [Column("ALL_CIM_UTCA")]
        public string AllandoLakcimUtca_NE_HASZNALD { get; set; }
        [NotMapped]
        public string AllandoLakcimUtca { get { return Fogvatartott.Vedett == true ? "-" : AllandoLakcimUtca_NE_HASZNALD; } }

        [Column("ALL_CIM_HAZSZAM")]
        public string AllandoLakcimHazszam_NE_HASZNALD { get; set; }
        [NotMapped]
        public string AllandoLakcimHazszam { get { return Fogvatartott.Vedett == true ? "-" : AllandoLakcimHazszam_NE_HASZNALD; } }

        [Column("BEJ_CIM_IRSZAM")]
        public int? BejelentettLakcimIranyitoszam { get; set; }

        [Column("BEJ_CIM_HELYSEG_NEV")]
        public string BejelentettLakcimHelysegnev { get; set; }

        [Column("BEJ_CIM_UTCA")]
        public string BejelentettLakcimUtca_NE_HASZNALD { get; set; }
        [NotMapped]
        public string BejelentettLakcimUtca { get { return Fogvatartott.Vedett == true ? "-" : BejelentettLakcimUtca_NE_HASZNALD; } }

        [Column("BEJ_CIM_HAZSZAM")]
        public string BejelentettLakcimHazszam_NE_HASZNALD { get; set; }
        [NotMapped]
        public string BejelentettLakcimHazszam { get { return Fogvatartott.Vedett == true ? "-" : BejelentettLakcimHazszam_NE_HASZNALD; } }

        [Column("GYERMEKEK_SZAMA")]
        public int? GyermekekSzama { get; set; }

        [Column("FOGVATARTOTT_ID")]
        public int FogvatartottId { get; set; }

        [Column("FOGV_SZEMELY_ID")]
        public int FogvatartottSzemelyId { get; set; }

        [Column("NEME_KSZ_ID")]
        public int NemId { get; set; }

        [Column("SZULETESI_ORSZAG_KSZ_ID")]
        public int? SzuletesiOrszagId { get; set; }

        [Column("ALLAMPOLG_KSZ_ID")]
        public int? AllampolgarsagId { get; set; }

        [Column("SZULETESI_HELYSEG_ID")]
        public int? SzuletesiHelysegId { get; set; }

        [Column("ALL_CIM_ORSZAG_KSZ_ID")]
        public int? AllandoLakcimOrszagId { get; set; }

        [Column("ALL_CIM_HELYSEG_ID")]
        public int? AllandoLakcimHelysegId { get; set; }

        [Column("BEJ_CIM_ORSZAG_KSZ_ID")]
        public int? BejelentettLakcimOrszagId { get; set; }

        [Column("BEJ_CIM_HELYSEG_ID")]
        public int? BejelentettLakcimHelysegId { get; set; }

        [Column("ISK_VEGZETTSEG_KSZ_ID")]
        public int? IskolaiVegzettsegId { get; set; }

        [Column("SZAKKEPZ_KSZ_ID")]
        public int? SzakkepzettsegId { get; set; }

        [Column("CSALADI_ALLAPOT_KSZ_ID")]
        public int? CsaladiAllapotId { get; set; }

        [Column("TAJ_TIP_KSZ_ID")]
        public int? TajSzamTipusId { get; set; }

        [Column("IRATEGY_FOGVAT_ID")]
        public int? IrategyesitesFogvatartottId { get; set; }

        [ForeignKey("FogvatartottId")]
        public virtual Fogvatartott Fogvatartott { get; set; }

        [ForeignKey("FogvatartottId")]
        public virtual FogvatartottNezet FogvatartottNezet { get; set; }

        [ForeignKey("FogvatartottSzemelyId")]
        public virtual FogvatartottSzemely FogvatartottSzemely { get; set; }

        ////[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "NemId")]
        [ForeignKey("NemId")]
        public virtual Kodszotar Nem { get; set; }

        ////[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "SzuletesiOrszagId")]
        //public virtual Kodszotar SzuletesiOrszag { get; set; }

        ////[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "AllampolgarsagId")]
        [ForeignKey("AllampolgarsagId")]
        public virtual Kodszotar Allampolgarsag { get; set; }

        ////[KesleltetettBetoltesu]
        //public virtual Helyseg SzuletesiHelyseg { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "AllandoLakcimOrszagId")]
        //public virtual Kodszotar AllandoLakcimOrszag { get; set; }

        //[KesleltetettBetoltesu]
        //public virtual Helyseg AllandoLakcimHelyseg { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "BejelentettLakcimOrszagId")]
        //public virtual Kodszotar BejelentettLakcimOrszag { get; set; }

        //[KesleltetettBetoltesu]
        //public virtual Helyseg BejelentettLakcimHelyseg { get; set; }
        
        [ForeignKey("IskolaiVegzettsegId")]
        public virtual Kodszotar IskolaiVegzettseg { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "SzakkepzettsegId")]
        //public virtual Kodszotar Szakkepzettseg { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "CsaladiAllapotId")]
        //public virtual Kodszotar CsaladiAllapot { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "TajSzamTipusId")]
        //public virtual Kodszotar TajSzamTipus { get; set; }

        //[KesleltetettBetoltesu]
        //public virtual Fogvatartott IrategyesitesFogvatartott { get; set; }

        #endregion jellemzők

        #region számított jellemzők

        //public virtual string TeljesNevEsSzuletesiNev
        //{
        //    get
        //    {
        //        return TeljesNev == TeljesSzuletesiNev ? TeljesNev : TeljesNev + " (szül: " + TeljesSzuletesiNev + ")";
        //    }
        //}

        //public virtual string TeljesNev { get { return SzemelynevFormazasa(CsaladiNev, Utonev); } }

        //public virtual string TeljesSzuletesiNev { get { return SzemelynevFormazasa(SzuletesiCsaladiNev, SzuletesiUtonev); } }

        //public string AllandoLakcim
        //{
        //    get { return LakcimFormazasa(AllandoLakcimIranyitoszam, AllandoLakcimHelyseg, AllandoLakcimUtca, AllandoLakcimHazszam, AllandoLakcimHelysegnev); }
        //}

        #endregion számított jellemzők

        #region eljárások

        //public static string SzemelynevFormazasa(string csaladiNev, string utonev)
        //{
        //    return string.Format("{0} {1}", csaladiNev, utonev);
        //}

        //public static string LakcimFormazasa(int? iranyitoszam, Helyseg telepules, string utca, string hazszam, string helysegnev)
        //{
        //    return helysegnev ?? string.Format("{0} {1}, {2} {3}", iranyitoszam, telepules != null ? telepules.Nev : "", utca, hazszam);
        //}

        #endregion eljárások

        #region konstruktor

        //public FogvatartottSzemelyesAdatai() : base() { }

    }
    #endregion konstruktor
}
