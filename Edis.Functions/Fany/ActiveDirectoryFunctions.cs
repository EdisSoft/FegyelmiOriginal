using Edis.Diagnostics;
using Edis.Entities.Common;
using Edis.Entities.Fany;
using Edis.Functions.Base;
using Edis.Repositories.Contexts;
using Edis.ViewModels.Base;
using Edis.ViewModels.Fany;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Edis.Functions.Fany
{
    public class ActiveDirectoryFunctions : IActiveDirectoryFunctions
    {

        #region mezők
        private readonly IActiveDirectoryKezeloFunctions _adKezelo;
        #endregion

        #region jellemzők
        private IActiveDirectoryKezeloFunctions AdKezelo { get { return _adKezelo; } }
        #endregion

        #region konstruktor
        public ActiveDirectoryFunctions(IActiveDirectoryKezeloFunctions adKezelo)
        {
            _adKezelo = adKezelo;
        }

       
        #endregion

        #region eljárások
        public UserPrincipal BetoltesUserPrincipal(String sid)
        {
            return AdKezelo.BetoltesUserPrincipal(sid);
        }

        public List<Principal> KeresGroupFelhasznalok(string groupName)
        {
            return AdKezelo.KeresGroupFelhasznalok(groupName);
        }
        public List<AdUser> KeresActiveDirectoryUsers(string intezetGroup)
        {
            List<AdUser> result = KonasoftBVFonixContext.GetContextInstance().Database.SqlQuery<AdUser>
            ("dbo.KeresActiveDirectoryUsers @AdGroupName",
                new SqlParameter("@AdGroupName", intezetGroup)
            ).ToList();
            return result;
        }

        public List<AdUser> KeresActiveDirectoryUsersForBvop(string intezetGroup)
        {
            List<AdUser> result = KonasoftBVFonixContext.GetContextInstance().Database.SqlQuery<AdUser>
            ("dbo.KeresActiveDirectoryUsersForBvop @AdGroupName",
                new SqlParameter("@AdGroupName", intezetGroup)
            ).ToList();
            return result;
        }

        public List<AdUserEmail> KeresActiveDirectoryUsersEmail(string intezetGroup)
        {
            List<AdUserEmail> result = KonasoftBVFonixContext.GetContextInstance().Database.SqlQuery<AdUserEmail>
            ("dbo.KeresActiveDirectoryUsers @AdGroupName",
                new SqlParameter("@AdGroupName", intezetGroup)
            ).ToList();
            return result;
        }
        public List<AdUserEmail> KeresActiveDirectoryUsersEmailForBvop(string intezetGroup)
        {
            List<AdUserEmail> result = KonasoftBVFonixContext.GetContextInstance().Database.SqlQuery<AdUserEmail>
            ("dbo.KeresActiveDirectoryUsersForBvop @AdGroupName",
                new SqlParameter("@AdGroupName", intezetGroup)
            ).ToList();
            return result;
        }

        public List<AdUser> KeresGroupFelhasznalokMsSql(string intezetAzon, string groupName)
        {
            List<AdUser> result = KonasoftBVFonixContext.GetContextInstance().Database.SqlQuery<AdUser>
            ("dbo.KeresActiveDirectoryGroupMembers @BvIntezetiAzonkod,@AdGroupName",
                new SqlParameter("@BvIntezetiAzonkod", intezetAzon),
                new SqlParameter("@AdGroupName", groupName)
            ).ToList();
            return result;
        }

        public List<AdUserEmail> KeresGroupFelhasznalokMsSqlNoFm(string intezetAzon, string groupName, bool csakEmailel)
        {
            List<AdUserEmail> result = KonasoftBVFonixContext.GetContextInstance().Database.SqlQuery<AdUserEmail>
            ("dbo.KeresActiveDirectoryGroupMembersNoFN @BvIntezetiAzonkod,@AdGroupName,@csakEmail",
                new SqlParameter("@BvIntezetiAzonkod", intezetAzon),
                new SqlParameter("@AdGroupName", groupName),
                new SqlParameter("@csakEmail", csakEmailel)
            ).ToList();
            return result;
        }

        public List<AdUser> KeresGroupFelhasznalokNoFmWithSid(string intezetAzon, string groupName)
        {
            List<AdUser> result = KonasoftBVFonixContext.GetContextInstance().Database.SqlQuery<AdUser>
            ("dbo.KeresActiveDirectoryUsersNoFn @BvIntezetiAzonkod,@AdGroupName",
                new SqlParameter("@BvIntezetiAzonkod", intezetAzon),
                new SqlParameter("@AdGroupName", groupName)
            ).ToList();
            return result;
        }

        public List<GroupPrincipal> ListazasUserGroupPrincipal(string sid)
        {
            return AdKezelo.ListazasUserGroupPrincipal(sid);
        }

        public List<GroupPrincipal> ListazasGroupPrincipal(string ouNev)
        {
            return AdKezelo.ListazasGroupPrincipal(ouNev);
        }

        public List<GroupPrincipal> ListazasGroupPrincipalTOL(string ouNev)
        {
            return AdKezelo.ListazasGroupPrincipalTOL(ouNev);
        }

        public List<GroupPrincipal> ListazasFttrUgyintezo(string ouNev)
        {

            return null;
        }

        public List<string> ListazasOU()
        {
            return AdKezelo.ListazasOU();
        }

        public bool IsDcHost(string hostNev)
        {
            return AdKezelo.IsDcHost(hostNev);
        }

        public string KeresHadrendiSzam(string sid)
        {
            try
            {
                using (HostingEnvironment.Impersonate())
                {
                    var szam = AdKezelo.KeresHadrendiSzam(sid);
                    return szam;
                }
            }
            catch (Exception e)
            {
                Log.Debug("KeresHadrendiSzam hiba", e);
                return null;
            }
        }

        public string KeresRendfokozat(string sid)
        {
            try
            {
                using (HostingEnvironment.Impersonate())
                {
                    return AdKezelo.KeresRendfokozat(sid);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string KeresBeosztas(string sid)
        {
            try
            {
                using (HostingEnvironment.Impersonate())
                {
                    return AdKezelo.KeresBeosztas(sid);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string KeresTelefonszam(string sid)
        {
            try
            {
                return AdKezelo.KeresTelefonszam(sid);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string KeresBeosztasiFokozat(string sid)
        {
            try
            {
                using (HostingEnvironment.Impersonate())
                {
                    return AdKezelo.KeresBeosztasiFokozat(sid);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string KeresDisplayName(string sid)
        {
            try
            {
                return AdKezelo.KeresDisplayName(sid);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string KeresSzemelyzetLoginNev(string sid)
        {
            try
            {
                return AdKezelo.KeresSzemelyzetLoginNev(sid);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //BvShop hozadékai

        public ActiveDirectoryUserRoleModel GetUserAndRoles(string sid)
        {
            var adPort = ConfigurationManager.AppSettings["adPort"];
            var adFelhasznaloNev = ConfigurationManager.AppSettings["adFelhasznaloNev"];
            var adJelszo = ConfigurationManager.AppSettings["adJelszo"];
            var adGyokerUt = ConfigurationManager.AppSettings["adGyokerUt"];



            using (HostingEnvironment.Impersonate())
            {

                var domain = Domain.GetCurrentDomain();
                var dc = domain.FindDomainController();
                var dcName = string.IsNullOrEmpty(adPort) ? dc.Name : dc.Name + @":" + adPort;
                var principalcontext = new PrincipalContext(ContextType.Domain, dcName, adGyokerUt, adFelhasznaloNev, adJelszo);


                var userPrincipal = UserPrincipal.FindByIdentity(principalcontext, IdentityType.Sid, sid);

                //Log.Info($"Kint dcName: {dcName}, adFelhasznaloNev: {adFelhasznaloNev}, adJelszo: {adJelszo} ");


                //List<string> groupList = new List<string>();
                //var groupPrincipalDict = new Dictionary<string, GroupPrincipal>();


                //var rootDSE = Domain.GetComputerDomain().GetDirectoryEntry();

                //var search = new DirectorySearcher
                //{
                //    SearchRoot = rootDSE,
                //    Filter = "(sAMAccountName=" + userPrincipal.SamAccountName + ")"
                //};
                //search.PropertiesToLoad.Add("memberOf");

                ////-LogGenerator.LogToFile("ListazasUserGroupPrincipal FindOne: " + sid);       
                //SearchResult talalat = search.FindOne();
                //if (talalat == null)
                //{
                //    return null;
                //}

                ////CsoportTagsagokHozzadasa(talalat, groupPrincipalDict, groupList);

                //foreach (var tagsag in talalat.Properties["memberOf"])
                //{
                //    var dn = (string)tagsag;
                //    if (groupPrincipalDict.ContainsKey(dn)) continue;
                //    groupPrincipalDict.Add(dn, null);
                //    groupList.Add(dn);
                //}

                //while (groupList.Any())
                //{
                //    string dn = groupList.First();
                //    groupList.Remove(dn);

                //    var domain2 = Domain.GetCurrentDomain();
                //    var dc2 = domain.FindDomainController();
                //    var dcName2 = string.IsNullOrEmpty(adPort) ? dc.Name : dc.Name + @":" + adPort;
                //    var princCont = new PrincipalContext(ContextType.Domain, dcName, dn, adFelhasznaloNev, adJelszo);

                //    var groupPrincipalSablon = new GroupPrincipal(princCont);
                //    using (var kereso = new PrincipalSearcher(groupPrincipalSablon))
                //    {
                //        var group = (GroupPrincipal)kereso.FindOne();
                //        groupPrincipalDict[group.DistinguishedName] = group;
                //    }

                //    //search = new DirectorySearcher
                //    //             {
                //    //                 SearchRoot = LetrehozasDirectoryEntry(dn)
                //    //             };
                //    //search.PropertiesToLoad.Add("memberOf");
                //    //talalat = search.FindOne();
                //    //CsoportTagsagokHozzadasa(talalat, groupPrincipalDict, groupList);
                //}
                //var groupPrincipalList = groupPrincipalDict.Values.ToList();

                ActiveDirectoryUserRoleModel userAndRoles2 = new ActiveDirectoryUserRoleModel();
                //userAndRoles2.AdUserGroups = groupPrincipalList;
                userAndRoles2.AdUser = userPrincipal;


                return userAndRoles2;
                ////using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, dcName, string.Format("OU=BV,{0}", adGyokerUt), adFelhasznaloNev, adJelszo))

                //using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, dcName, adFelhasznaloNev, adJelszo))
                //{


                //    Log.Info($"Bent dcName: {dcName}, adFelhasznaloNev: {adFelhasznaloNev}, adJelszo: {adJelszo} ");

                //    Log.Info($"Bent principalContext: {principalContext.UserName}");

                //    using (HostingEnvironment.Impersonate())
                //    {

                //        ActiveDirectoryUserRoleModel userAndRoles = new ActiveDirectoryUserRoleModel();
                //        PrincipalSearchResult<Principal> groups = null;


                //        UserPrincipal user = UserPrincipal.FindByIdentity(principalContext, IdentityType.Sid, sid);

                //        if (user != null)
                //        {
                //            groups = user.GetAuthorizationGroups();

                //            //foreach (Principal principal in groups)
                //            //{

                //            //}
                //        }

                //        userAndRoles.AdUser = user;
                //        userAndRoles.AdUserGroups = groups;
                //        return userAndRoles;
                //    }

                //}
            }
        }
        public List<GroupPrincipal> GetUserGroups(string sid)
        {
            var adPort = ConfigurationManager.AppSettings["adPort"];
            var adFelhasznaloNev = ConfigurationManager.AppSettings["adFelhasznaloNev"];
            var adJelszo = ConfigurationManager.AppSettings["adJelszo"];
            var adGyokerUt = ConfigurationManager.AppSettings["adGyokerUt"];



            using (HostingEnvironment.Impersonate())
            {

                var domain = Domain.GetCurrentDomain();
                var dc = domain.FindDomainController();
                var dcName = string.IsNullOrEmpty(adPort) ? dc.Name : dc.Name + @":" + adPort;
                var principalcontext = new PrincipalContext(ContextType.Domain, dcName, adGyokerUt, adFelhasznaloNev, adJelszo);


                var userPrincipal = UserPrincipal.FindByIdentity(principalcontext, IdentityType.Sid, sid);

                //Log.Info($"Kint dcName: {dcName}, adFelhasznaloNev: {adFelhasznaloNev}, adJelszo: {adJelszo} ");


                List<string> groupList = new List<string>();
                var groupPrincipalDict = new Dictionary<string, GroupPrincipal>();


                var rootDSE = Domain.GetComputerDomain().GetDirectoryEntry();

                //var search = new DirectorySearcher
                //{
                //    SearchRoot = rootDSE,
                //    Filter = "(sAMAccountName=" + userPrincipal.SamAccountName + ")"
                //};
                //search.PropertiesToLoad.Add("memberOf");

                ////-LogGenerator.LogToFile("ListazasUserGroupPrincipal FindOne: " + sid);       
                //SearchResult talalat = search.FindOne();
                //if (talalat == null)
                //{
                //    return null;
                //}

                //CsoportTagsagokHozzadasa(talalat, groupPrincipalDict, groupList);

                //foreach (var tagsag in talalat.Properties["memberOf"])
                //{
                //    var dn = (string)tagsag;
                //    if (groupPrincipalDict.ContainsKey(dn)) continue;
                //    groupPrincipalDict.Add(dn, null);
                //    groupList.Add(dn);
                //}

                foreach (var item in userPrincipal.GetGroups())
                {
                    var group = (GroupPrincipal)item;
                    groupPrincipalDict[group.DistinguishedName] = group;
                }

                //while (groupList.Any())
                //{
                //    string dn = groupList.First();
                //    groupList.Remove(dn);

                //    var domain2 = Domain.GetCurrentDomain();
                //    var dc2 = domain.FindDomainController();
                //    var dcName2 = string.IsNullOrEmpty(adPort) ? dc.Name : dc.Name + @":" + adPort;
                //    var princCont = new PrincipalContext(ContextType.Domain, dcName, dn, adFelhasznaloNev, adJelszo);

                //    var groupPrincipalSablon = new GroupPrincipal(princCont);
                //    using (var kereso = new PrincipalSearcher(groupPrincipalSablon))
                //    {
                //        var group = (GroupPrincipal)kereso.FindOne();
                //        groupPrincipalDict[group.DistinguishedName] = group;
                //    }

                //    //search = new DirectorySearcher
                //    //             {
                //    //                 SearchRoot = LetrehozasDirectoryEntry(dn)
                //    //             };
                //    //search.PropertiesToLoad.Add("memberOf");
                //    //talalat = search.FindOne();
                //    //CsoportTagsagokHozzadasa(talalat, groupPrincipalDict, groupList);
                //}
                var groupPrincipalList = groupPrincipalDict.Values.ToList();

                ActiveDirectoryUserRoleModel userAndRoles2 = new ActiveDirectoryUserRoleModel();
                userAndRoles2.AdUserGroups = groupPrincipalList;


                return groupPrincipalList;
            }
        }

        public UserPrincipal GetUserFromSamAccountName(string samAccountName)
        {
            var adPort = ConfigurationManager.AppSettings["adPort"];
            var adFelhasznaloNev = ConfigurationManager.AppSettings["adFelhasznaloNev"];
            var adJelszo = ConfigurationManager.AppSettings["adJelszo"];
            var adGyokerUt = ConfigurationManager.AppSettings["adGyokerUt"];

            var domain = Domain.GetCurrentDomain();
            var dc = domain.FindDomainController();
            var dcName = string.IsNullOrEmpty(adPort) ? dc.Name : dc.Name + @":" + adPort;
            var principalcontext = new PrincipalContext(ContextType.Domain, dcName, adGyokerUt, adFelhasznaloNev, adJelszo);

            var userPrincipal = UserPrincipal.FindByIdentity(principalcontext, IdentityType.SamAccountName, samAccountName);
            
            return userPrincipal;
        }

        public string GetSidFromSamAccountName(string samAccountName)
        {
            var adPort = ConfigurationManager.AppSettings["adPort"];
            var adFelhasznaloNev = ConfigurationManager.AppSettings["adFelhasznaloNev"];
            var adJelszo = ConfigurationManager.AppSettings["adJelszo"];
            var adGyokerUt = ConfigurationManager.AppSettings["adGyokerUt"];

            var domain = Domain.GetCurrentDomain();
            var dc = domain.FindDomainController();
            var dcName = string.IsNullOrEmpty(adPort) ? dc.Name : dc.Name + @":" + adPort;
            var principalcontext = new PrincipalContext(ContextType.Domain, dcName, adGyokerUt, adFelhasznaloNev, adJelszo);

            var userPrincipal = UserPrincipal.FindByIdentity(principalcontext, IdentityType.SamAccountName, samAccountName);

            return userPrincipal.Sid.ToString();
        }

        public UserPrincipal GetUserFromSid(string sid)
        {
            var adPort = ConfigurationManager.AppSettings["adPort"];
            var adFelhasznaloNev = ConfigurationManager.AppSettings["adFelhasznaloNev"];
            var adJelszo = ConfigurationManager.AppSettings["adJelszo"];
            var adGyokerUt = ConfigurationManager.AppSettings["adGyokerUt"];

            var domain = Domain.GetCurrentDomain();
            var dc = domain.FindDomainController();
            var dcName = string.IsNullOrEmpty(adPort) ? dc.Name : dc.Name + @":" + adPort;
            var principalcontext = new PrincipalContext(ContextType.Domain, dcName, adGyokerUt, adFelhasznaloNev, adJelszo);

            var userPrincipal = UserPrincipal.FindByIdentity(principalcontext, IdentityType.Sid, sid);

            return userPrincipal;
        }

        public List<GroupPrincipal> GetUserGroupsFromAd(string userSid)
        {
            var user = GetUserFromSid(userSid);
            return GetUserGroupsFromAd(user);
        }

        public List<GroupPrincipal> GetUserGroupsFromAd(UserPrincipal user)
        {
            var groupPrincipalList = user.GetGroups().Select(x => (GroupPrincipal)x).ToList();
            return groupPrincipalList;
        }

        #endregion
    }
}
