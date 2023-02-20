using Edis.Entities.Fany;
using Edis.Functions.Base;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.Fany
{
    public interface IActiveDirectoryKezeloFunctions
    {
        UserPrincipal BetoltesUserPrincipal(String sid);

        List<Principal> KeresGroupFelhasznalok(string groupName);

        List<GroupPrincipal> ListazasUserGroupPrincipal(string sid);

        bool IsDcHost(string hostNev);

        List<GroupPrincipal> ListazasGroupPrincipal(string ouNev);
        List<GroupPrincipal> ListazasGroupPrincipalTOL(string ouNev);

        List<string> ListazasOU();

        string KeresHadrendiSzam(string sid);
        string KeresRendfokozat(string sid);
        string KeresBeosztas(string sid);
        string KeresBeosztasiFokozat(string sid);

        string KeresDisplayName(string sid);

        string KeresTelefonszam(string sid);

        string KeresEmailcim(string sid);

        string KeresSzemelyzetLoginNev(string sid);
    }
}
