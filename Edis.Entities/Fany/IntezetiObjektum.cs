using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    [Table("INTEZETI_OBJEKTUM")]
    public class IntezetiObjektum : ExtendedBaseEntity, IAzonositovalRendelkezo
    {
        #region jellemzők

        [Column("AZON_KOD")]
        public  string Azonosito { get; set; }

        [Column("NEV")]
        public  string Nev { get; set; }

        [Column("ROVID_NEV")]
        public  string RovidNev { get; set; }

        [Column("BEFOGADOKEP")]
        public  int Befogadokepesseg { get; set; }

        [Column("CIM_IRSZAM")]
        public  int? CimIranyitoszam { get; set; }

        [Column("CIM_UTCA")]
        public  string CimUtca { get; set; }

        [Column("CIM_HAZSZAM")]
        public  string CimHazszam { get; set; }

        [Column("LEVEL_IRSZAM")]
        public  int? LevelezesiCimIranyitoszam { get; set; }

        [Column("LEVEL_UTCA")]
        public  string LevelezesiCimUtca { get; set; }

        [Column("LEVEL_HAZSZAM")]
        public  string LevelezesiCimHazszam { get; set; }

       
        [Column("BV_INTEZET_ID")]
        public  int IntezetId { get; set; }

        
        [Column("CIM_HELYSEG_ID")]
        public  int CimHelysegId { get; set; }

       
        [Column("LEVEL_HELYSEG_ID")]
        public  int LevelezesiCimHelysegId { get; set; }

       
        public virtual Intezet Intezet { get; set; }

       
        public virtual Helyseg CimHelyseg { get; set; }

        
        public virtual Helyseg LevelezesiCimHelyseg { get; set; }

        public virtual IList<Korlet> Korletek { get; set; }

        public string IntezetRovidNev
        {
            get { return Intezet.RovidNev; }
        }

        public string AzonositoNev
        {
            get { return string.Format("{0} - {1}", Azonosito, Nev); }
        }

        public string IntezetRovidNevNev
        {
            get { return string.Format("{0} - {1}", IntezetRovidNev, Nev); }
        }

      

        #endregion jellemzők

        #region konstruktor

        public IntezetiObjektum() : base() { }

        #endregion
    }
}
