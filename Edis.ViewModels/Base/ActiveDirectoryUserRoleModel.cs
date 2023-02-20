using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;

namespace Edis.ViewModels.Base
{
    public class ActiveDirectoryUserRoleModel
    {
        public UserPrincipal AdUser { get; set; }

        public List<GroupPrincipal> AdUserGroups { get; set; }
    }
}
