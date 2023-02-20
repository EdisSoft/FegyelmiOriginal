using Edis.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    public class MenuItemModel
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public Jogosultsagok? PermissionCode { get; set; }
        public bool Enabled { get; set; }
        public string NavigateUrl { get; set; }
        public string OnClickMethod { get; set; }
    }
}
