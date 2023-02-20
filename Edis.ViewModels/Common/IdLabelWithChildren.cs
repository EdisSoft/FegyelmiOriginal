using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.Common
{
    public class IdLabelWithChildren
    {
        public string id { get; set; }
        public string label { get; set; }
        public List<IdLabelWithChildren> children { get; set; }
    }
}
