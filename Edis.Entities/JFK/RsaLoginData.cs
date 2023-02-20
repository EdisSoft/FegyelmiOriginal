using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.JFK
{
    public class RsaLoginData
    {
        public int ProjektId { get; set; }
        public string Sid { get; set; }
        public string Cegnev { get; set; }
        public int? WorkItemId { get; set; }
        public int? BaratId { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
