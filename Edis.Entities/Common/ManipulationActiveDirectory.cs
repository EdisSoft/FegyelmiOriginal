using Edis.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edis.Entities.Common
{
    [Table("dbo.ManipulationActiveDirectory")]
    public class ManipulationActiveDirectory : SoftDeleteEntity
    {
        [Key]
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Column("Sid")]
        public string Sid { get; set; }
        [Column("Jogosultsag")]
        public string Jogosultsag { get; set; }
        [Column("Megjegyzes")]
        public string Megjegyzes { get; set; }
    }
}
