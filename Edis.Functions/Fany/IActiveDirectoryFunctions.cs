using Edis.Entities.Common;
using Edis.Entities.Fany;
using Edis.Functions.Base;
using Edis.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.Fany
{
    public interface IActiveDirectoryFunctions 
    {
        #region eljárások
        UserPrincipal BetoltesUserPrincipal(String sid);
        List<GroupPrincipal> ListazasUserGroupPrincipal(string sid);
        List<GroupPrincipal> ListazasGroupPrincipal(string ouNev);
        List<GroupPrincipal> ListazasGroupPrincipalTOL(string ouNev);
        List<GroupPrincipal> ListazasFttrUgyintezo(string ouNev);

        List<Principal> KeresGroupFelhasznalok(string groupName);

        List<AdUser> KeresGroupFelhasznalokMsSql(string intezetAzon, string groupName);

        List<AdUserEmail> KeresGroupFelhasznalokMsSqlNoFm(string intezetAzon, string groupName, bool csakEmailel);

        List<AdUser> KeresGroupFelhasznalokNoFmWithSid(string intezetAzon, string groupName);
        List<string> ListazasOU();
        bool IsDcHost(string hostNev);
        string KeresHadrendiSzam(string sid);
        string KeresRendfokozat(string sid);
        string KeresBeosztas(string sid);
        string KeresTelefonszam(string sid);
        string KeresBeosztasiFokozat(string sid);

        string KeresDisplayName(string sid);

        string KeresSzemelyzetLoginNev(string sid);

        ActiveDirectoryUserRoleModel GetUserAndRoles(string sid);
        List<GroupPrincipal> GetUserGroups(string sid);

        UserPrincipal GetUserFromSamAccountName(string samAccountName);
        string GetSidFromSamAccountName(string samAccountName);
        #endregion
        List<AdUser> KeresActiveDirectoryUsers(string intezetGroup);
        List<AdUserEmail> KeresActiveDirectoryUsersEmail(string intezetGroup);
        List<AdUser> KeresActiveDirectoryUsersForBvop(string intezetGroup);
        List<AdUserEmail> KeresActiveDirectoryUsersEmailForBvop(string intezetGroup);
    }
}
