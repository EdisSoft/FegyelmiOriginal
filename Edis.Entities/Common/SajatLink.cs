using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Common
{
    [Table("sajat_link")]
    public class SajatLink : KonaExtendedEntity
    {
        [Column("UGYINTEZO_SID")]
        public string UgyintezoSid { get; set; }
        
        [Column("LINK_ROVID_NEVE")]
        public string RovidNev { get; set; }

        [Column("LINK")]
        public string Link { get; set; }

        [Column("MODUL_NEVE")]
        public string ModulNeve { get; set; }
    }
}
