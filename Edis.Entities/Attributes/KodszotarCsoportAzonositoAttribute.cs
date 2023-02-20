using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Attributes
{
    public class KodszotarCsoportAzonositoAttribute : Attribute
    {
        public string Azonosito { get; set; }

        public KodszotarCsoportAzonositoAttribute()
            : base()
        {
        }

        public KodszotarCsoportAzonositoAttribute(string azonosito)
            : base()
        {
            this.Azonosito = azonosito;
        }
    }
}
