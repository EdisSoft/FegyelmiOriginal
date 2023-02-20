using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.JFK.FENY
{
    [Table("Fegyelmi.VegrehajtoSzakteruletek")]
    public class VegrehajtoSzakteruletek : KeziJavitoEntity
    {
        [Column("IntezetId")]
        public int IntezetId { get; set; }
        [Column("LetetesCimzettLista")]
        public string LetetesCimzettLista { get; set; }
        [Column("RendezvenySzervezoCimzettLista")]
        public string RendezvenySzervezoCimzettLista { get; set; }
    }
}
