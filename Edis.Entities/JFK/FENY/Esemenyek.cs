//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Edis.Entities.JFK.FENY
{
    using System;
    using Edis.Entities.Base;
    using System.ComponentModel.DataAnnotations.Schema;
    using Edis.Entities.Common;
    using Edis.Entities.Fany;

    [Table("Fegyelmi.Esemenyek")]
    public class Esemenyek : KeziJavitoEntity
    {
        

        [Column("EsemenyDatuma")]
        public DateTime EsemenyDatuma { get; set; }

        [Column("JellegCimkeId")]
		public int JellegCimkeId { get; set; }

		[Column("NapszakCimkeId")]
		public int NapszakCimkeId { get; set; }

        [Column("HelyCimkeId")]
        public int HelyCimkeId { get; set; }

        [Column("Leiras")]
		public string Leiras { get; set; }

        [Column("Bizonyitek")]
        public string Bizonyitek { get; set; }

        [Column("EszleloId")]
        public int EszleloId { get; set; }

        [ForeignKey(nameof(JellegCimkeId))]
        public virtual Cimke Jelleg { get; set; }

        [ForeignKey(nameof(NapszakCimkeId))]
        public virtual Cimke Napszak { get; set; }

        [ForeignKey(nameof(HelyCimkeId))]
        public virtual Cimke Hely { get; set; }

        [ForeignKey(nameof(RogzitoSzemelyId))]
        public virtual Szemelyzet RogzitoSzemely { get; set; }

        [ForeignKey(nameof(EszleloId))]
        public virtual Szemelyzet Eszlelo { get; set; }
    }
}