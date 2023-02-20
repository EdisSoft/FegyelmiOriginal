using Edis.Diagnostics;
using Edis.Entities.Common;
using Edis.Functions.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Hosting;

namespace Edis.Functions.Common
{
    public class AdKezeloFunctions : IAdKezeloFunctions
    {
        private const string DisplayNameAttribute = "DisplayName";
        private const string SAMAccountNameAttribute = "SAMAccountName";
        private const string SidAttribute = "objectSid";

        readonly string ADFelhasznaloNev, ADJelszo, ADPort;

        public AdKezeloFunctions()
        {
            ADFelhasznaloNev = ConfigurationManager.AppSettings["ADFelhasznaloNev"];
            ADJelszo = ConfigurationManager.AppSettings["ADJelszo"];
            ADPort = ConfigurationManager.AppSettings["ADPort"];
        }

        public bool ValidateUser(string userName, string password, out string passwordHash)
        {
            passwordHash = null;
            Log.Debug("ValidateUser 1.");

            bool isValid = ValidateCredentials(userName, password);
            Log.Debug("ValidateUser 2.");

            if (!isValid)
            {
                Log.Debug("ValidateUser 3.");

                var adUser = GetUserFromSamAccountName(userName);
                Log.Debug("ValidateUser 4.");

                if (adUser == null)
                {
                    throw new WarningException("Nincs ilyen felhasználó!", WarningExceptionLevel.Warning);
                }
                if (adUser.IsAccountLockedOut())
                {
                    throw new WarningException("A felhasználói fiók zárolva van!", WarningExceptionLevel.Warning);
                }
                if (adUser.Enabled == false)
                {
                    throw new WarningException("A felhasználói fiók le van tiltva!", WarningExceptionLevel.Warning);
                }

                throw new WarningException("Hibás felhasználónév vagy jelszó!", WarningExceptionLevel.Warning);
            }
            Log.Debug("ValidateUser 5.");

            passwordHash = HashPassword(password);
            Log.Debug("ValidateUser 6." + isValid);

            return isValid;
            

        }

        private static string HashPassword(string password)
        {
            string passwordHash;
            using (var sha256 = SHA256.Create())
            {
                var crypto = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                passwordHash = string.Join("", crypto.Select(x => string.Format("{0:x2}", x)).Take(32));
            }

            return passwordHash;
        }

        private bool ValidateCredentials(string username, string password)
        {
            try
            {
                using (var context = GetPrincipalContext())
                {
                    var result = context.ValidateCredentials(username, password);
                    return result;
                }

            }
            catch (Exception ex)
            {
                Log.Error("ValidateCredentials - domainControllers ", ex);
            }
            return false;
        }

        private ActiveDirectoryModel GetDirectoryEntry()
        {
            var result = new ActiveDirectoryModel();
            var username = ADFelhasznaloNev;
            var password = ADJelszo;

            var domainControllers = Domain.GetCurrentDomain().FindAllDomainControllers();
            foreach (DomainController depItem in domainControllers)
            {
                try
                {
                    Log.Debug("AD ctrl: " + depItem.Name);
                    result.Path = depItem.Name;
                    var path = depItem.GetDirectoryEntry().Path;
                    result.Entry = new DirectoryEntry(path, username, password);
                    return result;
                }
                catch (Exception ex)
                {
                    Log.Error("GetDirectoryEntry - FindAllDomainControllers ", ex);
                }
            }
            throw new Exception("Egyik AD szerver sem elérhető");
        }

        public PrincipalContext GetPrincipalContext()
        {
            using (HostingEnvironment.Impersonate())
            {

                //var de = GetDirectoryEntry();
                //var dcName = string.IsNullOrEmpty(ADPort) ? de.Path : de.Path + @":" + ADPort;
                //Log.Debug("AD DC Name: " + dcName);
                //return new PrincipalContext(ContextType.Domain, dcName, ADFelhasznaloNev, ADJelszo);
                return new PrincipalContext(ContextType.Domain, null, ADFelhasznaloNev, ADJelszo);
            }
        }

        public List<GroupPrincipal> GetUserGroupsFromAd(UserPrincipal user)
        {
            var groupPrincipalList = user.GetGroups().Select(x => (GroupPrincipal)x).ToList();
            return groupPrincipalList;
        }

        public UserPrincipal GetUserFromSid(string sid)
        {
            using (var principalcontext = GetPrincipalContext())
            {
                var userPrincipal = UserPrincipal.FindByIdentity(principalcontext, IdentityType.Sid, sid);
                return userPrincipal;
            }
        }

        public UserPrincipal GetUserFromSamAccountName(string samAccountName)
        {
            using (var principalcontext = GetPrincipalContext())
            {
                var userPrincipal = UserPrincipal.FindByIdentity(principalcontext, IdentityType.SamAccountName, samAccountName);
                return userPrincipal;
            }
        }

    }

    public class ActiveDirectoryModel
    {
        public string Path { get; set; }

        public DirectoryEntry Entry { get; set; }
    }
    public interface IAdKezeloFunctions
    {
        bool ValidateUser(string userName, string password, out string passwordHash);
    }
}
