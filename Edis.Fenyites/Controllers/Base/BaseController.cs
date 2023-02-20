using System;
using System.Web;
using System.Web.Mvc;
using Edis.Functions.Common;
using Edis.IoC;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using FunctionsBase = Edis.Functions.Base;
namespace Edis.Fenyites.Controllers.Base
{
    using Edis.Entities.Enums;
    using Edis.Entities.Enums.Cimke.Fonix3;
    using Edis.Functions.Fany;
    using Edis.Functions.JFK;
    using Edis.IoC.Attributes;
    using Edis.ViewModels.Common;
    using Edis.ViewModels.Fany;
    using Edis.ViewModels.JFK.FENY;
    using System.Configuration;
    using static Edis.Entities.Enums.Cimke.CimkeEnums;

    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class BaseController : Controller
    {
        public static UserData GetUserData()
        {
            var kontext = new AlkalmazasKontextusFunctions().Kontextus;
            var szemelyzetfunc = new SzemelyzetFunctions();
            var fegyelmiUgyFunctions = new FegyelmiUgyFunctions();
            var szemelyzet = szemelyzetfunc.FindById(kontext.SzemelyzetId);
            var jogosultsagCacheFunctions = new JogosultsagCacheFunctions();
            var jogosultIntezetek = jogosultsagCacheFunctions.JogosultIntezetek;
            var intezet = jogosultIntezetek.SingleOrDefault(f => f.Id == kontext.RogzitoIntezetId);

            //var archivUgyekEvei = fegyelmiUgyFunctions.GetArchivEvek(kontext.RogzitoIntezetId);
            var archivUgyekEvei = new List<int>();
            for (int i = 1995; i <= DateTime.Now.Year; i++)
            {
                archivUgyekEvei.Add(i);
            }
            archivUgyekEvei.Reverse();
            List<IdNevParos> adatbazisok = new List<IdNevParos>();
            IJogosultsagKezeloFunctions jogosultsagFunctions = InjectionKernel.Instance.GetInstance<IJogosultsagKezeloFunctions>();

            var userData = new UserData()
            {
                Id = szemelyzet.Id,
                RogzitoIntezet = (IntezetModel)intezet,
                SzemelyzetNev = szemelyzet.Nev,
                SzemelyzetSid = kontext.SzemelyzetSid,
                ArchivUgyekEvei = archivUgyekEvei,
                ValaszthatoIntezetek = jogosultIntezetek.Select(s => (IntezetModel)s).ToList()
            };

            var jogkorJson = new IdNevParos();
            //var parancsnok = new F3CimkeFunctions().GetFonix2CimkeByCimkeId((int)JutalmazasJogkorok.Parancsnok);
            //var osztalyvezeto = new F3CimkeFunctions().GetFonix2CimkeByCimkeId((int)JutalmazasJogkorok.Osztalyvezeto);
            //var reint = new F3CimkeFunctions().GetFonix2CimkeByCimkeId((int)JutalmazasJogkorok.ReintegraciosTiszt);
            if (jogosultsagFunctions.VanJogosultsaga(Jogosultsagok.Fegyelmi_jogkor_gyakorloja))
                if ((kontext.F3SzakteruletIds?.Contains((int)F3SzakteruletTipusa.Parancsnok)??false)
                    || (kontext.F3SzakteruletIds?.Contains((int)F3SzakteruletTipusa.ParancsnokHelyettes)??false))
                {
                    jogkorJson = new IdNevParos()
                    {
                        Id = (int)JutalmazasJogkorok.Parancsnok,
                        Nev = JutalmazasJogkorok.Parancsnok.ToString()
                    };
                }
                else
                {
                    jogkorJson = new IdNevParos()
                    {
                        Id = (int)JutalmazasJogkorok.Osztalyvezeto,
                        Nev = JutalmazasJogkorok.Osztalyvezeto.ToString()
                    };
                }
            else if (jogosultsagFunctions.VanJogosultsaga(Jogosultsagok.Fegyelmi_reintegracios_tiszt))
            {
                jogkorJson = new IdNevParos()
                {
                    Id = (int)JutalmazasJogkorok.ReintegraciosTiszt,
                    Nev = JutalmazasJogkorok.ReintegraciosTiszt.ToString()
                };
            }

            userData.Jogkor = jogkorJson;
            //userData.Szakterulet = szakteruletJson;

#if DEBUG
            userData.Jogosultsagok = new List<string>() { Jogosultsagok.Jfk_fegyjutmegtekinto.ToString().ToLower(),
                                                          Jogosultsagok.Fegyelmi_egyeb_szakterulet.ToString().ToLower(),
                                                          Jogosultsagok.Fegyelmi_jogkor_gyakorloja.ToString().ToLower(),
                                                          Jogosultsagok.Fegyelmi_fofelugyelo.ToString().ToLower(),
                                                          Jogosultsagok.Fegyelmi_reintegracios_tiszt.ToString().ToLower()};
#else
             userData.Jogosultsagok = jogosultsagCacheFunctions.UserJogosultsagok.Where(x => x.Value.Contains(kontext.RogzitoIntezetId)).Select(x => x.Key).ToList();
#endif
            if (!string.IsNullOrWhiteSpace(kontext.PersonalHelpdeskRSA))
            {
                userData.PersonalHelpdeskLoginUrl = ConfigurationManager.AppSettings["PersonalHelpdeskUrl"] +
                   HttpUtility.UrlEncode(kontext.PersonalHelpdeskRSA);
            }
            return userData;
        }
        public static string SerializeToJavascriptObject(object o)
        {
            var s = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 5 };
            return s.Serialize(o);
        }

        public new JsonResult Json(object data, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            //var result = base.Json(data, behavior);
            //result.MaxJsonLength = int.MaxValue;
            //return result;

            var result = new JsonNetResult(data);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        public void ThrowValidationExceptionIfNotValid()
        {
            if (ModelState.IsValid)
                return;

            var keyList = ModelState.Keys.ToList();
            var errorList = ModelState.Values.ToList();

            FunctionsBase.ValidationException ex = new FunctionsBase.ValidationException();
            for (int i = 0; i < ModelState.Keys.Count; i++)
            {
                if (errorList[i].Errors.Count > 0)
                {
                    ex.Errors.Add(new Functions.Base.ValidationItem()
                    {
                        Id = keyList[i],
                        Text = errorList[i].Errors.First().ErrorMessage
                    });
                }
            }
            throw ex;
        }

        public static bool IsDebugBuild
        {
            get
            {
#if DEBUG
                return true;
#endif
#if !DEBUG
                return false;
#endif

            }
        }

        public static bool IsTesztRendszer
        {
            get
            {
                var conn = ConfigurationManager.ConnectionStrings["KonasoftBVFonixContext"];
                string s = string.Empty;
                bool debug = IsDebugBuild;

                if (conn != null)
                {
                    s = conn.ConnectionString;
                }

                foreach (var item in s.ToLower().Split(';'))
                {
                    var keyValuePair = item.Replace(" ", "").Split('=');
                    if (keyValuePair.Length == 2)
                    {
                        if (keyValuePair[0] == "Initial Catalog".ToLower().Replace(" ", "") && keyValuePair[1].ToLower().Contains("_teszt"))
                        {
                            return true;
                        }
                    }
                }

                if (debug)
                {
                    return true;
                }

                return false;
            }
        }

        static string systemBuildAndTestTitle;

        public static string SystemBuildAndTestTitle
        {
            get
            {
                if (systemBuildAndTestTitle != null)
                    return systemBuildAndTestTitle;

                var conn = ConfigurationManager.ConnectionStrings["KonasoftBVFonixContext"];
                string s = "";
                if (conn != null)
                {
                    s = conn.ConnectionString;
                }
                bool tesztrendszer = false;
                bool debug = IsDebugBuild;

                foreach (var item in s.ToLower().Split(';'))
                {
                    var keyValuePair = item.Replace(" ", "").Split('=');
                    if (keyValuePair.Length == 2)
                    {
                        if (keyValuePair[0] == "Initial Catalog".ToLower().Replace(" ", "") && keyValuePair[1].ToLower().Contains("_teszt"))
                        {
                            tesztrendszer = true;
                        }
                    }
                }
                if (tesztrendszer)
                {
                    if (debug)
                        systemBuildAndTestTitle = "Tesztrendszer (fejlesztői)"; // teszt DB debug
                    else
                        systemBuildAndTestTitle = "Tesztrendszer"; // teszt DB release
                }
                else
                {
                    if (debug)
                        systemBuildAndTestTitle = "Fejlesztői tesztrendszer"; // éles DB debug
                    else
                        systemBuildAndTestTitle = ""; // éles DB release
                }

                return systemBuildAndTestTitle;


            }
        }



    }
}