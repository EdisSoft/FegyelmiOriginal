using Edis.Entities.Fany;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    public class GroupPrincipalSeged
    {
        #region statikus eljárások
        public static int Globalis(GroupPrincipal groupPrincipal)
        {
            return Globalis(groupPrincipal.Name);
        }

        public static int Globalis(string nev)
        {
            return nev.Substring(5).ToUpper().StartsWith("GLOBALIS-") || nev.Substring(5).ToUpper().StartsWith("FNO-") ? 1 : 0;
        }

        public static string GetGroupPrincipalNev(GroupPrincipal groupPrincipal)
        {
#if !DEBUG
                return groupPrincipal.Name;
#else
            return groupPrincipal is DebugGroupPrincipal ? ((DebugGroupPrincipal)groupPrincipal).Name : groupPrincipal.Name;
#endif
        }

        public static string IntezetAzonosito2(GroupPrincipal groupPrincipal)
        {
            var groupPrincipalNev = GetGroupPrincipalNev(groupPrincipal);
            return groupPrincipalNev.Substring(0, 4);
        }

        public static string SzerepkorNeve(GroupPrincipal groupPrincipal)
        {
            string groupPrincipalNev = GetGroupPrincipalNev(groupPrincipal);
            return Globalis(groupPrincipalNev) == 1
                           ? groupPrincipalNev.Substring(9)
                           : groupPrincipalNev.Substring(8);
        }

        public static bool IsFonixGroup(GroupPrincipal groupPrincipal)
        {
            var groupPrincipalNev = GetGroupPrincipalNev(groupPrincipal);
            //if (groupPrincipalNev == null)
            //    groupPrincipalNev = "";
            var fonixCsoportJelolo = "-FN-";
            var jeloloKezdete = groupPrincipalNev.IndexOf(fonixCsoportJelolo, StringComparison.InvariantCultureIgnoreCase);
            if (jeloloKezdete == -1)
            {
                fonixCsoportJelolo = "-FNO-";
                jeloloKezdete = groupPrincipalNev.IndexOf(fonixCsoportJelolo, StringComparison.InvariantCultureIgnoreCase);
            }
            if (jeloloKezdete == -1 || groupPrincipalNev.Length < 9)
                return false;

            return true;

            /*
            var jeloloHossza = fonixCsoportJelolo.Length;
            var intezetAzonosito = groupPrincipalNev.Substring(0, jeloloKezdete);
            var intezetAzonositoHossza = intezetAzonosito.Length;

            // Ha az XXXX-FNO-XXXX- már nem fér a hosszba, akkor nem lehet intézeti szerpkör, tehát ez központi szerepkőr, további szűrés nem kell.
            if (groupPrincipalNev.Length < 2 * intezetAzonositoHossza + jeloloHossza + 1)
                return true;

            var intezetAzonosito2 = groupPrincipalNev.Substring(intezetAzonositoHossza + jeloloHossza, intezetAzonositoHossza + 1);
            return intezetAzonosito2.Last() != '-' 
                   || intezetAzonosito2.Length < intezetAzonositoHossza
                   || intezetAzonosito == intezetAzonosito2.Substring(0, intezetAzonositoHossza);
             */
        }

        #endregion statikus eljárások


    }
}
