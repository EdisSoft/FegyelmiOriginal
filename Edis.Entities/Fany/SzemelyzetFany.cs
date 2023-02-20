using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("SZEMELYZET")]
    public class SzemelyzetFany : ExtendedBaseEntity
    {
        //[Column("ID")]
        //[Key]
        //[Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public new int Id { get; set; }
        
        #region mezők
        #endregion

        #region jellemzők
        #region oszlopok

        [Column("AZON_KOD")]
        public string Azonosito { get; set; }

        [Column("NEV")]
        public string Nev { get; set; }

        [Column("INTEZETI_DOLG_FL")]
        public bool IntezetiDolgozo { get; set; }

        [Column("BV_INTEZET_ID")]
        public int? IntezetId { get; set; }

        [Column("RENDFOKOZAT_KSZ_ID")]
        public int RendfokozatKszId { get; set; }

        [Column("BEOSZTAS_KSZ_ID")]
        public int BeosztasKszId { get; set; }

        [Column("BESOR_FOKOZAT_KSZ_ID")]
        public int? BesorolasKszId { get; set; }

        [Column("AD_SID")]
        public string AdSid { get; set; }

        #endregion


        //#region kapcsolódó objektumok
        //[KesleltetettBetoltesu]
        //public virtual Intezet Intezet { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "RendfokozatKszId")]
        [ForeignKey("RendfokozatKszId")]
        public virtual Kodszotar Rendfokozat { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "BeosztasKszId")]
        //public virtual Kodszotar Beosztas { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "BesorolasKszId")]
        //public virtual Kodszotar Besorolas { get; set; }

        //#endregion kapcsolódó objektumok

        //#endregion

        //#region eljárások


        //public string AdomanyozottCim { get { return (BesorolasKszId ?? 0) != 0 ? Besorolas.Nev : null; } }

        //public string NevRendfokozat { get { return SzemelyzetFormazas(this); } }

        //public string NevRendfokozatBeosztas { get { return string.Format("{0}, {1}", NevRendfokozat, Beosztas.Nev); } }

        //public string AzonositoNev { get { return SzemelyzetFormazasAzonositoNev(this); } }

        //public string AzonositoNevRendfokozat { get { return SzemelyzetFormazasAzonositoNevRendfokozat(this); } }

        private string RendfokozatNeve { get { return (RendfokozatKszId != 0 && Rendfokozat != null) ? Rendfokozat.Nev : null; } }

        //public static string SzemelyzetFormazas(Szemelyzet szemelyzet)
        //{
        //    if (szemelyzet == null)
        //        return null;

        //    return SzemelyzetFormazas(szemelyzet.Nev, szemelyzet.RendfokozatNeve);
        //}

        public static string SzemelyzetFormazasAzonositoNev(Szemelyzet szemelyzet)
        {
            if (szemelyzet == null)
                return null;

            return SzemelyzetFormazasAzonositoNev(szemelyzet.Azonosito, szemelyzet.Nev);
        }


        //public static string SzemelyzetFormazasAzonositoNevRendfokozat(Szemelyzet szemelyzet)
        //{
        //    if (szemelyzet == null)
        //        return null;

        //    return SzemelyzetFormazasAzonositoNevRendfokozat(szemelyzet.Azonosito, szemelyzet.Nev, szemelyzet.RendfokozatNeve);
        //}

        public static string SzemelyzetFormazas(string nev, string rendfokozatNev)
        {
            return string.Format("{0}, {1}", nev, rendfokozatNev);
        }

        public static string SzemelyzetFormazasAzonositoNev(string azonosito, string nev)
        {
            return string.Format("{0}, {1}", azonosito, nev);
        }

        public static string SzemelyzetFormazasAzonositoNevRendfokozat(string azonosito, string nev, string rendfokozatNev)
        {
            return string.Format("{0}, {1}, {2}", azonosito, nev, rendfokozatNev);
        }

        public override string ToString()
        {
            return Nev;
        }
        #endregion


        public virtual void OnCreate(int tranzakcioId)
        {
            this.FelvTranzId = tranzakcioId;
            this.ModTranzId = tranzakcioId;
            this.ErvenyessegKezdete = DateTime.Now;
        }

        public virtual void OnModify(int tranzakcioId)
        {
            this.ModTranzId = tranzakcioId;
            this.ErvenyessegKezdete = DateTime.Now;
        }
    }
}
