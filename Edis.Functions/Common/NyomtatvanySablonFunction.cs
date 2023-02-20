using Edis.Entities.Enums;
using Edis.Entities.Enums.Cimke;
using Edis.Entities.Enums.Kodszotar;
using Edis.Entities.Fany;
using Edis.Functions.Base;
using Edis.Functions.Fany;
using Edis.Functions.JFK;
using Edis.IoC.Attributes;
using Edis.Repositories.Contexts;
using Edis.Utilities;
using Edis.ViewModels.Fany;
using Novacode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using static Edis.Entities.Enums.Kodszotar.KodszotarEnums;
using Calendar = System.Globalization.Calendar;
using Edis.Entities.Enums.Cimke.Aktivitas;
using Newtonsoft.Json.Linq;
using Edis.ViewModels.Common;
using Log = Edis.Diagnostics.Log;
using System.Configuration;
using static Edis.Entities.Enums.Cimke.CimkeEnums;
using Edis.ViewModels.JFK.FENY;
using System.Web.Helpers;
using Edis.Entities.Enums.Cimke.Fonix3;
using System.Text.RegularExpressions;

namespace Edis.Functions.Common
{

    public partial class NyomtatvanySablonFunction : NyomtatvanySablonFunctionBase, INyomtatvanySablonFunction
    {
      



        private static void SzoCsere(Dictionary<string, string> cserelendoErtekek, List<DocWordReplace> cserelendoSzavak)
        {
            var keys = cserelendoErtekek.Keys.ToList();

            foreach (var key in keys)
            {
                if (key.Contains("_"))
                {
                    cserelendoErtekek[key.Replace("_", "")] = cserelendoErtekek[key];
                }
            }

            foreach (var item in cserelendoErtekek)
            {
                DocWordReplace replace = new DocWordReplace();
                replace.RegiErtek = "%" + item.Key + "%";
                replace.UjErtek = item.Value == null ? "" : item.Value;
                cserelendoSzavak.Add(replace);
            }
        }

    }
}

