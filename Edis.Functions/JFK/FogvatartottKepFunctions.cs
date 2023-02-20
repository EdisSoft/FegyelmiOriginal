using Edis.Repositories.Contexts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Edis.Functions.JFK
{
    public class FogvatartottKepFunctions
    {
        const int LEGNAGYOBBKEPMERET = 100;
        public const string CONFIGKEY_KEPMAPPA = "KepekMentesiHelye";
        private KonasoftBVFonixContext context { get; set; } = new KonasoftBVFonixContext();

       
        public string GenerateFogvatartottKep(int fogvatartottId)
        {
            var fogvatartott = context.FogvatartottakNezet.Include("FogvatartottFenykepek").FirstOrDefault(x => x.Id == fogvatartottId);
            var kep = fogvatartott.FogvatartottFenykepek.OrderBy(x => x.ErvenyessegKezdete).LastOrDefault();
            if (kep != null)
            {
                var relativeDir = ConfigurationManager.AppSettings[CONFIGKEY_KEPMAPPA];
                if (!relativeDir.EndsWith("/"))
                {
                    relativeDir += "/";
                }

                var absoluteDir = System.Web.Hosting.HostingEnvironment.MapPath(relativeDir);
                if (!Directory.Exists(absoluteDir))
                {
                    Directory.CreateDirectory(absoluteDir);
                }
                var fileName = $"{fogvatartott.FogvSzemelyId}_{kep.ErvenyessegKezdete.ToString("yyyyMMdd")}.jpg";

                #region Régi képek törlése
                string[] files = System.IO.Directory.GetFiles(absoluteDir, $"{fogvatartott.FogvSzemelyId}_*.jpg");
                foreach (string s in files)
                {
                    File.Delete(s);
                }
                #endregion

                #region Új kép létrehozása

                File.WriteAllBytes(absoluteDir + fileName, kep.Kisindexkep100);

                return relativeDir + fileName;
                
                #endregion
            }
            return null;
        }

        public string GetFogvatartottKepUrl(int szemelyId, DateTime utolsoKepDatum)
        {
            bool result = false;
            
                var relativeDir = ConfigurationManager.AppSettings[CONFIGKEY_KEPMAPPA];
                if (!relativeDir.EndsWith("/"))
                {
                    relativeDir += "/";
                }

                var absoluteDir = System.Web.Hosting.HostingEnvironment.MapPath(relativeDir);
                var fileName = $"{szemelyId}_{utolsoKepDatum.ToString("yyyyMMdd")}.jpg";
                if(File.Exists(absoluteDir + fileName))
                {
                return relativeDir + fileName;
                }

            return null;
        }
    }
}
