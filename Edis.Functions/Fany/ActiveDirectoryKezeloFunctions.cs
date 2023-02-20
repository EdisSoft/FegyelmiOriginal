using Edis.Diagnostics;
using Edis.Entities.Fany;
using Edis.Functions.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Edis.Functions.Fany
{
    public class ActiveDirectoryKezeloFunctions : IActiveDirectoryKezeloFunctions
    {

        private const string Ou_Bv = "OU=BV";

        #region mezők
        private PrincipalContext _adContext;
        //private string _adHost;
        private string _adPort;
        private string _adFelhasznaloNev;
        private string _adJelszo;
        private string _adGyokerUt;
        private UserPrincipal _userPrincipal;
        private List<GroupPrincipal> _groupPrincipalList;
        #endregion 

        #region jellemzők
        //-private LogGenerator LogGenerator { get; set; }

        private PrincipalContext GyokerAdKontextus { get { return _adContext ?? (_adContext = LetrehozasPrincipalContext(_adGyokerUt)); } }

        #endregion

        #region konstruktorok
        public ActiveDirectoryKezeloFunctions(/*LogGenerator logGenerator*/)
        {
            var adHost = ConfigurationManager.AppSettings["adHost"];
            var adPort = ConfigurationManager.AppSettings["adPort"];
            var adFelhasznaloNev = ConfigurationManager.AppSettings["adFelhasznaloNev"];
            var adJelszo = ConfigurationManager.AppSettings["adJelszo"];
            var adGyokerUt = ConfigurationManager.AppSettings["adGyokerUt"];
            //-LogGenerator = logGenerator;

            Initialize(adHost, adPort, adFelhasznaloNev, adJelszo, adGyokerUt);
        }

        //public ActiveDirectoryKezeloFunctions(string adHost, string adPort, string adFelhasznaloNev, string adJelszo, string adGyokerUt)
        //{
        //    Initialize(adHost, adPort, adFelhasznaloNev, adJelszo, adGyokerUt);
        //}
        #endregion

        #region IActiveDirectoryKezelo eljárások

        public UserPrincipal BetoltesUserPrincipal(string sid)
        {
            return UserPrincipal.FindByIdentity(GyokerAdKontextus, IdentityType.Sid, sid);
        }

        public string KeresHadrendiSzam(string sid)
        {
            var hibahely = 0;
            try
            {
//#if DEBUG
//                return null;
//#else
                hibahely = 1;
                //if (_userPrincipal == null)
                    _userPrincipal = BetoltesUserPrincipal(sid);
                hibahely = 2;
                var search = new DirectorySearcher
                {
                    SearchRoot = LetrehozasDirectoryEntry(null),
                    Filter = "(sAMAccountName=" + _userPrincipal.SamAccountName + ")"
                };
                hibahely = 3; 
                search.PropertiesToLoad.Add("extensionAttribute1");
                hibahely = 4;
                SearchResult talalat = search.FindOne();
                hibahely = 5;
                if (talalat == null)
                {

                    return null;
                }
                else
                {
                    hibahely = 6;
                    if (talalat.Properties["extensionAttribute1"].Count > 0 &&
                        talalat.Properties["extensionAttribute1"][0] != null)
                    {
                        hibahely = 7;
                        string hadrendiSzam = talalat.Properties["extensionAttribute1"][0].ToString();
                        //-LogGenerator.LogToFile("ExtensionAttribute1:" + hadrendiSzam);
                        return hadrendiSzam;
                    }
                    else
                    {
                        //-LogGenerator.LogToFile("ExtensionAttribute1 null ");
                        return null;
                    }
                }
//#endif
            }
            catch (Exception e)
            {
                Log.Debug("KeresHadrendiSzam hiba, hibahely "+hibahely, e);
                return null;
            }
        }

        public string KeresRendfokozat(string sid)
        {
            try
            {
//#if DEBUG
//                return null;
//#else
                //if (_userPrincipal == null)
                    _userPrincipal = BetoltesUserPrincipal(sid);
                var search = new DirectorySearcher
                {
                    SearchRoot = LetrehozasDirectoryEntry(null),
                    Filter = "(sAMAccountName=" + _userPrincipal.SamAccountName + ")"
                };
                search.PropertiesToLoad.Add("extensionAttribute2");
                SearchResult talalat = search.FindOne();
                if (talalat == null)
                {

                    return null;
                }
                else
                {
                    if (talalat.Properties["extensionAttribute2"].Count > 0 &&
                        talalat.Properties["extensionAttribute2"][0] != null)
                    {
                        string rendfokozat = talalat.Properties["extensionAttribute2"][0].ToString();
                        //-LogGenerator.LogToFile("ExtensionAttribute2:" + rendfokozat);
                        return rendfokozat;
                    }
                    else
                    {
                        //-LogGenerator.LogToFile("ExtensionAttribute2 null ");
                        return null;
                    }
                }
//#endif
            }
            catch (Exception e)
            {
                //-LogGenerator.LogToFile("ERROR;KeresRendfokozat " + e.Message);
                return null;
            }
        }

        public string KeresBeosztas(string sid)
        {
            try
            {
//#if DEBUG
//                return null;
//#else
                //if (_userPrincipal == null)
                    _userPrincipal = BetoltesUserPrincipal(sid);
                var search = new DirectorySearcher
                {
                    SearchRoot = LetrehozasDirectoryEntry(null),
                    Filter = "(sAMAccountName=" + _userPrincipal.SamAccountName + ")"
                };
                search.PropertiesToLoad.Add("extensionAttribute3");
                SearchResult talalat = search.FindOne();
                if (talalat == null)
                {

                    return null;
                }
                else
                {
                    if (talalat.Properties["extensionAttribute3"].Count > 0 &&
                        talalat.Properties["extensionAttribute3"][0] != null)
                    {
                        string beosztas = talalat.Properties["extensionAttribute3"][0].ToString();
                        //-LogGenerator.LogToFile("ExtensionAttribute3:" + beosztas);
                        return beosztas;
                    }
                    else
                    {
                        //-LogGenerator.LogToFile("ExtensionAttribute3 null ");
                        return null;
                    }
                }
//#endif
            }
            catch (Exception e)
            {
                //-LogGenerator.LogToFile("ERROR;KeresBeosztas " + e.Message);
                return null;
            }
        }

        public string KeresTelefonszam(string sid)
        {
            try
            {
//#if DEBUG
//                return null;
//#else
                //if (_userPrincipal == null)
                    _userPrincipal = BetoltesUserPrincipal(sid);
                var search = new DirectorySearcher
                {
                    SearchRoot = LetrehozasDirectoryEntry(null),
                    Filter = "(sAMAccountName=" + _userPrincipal.SamAccountName + ")"
                };
                search.PropertiesToLoad.Add("facsimileTelephonenumber");
                SearchResult talalat = search.FindOne();
                if (talalat == null)
                {

                    return null;
                }
                else
                {
                    if (talalat.Properties["facsimileTelephonenumber"].Count > 0 &&
                        talalat.Properties["facsimileTelephonenumber"][0] != null)
                    {
                        string telefon = talalat.Properties["facsimileTelephonenumber"][0].ToString();
                        //-LogGenerator.LogToFile("ExtensionAttribute3:" + beosztas);
                        return telefon;
                    }
                    else
                    {
                        //-LogGenerator.LogToFile("ExtensionAttribute3 null ");
                        return null;
                    }
                }
//#endif
            }
            catch (Exception e)
            {
                //-LogGenerator.LogToFile("ERROR;KeresBeosztas " + e.Message);
                return null;
            }
        }

        public string KeresEmailcim(string sid)
        {
            try
            {
//#if DEBUG
//                return null;
//#else
                using (HostingEnvironment.Impersonate())
                {
                    //if (_userPrincipal == null)
                        _userPrincipal = BetoltesUserPrincipal(sid);
                    var search = new DirectorySearcher
                    {
                        SearchRoot = LetrehozasDirectoryEntry(null),
                        Filter = "(sAMAccountName=" + _userPrincipal.SamAccountName + ")"
                    };
                    search.PropertiesToLoad.Add("mail");
                    SearchResult talalat = search.FindOne();
                    if (talalat == null)
                    {

                        return null;
                    }
                    else
                    {
                        if (talalat.Properties["mail"].Count > 0 &&
                            talalat.Properties["mail"][0] != null)
                        {
                            string email = talalat.Properties["mail"][0].ToString();
                            //-LogGenerator.LogToFile("ExtensionAttribute3:" + beosztas);
                            return email;
                        }
                        else
                        {
                            //-LogGenerator.LogToFile("ExtensionAttribute3 null ");
                            return null;
                        }
                    }
                }
//#endif
            }
            catch (Exception e)
            {
                //-LogGenerator.LogToFile("ERROR;KeresBeosztas " + e.Message);
                return null;
            }
        }

        public List<Principal> KeresGroupFelhasznalok(string groupName)
        {
            GroupPrincipal qbeGroup = new GroupPrincipal(GyokerAdKontextus);
            qbeGroup.Name = groupName;
            PrincipalSearcher srch = new PrincipalSearcher(qbeGroup);
            srch.QueryFilter = qbeGroup;
            var groups = KeresGroup(groupName);
            if (groups == null)
                return null;
            List<Principal> users = new List<Principal>();
            foreach (var group in groups)
            {
                users.AddRange((group as GroupPrincipal).GetMembers().ToList());
            }
            return users;
        }

        public List<Principal> KeresGroup(string groupName)
        {
            GroupPrincipal qbeGroup = new GroupPrincipal(GyokerAdKontextus);
            qbeGroup.Name = groupName;
            PrincipalSearcher srch = new PrincipalSearcher(qbeGroup);
            srch.QueryFilter = qbeGroup;
            var groups = srch.FindAll();
            if (groups == null)
                return null;

            return groups.ToList();
        }

        public string KeresBeosztasiFokozat(string sid)
        {
            try
            {
//#if DEBUG
//                return null;
//#else
                //if (_userPrincipal == null)
                    _userPrincipal = BetoltesUserPrincipal(sid);
                var search = new DirectorySearcher
                {
                    SearchRoot = LetrehozasDirectoryEntry(null),
                    Filter = "(sAMAccountName=" + _userPrincipal.SamAccountName + ")"
                };
                search.PropertiesToLoad.Add("extensionAttribute4");
                SearchResult talalat = search.FindOne();
                if (talalat == null)
                {

                    return null;
                }
                else
                {
                    if (talalat.Properties["extensionAttribute4"].Count > 0 &&
                        talalat.Properties["extensionAttribute4"][0] != null)
                    {
                        string beosztasiFokozat = talalat.Properties["extensionAttribute4"][0].ToString();
                        //-LogGenerator.LogToFile("ExtensionAttribute4:" + beosztasiFokozat);
                        return beosztasiFokozat;
                    }
                    else
                    {
                        //-LogGenerator.LogToFile("ExtensionAttribute4 null ");
                        return null;
                    }
                }
//#endif
            }
            catch (Exception e)
            {
                //-LogGenerator.LogToFile("ERROR;KeresBeosztasiFokozat " + e.Message);
                return null;
            }
        }

        public string KeresDisplayName(string sid)
        {
            try
            {
                using (HostingEnvironment.Impersonate())
                {
                    //if (_userPrincipal == null)
                        _userPrincipal = BetoltesUserPrincipal(sid);
                    var search = new DirectorySearcher
                    {
                        SearchRoot = LetrehozasDirectoryEntry(null),
                        Filter = "(sAMAccountName=" + _userPrincipal.SamAccountName + ")"
                    };

                    search.PropertiesToLoad.Add("DisplayName");
                    SearchResult talalat = search.FindOne();
                    if (talalat == null)
                    {

                        return null;
                    }
                    else
                    {
                        if (talalat.Properties["DisplayName"].Count > 0 &&
                            talalat.Properties["DisplayName"][0] != null)
                        {
                            string displayName = talalat.Properties["DisplayName"][0].ToString();
                            return displayName;
                        }
                        else
                        {
                            //-LogGenerator.LogToFile("ExtensionAttribute4 null ");
                            return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //-LogGenerator.LogToFile("ERROR;KeresBeosztasiFokozat " + e.Message);
                return null;
            }
        }

        public string KeresSzemelyzetLoginNev(string sid)
        {
            try
            {
//#if DEBUG
//                return null;
//#else
                using (HostingEnvironment.Impersonate())
                {
                    //if (_userPrincipal == null)
                        _userPrincipal = BetoltesUserPrincipal(sid);

                    return _userPrincipal.SamAccountName;
                }
                //var search = new DirectorySearcher
                //{
                //    SearchRoot = LetrehozasDirectoryEntry(null),
                //    Filter = "(sAMAccountName=" + _userPrincipal.SamAccountName + ")"
                //};
                //search.PropertiesToLoad.Add("SamAccountName");
                //SearchResult talalat = search.FindOne();
                //if (talalat == null)
                //{

                //    return null;
                //}
                //else
                //{
                //    if (talalat.Properties["SamAccountName"].Count > 0 &&
                //        talalat.Properties["SamAccountName"][0] != null)
                //    {
                //        string beosztasiFokozat = talalat.Properties["SamAccountName"][0].ToString();
                //        //-LogGenerator.LogToFile("ExtensionAttribute4:" + beosztasiFokozat);
                //        return beosztasiFokozat;
                //    }
                //    else
                //    {
                //        //-LogGenerator.LogToFile("ExtensionAttribute4 null ");
                //        return null;
                //    }
                //}
//#endif
            }
            catch (Exception e)
            {
                //-LogGenerator.LogToFile("ERROR;KeresBeosztasiFokozat " + e.Message);
                return null;
            }
        }

        public List<GroupPrincipal> ListazasUserGroupPrincipal(string sid)
        {
            try
            {
                if (_groupPrincipalList != null)
                    return _groupPrincipalList;
                else
                {
#if DEBUG

                    _groupPrincipalList = new List<GroupPrincipal>
                           {
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "BVOP-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "BVEK-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "BALA-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "PEST-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "KFBT-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "MARI-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "SAUJ-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "SKFB-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "SZGD-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "APTA-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "BARA-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "PLHM-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "FTOK-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "VACI-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "DBRC-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "EGER-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "FVRS-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "GYOR-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "GYUL-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "KVAR-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "KECS-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "MISK-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "NYRH-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "PECS-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "SZEK-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "SZFV-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "SZOL-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "SZOM-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "VESZ-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "ZALA-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "KORH-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "IMEI-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "SZIR-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "TSZL-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "TOKO-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   }
                               ,
                               new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "KISK-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   },
                              new DebugGroupPrincipal(new PrincipalContext(ContextType.Machine))
                                   {
                                       Name = "BVEK-FN-GLOBALIS-MIND",
                                       Sid = new SecurityIdentifier("S-1-5-32-545")
                                   }
                           };
#else
                    using (HostingEnvironment.Impersonate())
                    {
                        //-LogGenerator.LogToFile("ListazasUserGroupPrincipal START: " + sid);       
                        var userPrincipal = BetoltesUserPrincipal(sid);
                        List<string> groupList = new List<string>();
                        var groupPrincipalDict = new Dictionary<string, GroupPrincipal>();

                        //-LogGenerator.LogToFile("ListazasUserGroupPrincipal LetrehozasDirectoryEntry: " + sid);       
                        var search = new DirectorySearcher
                        {
                            SearchRoot = LetrehozasDirectoryEntry(null),
                            Filter = "(sAMAccountName=" + userPrincipal.SamAccountName + ")"
                        };
                        search.PropertiesToLoad.Add("memberOf");

                        //-LogGenerator.LogToFile("ListazasUserGroupPrincipal FindOne: " + sid);       
                        SearchResult talalat = search.FindOne();
                        if (talalat == null)
                        {
                            return null;
                        }

                        //-LogGenerator.LogToFile("ListazasUserGroupPrincipal CsoportTagsagokHozzadasa: " + sid); 
                        CsoportTagsagokHozzadasa(talalat, groupPrincipalDict, groupList);
                        while (groupList.Any())
                        {
                            string dn = groupList.First();
                            groupList.Remove(dn);
                            var groupPrincipalSablon = new GroupPrincipal(LetrehozasPrincipalContext(dn));
                            using (var kereso = new PrincipalSearcher(groupPrincipalSablon))
                            {
                                var group = (GroupPrincipal)kereso.FindOne();
                                groupPrincipalDict[group.DistinguishedName] = group;
                            }

                            //search = new DirectorySearcher
                            //             {
                            //                 SearchRoot = LetrehozasDirectoryEntry(dn)
                            //             };
                            //search.PropertiesToLoad.Add("memberOf");
                            //talalat = search.FindOne();
                            //CsoportTagsagokHozzadasa(talalat, groupPrincipalDict, groupList);
                        }
                        _groupPrincipalList = groupPrincipalDict.Values.ToList();
                        //-LogGenerator.LogToFile("ListazasUserGroupPrincipal END: " + sid);       
                    }

#endif
                }
                return _groupPrincipalList;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool IsDcHost(string hostNev)
        {
            using (HostingEnvironment.Impersonate())
            {
                //-LogGenerator.LogToFile("Gepnev " + hostNev);
                using (
                    var search = new DirectorySearcher
                    {
                        SearchRoot = LetrehozasDirectoryEntry(Ou_Bv),
                        SearchScope = SearchScope.Subtree,
                        Filter = "(cn=" + hostNev + ")"
                    })
                {
                    var talalat = search.FindAll();
                                                                                                                                                                                                                                                            //-LogGenerator.LogToFile("Gép talalatok " + talalat.Count);
                    return talalat.Count > 0;
                }
            }
        }

        public List<GroupPrincipal> ListazasGroupPrincipal(string ouNev)
        {
            var queryString = string.Format("{0},{1}", Ou_Bv, _adGyokerUt);
            if (ouNev != null)
                queryString = string.Format("OU={0},{1}", ouNev, queryString);
            //-LogGenerator.LogToFile("ListazasGroupPrincipal queryString = " + queryString);
            var kersesoObjektum = new GroupPrincipal(LetrehozasPrincipalContext(queryString));
            //-LogGenerator.LogToFile("GroupPrincipal created");

            using (var kereso = new PrincipalSearcher(kersesoObjektum))
            {
                //-LogGenerator.LogToFile("PrincipalSearcher created");
                return kereso.FindAll().Cast<GroupPrincipal>().ToList();
            }
        }

        public List<GroupPrincipal> ListazasGroupPrincipalTOL(string ouNev)
        {
            string queryString = "";
            if (ouNev != null)
            {
                queryString = string.Format("OU={0},{1}", ouNev, _adGyokerUt);
            }
            //-LogGenerator.LogToFile("ListazasGroupPrincipal queryString = " + queryString);
            var kersesoObjektum = new GroupPrincipal(LetrehozasPrincipalContext(queryString));
            //-LogGenerator.LogToFile("GroupPrincipal created");

            using (var kereso = new PrincipalSearcher(kersesoObjektum))
            {
                //-LogGenerator.LogToFile("PrincipalSearcher created");
                return kereso.FindAll().Cast<GroupPrincipal>().ToList();
            }
        }

        public List<string> ListazasOU()
        {
            //IList<string> ous = new List<string>();

            using (HostingEnvironment.Impersonate())
            {
                string path = ConfigurationManager.AppSettings["ADTarsszervezetTarolo"] != null
                                  ? "LDAP://OU=" + ConfigurationManager.AppSettings["ADTarsszervezetTarolo"] + "," + _adGyokerUt
                                  : "LDAP://" + _adGyokerUt;


                DirectoryEntry rootDSE = new DirectoryEntry(path);

                var dsFindOUs = new DirectorySearcher
                {
                    SearchRoot = rootDSE,
                    Filter = "(objectClass=organizationalUnit)",
                    SearchScope = SearchScope.OneLevel
                };
                dsFindOUs.PropertiesToLoad.Add("ou");
                dsFindOUs.PropertiesToLoad.Add("objectGUID");
                dsFindOUs.PropertiesToLoad.Add("description");
                dsFindOUs.PropertiesToLoad.Add("distinguishedName");

                SearchResultCollection src = dsFindOUs.FindAll();

                List<string> res = new List<string>();

                foreach (SearchResult sR in src)
                {
                    string resElement = "";
                    resElement += sR.Properties["ou"][0].ToString() + "|";
                    resElement += sR.Properties["objectGUID"][0].ToString() + "|";
                    resElement += (sR.Properties["description"].Count == 0 ? "" : sR.Properties["description"][0].ToString()) + "|";
                    resElement += sR.Properties["distinguishedName"][0].ToString();
                    res.Add(resElement);
                }

                //if(src != null || src.Count != 0)
                //{
                //    foreach (SearchResult result in src)
                //    {
                //        ous.Add(result.Properties["ou"][0].ToString());
                //    }
                //}
                //return ous;

                return res;
                //return src;
            }
        }

        #endregion IActiveDirectoryKezelo eljárások

        #region segéd eljárások

        private void Initialize(string adHost, string adPort, string adFelhasznaloNev, string adJelszo, string adGyokerUt)
        {
            //_adHost = adHost;
            _adPort = adPort;
            _adFelhasznaloNev = adFelhasznaloNev;
            _adJelszo = adJelszo;
            _adGyokerUt = adGyokerUt;
        }

        private DirectoryEntry LetrehozasDirectoryEntry(string ut)
        {
            //return new DirectoryEntry("LDAP://" + _adHost + @":" + _adPort + "/" + ut, _adFelhasznaloNev, _adJelszo);
            /*LogGenerator.LogToFile(Domain.GetComputerDomain().ToString());
            LogGenerator.LogToFile(Domain.GetComputerDomain().GetDirectoryEntry().ToString());
            LogGenerator.LogToFile(Domain.GetComputerDomain().GetDirectoryEntry().Path);*/

            Domain domain = Domain.GetComputerDomain();
            var rootDSE = Domain.GetComputerDomain().GetDirectoryEntry();
            if (string.IsNullOrEmpty(ut))
            {
                return rootDSE;
            }
            foreach (var directoryEntry in rootDSE.Children)
            {
                if (ut == ((DirectoryEntry)directoryEntry).Name)
                {
                    return (DirectoryEntry)directoryEntry;
                }
            }
            return null;
        }

        private PrincipalContext LetrehozasPrincipalContext(string queryString)
        {
            var domain = Domain.GetCurrentDomain();
            var dc = domain.FindDomainController();
            var dcName = string.IsNullOrEmpty(_adPort) ? dc.Name : dc.Name + @":" + _adPort;
            return new PrincipalContext(ContextType.Domain, dcName, queryString, _adFelhasznaloNev, _adJelszo);
            //return new PrincipalContext(ContextType.Domain, _adHost + @":" + _adPort, queryString, _adFelhasznaloNev, _adJelszo);
        }

        private void CsoportTagsagokHozzadasa(SearchResult result, Dictionary<string, GroupPrincipal> groupPrincipalDict, IList<string> groupList)
        {
            foreach (var tagsag in result.Properties["memberOf"])
            {
                var dn = (string)tagsag;
                if (groupPrincipalDict.ContainsKey(dn)) continue;
                groupPrincipalDict.Add(dn, null);
                groupList.Add(dn);
            }
        }

        #endregion segéd eljárások
    }
}
