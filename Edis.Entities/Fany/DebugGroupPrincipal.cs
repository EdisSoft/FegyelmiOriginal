using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    public class DebugGroupPrincipal : GroupPrincipal
    {
        public new SecurityIdentifier Sid { get; set; }
        public new String Name { get; set; }

        public DebugGroupPrincipal(PrincipalContext principalContext)
            : base(principalContext)
        {
        }
    }
}
