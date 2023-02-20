using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.JFK.FENY
{
    [Table("Fegyelmi.FenyitesVegrehajtasok")]
    public class FenyitesVegrehajtasok : KeziJavitoEntity
    {
        [Column("FegyelmiUgyId")]
        public int FegyelmiUgyId { get; set; }
        [Column("KezdeteIdo")]
        public DateTime? KezdeteIdo { get; set; }
        [Column("VegeIdo")]
        public DateTime? VegeIdo { get; set; }
        [Column("Hossz")]
        public double? Hossz { get; set; }
        [Column("MennyisegiEgyegCimkeId")]
        public int? MennyisegiEgyegCimkeId { get; set; }
    }
}
