using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.JFK.FENY
{
    [Table("Fegyelmi.SzakteruletiVelemenyKeresek")]
    public class SzakteruletiVelemenyKeresek : KeziJavitoEntity
    {
        [Column("FegyelmiUgyIdLista")]
        public string FegyelmiUgyIdLista { get; set; }
        [Column("SzakvelemenyKeroje")]
        public string SzakvelemenyKeroje { get; set; }
        [Column("CimzettLista")]
        public string CimzettLista { get; set; }
        [Column("Tema")]
        public string Tema { get; set; }
        [Column("GeneraltLink")]
        public string GeneraltLink { get; set; }
        [Column("Velemenyezve")]
        public DateTime? Velemenyezve { get; set; }
        [Column("HatarIdo")]
        public DateTime HatarIdo { get; set; }
    }
}
