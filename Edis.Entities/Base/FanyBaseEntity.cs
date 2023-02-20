using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Base
{
    public class FanyBaseEntity : BaseEntity
    {
        [Column("ID")]
        [Key]
        [Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public new int Id { get; set; }


    }
}
