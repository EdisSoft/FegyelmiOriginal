using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("BV_INTEZET")]
    public class Intezet : ExtendedBaseEntity, IAzonositovalRendelkezo
    {
        #region mezők

        public const string KnyAzonosito = "200";

        #endregion mezők

        #region jellemzők
        [Column("AZON_KOD")]
        public string Azonosito { get; set; }

        [Column("AZON_KOD_2")]
        public string Azonosito2 { get; set; }

        [Column("NEV")]
        public string Nev { get; set; }

        [Column("KOZEPES_NEV")]
        public string KozepesNev { get; set; }

        [Column("ROVID_NEV")]
        public string RovidNev { get; set; }

        [Column("TELEFON_FAX")]
        public string TelefonFax { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("CIM_IRSZAM")]
        public int? CimIranyitoszam { get; set; }

        [Column("CIM_UTCA")]
        public string CimUtca { get; set; }

        [Column("CIM_HAZSZAM")]
        public string CimHazszam { get; set; }

        [Column("LEVEL_IRSZAM")]
        public int? LevelezesiCimIranyitoszam { get; set; }

        [Column("LEVEL_UTCA")]
        public string LevelezesiCimUtca { get; set; }

        [Column("LEVEL_HAZSZAM")]
        public string LevelezesiCimHazszam { get; set; }

        [Column("MEGSZUNES_DATUM")]
        public DateTime? MegszunesDatum { get; set; }

        [Column("BEFOGADOKEP")]
        public int Befogadokepesseg { get; set; }

        [Column("INT_JELLEG_KSZ_ID")]
        public int IntezetJellegKszId { get; set; }


        [Column("CIM_HELYSEG_ID")]
        public int? CimHelysegId { get; set; }


        [Column("LEVEL_CIM_HELYSEG_ID")]
        public int? LevelezesiCimHelysegId { get; set; }

        [Column("Latitude_Szelesseg")]
        public double? SzelessegiKoordinata { get; set; }
        [Column("Longitude_Hosszusag")]
        public double? HosszusagiKoordinata { get; set; }

        [ForeignKey("CimHelysegId")]
        public virtual Helyseg CimHelyseg { get; set; }

        //[KesleltetettBetoltesu]
        //public virtual Helyseg LevelezesiCimHelyseg { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "IntezetJellegKszId")]
        //public virtual Kodszotar IntezetJelleg { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "Id")]
        //public virtual IList<IntezetiObjektum> IntezetiObjektumok { get; set; }

        public virtual IList<IntezetiObjektum> IntezetiObjektumok { get; set; }

        public string AzonositoNev
        {
            get { return string.Format("{0} - {1}", Azonosito, Nev); }
        }

        [NotMapped]
        public string CimHelysegNev { get { return CimHelyseg?.Nev; } }

        #endregion

        #region konstruktor
        public string KodNev { get { return IntezetFormazas(this); } }

        public static string IntezetFormazas(Intezet Intezet)
        {
            return Intezet != null ? IntezetFormazas(Intezet.Azonosito, Intezet.Nev) : null;
        }

        public static string IntezetFormazas(string azonosito, string nev)
        {
            return string.Format("{0}, {1}", azonosito, nev);
        }
        #endregion

    }
}
