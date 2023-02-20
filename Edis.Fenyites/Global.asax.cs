using Edis.Diagnostics;
using Edis.Entities.Enums;
using Edis.Entities.Fany;
using Edis.Fenyites.App_Start;
using Edis.Fenyites.Hubs;
using Edis.Functions.Base;
using Edis.Functions.Common;
using Edis.Functions.Fany;
using Edis.Functions.JFK;
using Edis.Functions.JFK.FENY;
using Edis.IoC;
using Edis.Repositories.Contexts;
using Edis.ViewModels.Base;
using Edis.ViewModels.JFK.FENY.Email;
using Microsoft.AspNet.SignalR;
using RazorEngine;
using RazorEngine.Templating;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Edis.Fenyites
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static int timeout = 30000;
        private Timer timerFenyitesVegrehajtasok;
        private Timer timerAutomatikusFelfuggesztes;
        private Timer timerAutomatikusJutalomVegrehajtasok;
        //private Timer timerTeszt;
        private readonly object lockObject = new object();
        private static Object _lockObj = new Object();
        private Timer timer;

        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            RegisterAllListTypesModelBinder();
            InjectionConfig.InitializeInjection(typeof(ServiceLocator));
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Teszt Timer beállítása
            //timerTeszt = new Timer(TesztTimerElapsed, null, 30 * 60 * 1000, 30 * 60 * 1000);

            // Timer indítása
            //Log.Info("kezdődik: " + DateTime.Now.ToLongTimeString());
            //Task.Delay(TimeSpan.FromMilliseconds(timeout)).ContinueWith(task => NotifyClient());
            //this.timer = new Timer(Worker, null, 1000, 5 * 60 * 1000); //5 min

            //string timeFenyitesVegrehajtasok = ConfigurationManager.AppSettings["DataUpdateTimeFenyitesVegrehajtasok"];
            //if (timeFenyitesVegrehajtasok != null)
            //{
            //    DateTime now = DateTime.Now;
            //    string[] numbers = timeFenyitesVegrehajtasok.Split(':');
            //    DateTime todayRun = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt32(numbers[0]), Convert.ToInt32(numbers[1]), 0);
            //    int msDelay = 0;
            //    if (now < todayRun)
            //        msDelay = (int)(todayRun - now).TotalMilliseconds;
            //    else
            //        msDelay = (int)(todayRun.AddDays(1) - now).TotalMilliseconds;

            //    timerFenyitesVegrehajtasok = new Timer(UpdateFenyitesVegrehajtasok, null, msDelay, 24 * 60 * 60 * 1000); // 24 óra

            //    Log.Debug("delay start: " + msDelay);
            //    Log.Info("updater set to: " + timeFenyitesVegrehajtasok);
            //}

            //string timeAutomatikusFelfuggesztes = ConfigurationManager.AppSettings["DataUpdateTimeAutomatikusFelfuggesztes"];
            //if (timeAutomatikusFelfuggesztes != null)
            //{
            //    DateTime now = DateTime.Now;
            //    string[] numbers = timeAutomatikusFelfuggesztes.Split(':');
            //    DateTime todayRun = new DateTime(now.Year, now.Month, now.Day, Convert.ToInt32(numbers[0]), Convert.ToInt32(numbers[1]), 0);
            //    int msDelay = 0;
            //    if (now < todayRun)
            //        msDelay = (int)(todayRun - now).TotalMilliseconds;
            //    else
            //        msDelay = (int)(todayRun.AddDays(1) - now).TotalMilliseconds;

            //    timerAutomatikusFelfuggesztes = new Timer(UpdateAutomatikusFelfuggesztes, null, msDelay, 24 * 60 * 60 * 1000); // 24 óra

            //    Log.Debug("delay start AutomatikusFelfuggesztes: " + msDelay);
            //    Log.Info("updater set to AutomatikusFelfuggesztes: " + timeAutomatikusFelfuggesztes);
            //}

            //#if DEBUG
            //        var jutalomFn = new F3JutalomFunctions();
            //        jutalomFn.AutomatikusJutalomLezaras();
            //        Log.Debug("UpdateFenyitesVegrehajtasok AutomatikusJutalomLezaras update kész");
            //        jutalomFn.AutomatikusJutalomVegrehajtas();
            //#endif
        }

        //public void TesztTimerElapsed(object o)
        //{
        //    Log.Info("TesztTimerElapsed, timerAutomatikusFelfuggesztes: " + timerAutomatikusFelfuggesztes != null ? "Él" : "Nem él");
        //    Log.Info("TesztTimerElapsed, timerFenyitesVegrehajtasok: " + timerFenyitesVegrehajtasok != null ? "Él" : "Nem él");
        //}

        //public void UpdateFenyitesVegrehajtasok(object o)
        //{
        //    Log.Info("UpdateFenyitesVegrehajtasok lock obj. előtt");
        //    lock (this.lockObject)
        //    {
        //        Log.Debug("UpdateFenyitesVegrehajtasok start");

        //        var jutalomFn = new F3JutalomFunctions();
        //        jutalomFn.AutomatikusJutalomLezaras();
        //        Log.Debug("UpdateFenyitesVegrehajtasok AutomatikusJutalomLezaras update kész");
        //        jutalomFn.AutomatikusJutalomVegrehajtas();
        //        Log.Debug("UpdateFenyitesVegrehajtasok AutomatikusJutalomVegrehajtas update kész");

        //        FegyelmiUgyFunctions fegyelmiUgyFunctions = new FegyelmiUgyFunctions();

        //        fegyelmiUgyFunctions.UpdateFenyitesVegrahajtvaTipusuUgyek(o);
        //        fegyelmiUgyFunctions.UpdateSzabadultFogvatartottUgyek(o);
                
        //        Log.Debug("StatuszHuszonotEvnelRegebbiUgyek update előtt");
        //        fegyelmiUgyFunctions.UpdateStatuszHuszonotEvnelRegebbiUgyek(o);
        //        Log.Debug("StatuszHuszonotEvnelRegebbiUgyek update kész");

        //        Log.Debug("UpdateFenyitesVegrehajtasok update kész");
        //        List<ElkulonitesEmailData> elkulonitesEmailDatas = fegyelmiUgyFunctions.FegyelmiUgyElkulonitesErtesitoAdatok();
        //        Log.Debug("UpdateFenyitesVegrehajtasok elkulonitesEmailDatas kész");
        //        List<RendezvenyErtesitesEmailData> rendezvenyEmailDatas = fegyelmiUgyFunctions.FegyelmiUgyRendezvenyErtesitesEmailAdatok();
        //        Log.Debug("UpdateFenyitesVegrehajtasok rendezvenyEmailDatas kész");
        //        List<TargyiErtesitesEmailData> targyiKorlatozasEmailDatas = fegyelmiUgyFunctions.FegyelmiUgyTargyiErtesitesEmailAdatok();
        //        Log.Debug("UpdateFenyitesVegrehajtasok targyiKorlatozasEmailDatas kész");
        //        List<TargyiErtesitesEmailData> tobbletszolgaltatasEmailDatas = fegyelmiUgyFunctions.FegyelmiUgyTobbletszolgaltatasEmailAdatok();
        //        Log.Debug("UpdateFenyitesVegrehajtasok tobbletszolgaltatasEmailDatas kész");
        //        List<MaganelzarasFofelugyeloEmailData> maganelzarasFofelugyeloEmailData = fegyelmiUgyFunctions.MaganelzarasFofelugyelokEmailAdatok();
        //        Log.Debug("UpdateFenyitesVegrehajtasok maganelzarasFofelugyeloEmailData kész");
        //        List<VegszallitottFogvatartottEmailData> vegszallitottFogvatartottEmailDatas = fegyelmiUgyFunctions.VegszallitottFogvatartottEmailAdatok();
        //        Log.Debug("UpdateFenyitesVegrehajtasok vegszallitottFogvatartottEmailDatas kész");

        //        EmailFunctions emailFunctions = new EmailFunctions();

        //        var elkulonitesFilename = System.Web.Hosting.HostingEnvironment.MapPath("~/Views/EmailTemplates/ElkulonitesFigyelmeztetesTemplate.cshtml");
        //        string elkulonitesFileContent = File.ReadAllText(elkulonitesFilename);

        //        var rendezvenyFilename = System.Web.Hosting.HostingEnvironment.MapPath("~/Views/EmailTemplates/RendezvenyKorlatozasTemplate.cshtml");
        //        string rendezvenyFileContent = File.ReadAllText(rendezvenyFilename);

        //        var targyiKorlatozasFilename = System.Web.Hosting.HostingEnvironment.MapPath("~/Views/EmailTemplates/TargyiKorlatozasTemplate.cshtml");
        //        string targyiKorlatozasFileContent = File.ReadAllText(targyiKorlatozasFilename);

        //        var tobbletszolgaltatasFilename = System.Web.Hosting.HostingEnvironment.MapPath("~/Views/EmailTemplates/TobbletszolgaltatasTemplate.cshtml");
        //        string tobbletszolgaltatasFileContent = File.ReadAllText(tobbletszolgaltatasFilename);

        //        var maganelzarasFofelugyeloFilename = System.Web.Hosting.HostingEnvironment.MapPath("~/Views/EmailTemplates/MaganelzarasFofelugyeloErtesitesTemplate.cshtml");
        //        string maganelzarasFofelugyeloFileContent = File.ReadAllText(maganelzarasFofelugyeloFilename);

        //        var vegszallitottFogvatartottFilename = System.Web.Hosting.HostingEnvironment.MapPath("~/Views/EmailTemplates/VegszallitottakTemplate.cshtml");
        //        string vegszallitottFogvatartottFileContent = File.ReadAllText(vegszallitottFogvatartottFilename);

        //        string url = ConfigurationManager.AppSettings["AlkalmazasUrl"];

        //        foreach (var email in elkulonitesEmailDatas)
        //        {
        //            email.AlkalmazasUrl = url;
        //            var content = Engine.Razor.RunCompile(elkulonitesFileContent, "elkulonites", null, email);
        //            emailFunctions.SendEmailHTML(email.EmailAddresses, "Elkülönítés értesítés", content);

        //            fegyelmiUgyFunctions.KonasoftBVFonixContext.EmailErtesitesek.Add(new Entities.JFK.FENY.EmailErtesites()
        //            {
        //                ErtesitesTipus = "Elkülönítés értesítés",
        //                Ertesitettek = email.EmailAddresses,
        //                FegyelmiUgyszamok = email.Fegyelmi.Ugyszam,
        //                ErvenyessegKezdete = DateTime.Now,
        //                KeziRogzitoAdatok = true,
        //                LetrehozasDatuma = DateTime.Now,
        //                RogzitoIntezetId = 135,
        //                RogzitoSzemelyId = 12
        //            });

        //            fegyelmiUgyFunctions.KonasoftBVFonixContext.BaseSaveChanges();
        //        }

        //        foreach (var email in rendezvenyEmailDatas)
        //        {
        //            email.AlkalmazasUrl = url;
        //            var content = Engine.Razor.RunCompile(rendezvenyFileContent, "rendezveny", null, email);
        //            emailFunctions.SendEmailHTML(email.EmailAddresses, "Fegyelmi fenyítés végrehajtása", content);
        //            if (email.KorlatozottFegyelmiList.Count > 0)
        //                fegyelmiUgyFunctions.KonasoftBVFonixContext.EmailErtesitesek.Add(new Entities.JFK.FENY.EmailErtesites()
        //                {
        //                    ErtesitesTipus = "Rendezvény eltiltás értesítés",
        //                    Ertesitettek = email.EmailAddresses,
        //                    FegyelmiUgyszamok = string.Join(";", email.KorlatozottFegyelmiList.Select(x => x.Ugyszam)),
        //                    ErvenyessegKezdete = DateTime.Now,
        //                    KeziRogzitoAdatok = true,
        //                    LetrehozasDatuma = DateTime.Now,
        //                    RogzitoIntezetId = 135,
        //                    RogzitoSzemelyId = 12
        //                });
        //            if (email.EngedelyezettFegyelmiList.Count > 0)
        //                fegyelmiUgyFunctions.KonasoftBVFonixContext.EmailErtesitesek.Add(new Entities.JFK.FENY.EmailErtesites()
        //                {
        //                    ErtesitesTipus = "Rendezvény engedélyezés értesítés",
        //                    Ertesitettek = email.EmailAddresses,
        //                    FegyelmiUgyszamok = string.Join(";", email.EngedelyezettFegyelmiList.Select(x => x.Ugyszam)),
        //                    ErvenyessegKezdete = DateTime.Now,
        //                    KeziRogzitoAdatok = true,
        //                    LetrehozasDatuma = DateTime.Now,
        //                    RogzitoIntezetId = 135,
        //                    RogzitoSzemelyId = 12
        //                });

        //            fegyelmiUgyFunctions.KonasoftBVFonixContext.BaseSaveChanges();
        //        }

        //        foreach (var email in targyiKorlatozasEmailDatas)
        //        {
        //            email.AlkalmazasUrl = url;
        //            var content = Engine.Razor.RunCompile(targyiKorlatozasFileContent, "targyi", null, email);
        //            emailFunctions.SendEmailHTML(email.EmailAddresses, "Fegyelmi fenyítés végrehajtása", content);
        //            if (email.KorlatozottFegyelmiList.Count > 0)
        //                fegyelmiUgyFunctions.KonasoftBVFonixContext.EmailErtesitesek.Add(new Entities.JFK.FENY.EmailErtesites()
        //                {
        //                    ErtesitesTipus = "Tárgyi korlátozás értesítés",
        //                    Ertesitettek = email.EmailAddresses,
        //                    FegyelmiUgyszamok = string.Join(";", email.KorlatozottFegyelmiList.Select(x => x.Ugyszam)),
        //                    ErvenyessegKezdete = DateTime.Now,
        //                    KeziRogzitoAdatok = true,
        //                    LetrehozasDatuma = DateTime.Now,
        //                    RogzitoIntezetId = 135,
        //                    RogzitoSzemelyId = 12
        //                });
        //            if (email.EngedelyezettFegyelmiList.Count > 0)
        //                fegyelmiUgyFunctions.KonasoftBVFonixContext.EmailErtesitesek.Add(new Entities.JFK.FENY.EmailErtesites()
        //                {
        //                    ErtesitesTipus = "Tárgyi korlátozás megszüntetés értesítés",
        //                    Ertesitettek = email.EmailAddresses,
        //                    FegyelmiUgyszamok = string.Join(";", email.EngedelyezettFegyelmiList.Select(x => x.Ugyszam)),
        //                    ErvenyessegKezdete = DateTime.Now,
        //                    KeziRogzitoAdatok = true,
        //                    LetrehozasDatuma = DateTime.Now,
        //                    RogzitoIntezetId = 135,
        //                    RogzitoSzemelyId = 12
        //                });

        //            fegyelmiUgyFunctions.KonasoftBVFonixContext.BaseSaveChanges();
        //        }

        //        foreach (var email in tobbletszolgaltatasEmailDatas)
        //        {
        //            email.AlkalmazasUrl = url;
        //            var content = Engine.Razor.RunCompile(tobbletszolgaltatasFileContent, "tobbletszolgaltatas", null, email);
        //            emailFunctions.SendEmailHTML(email.EmailAddresses, "Fegyelmi fenyítés végrehajtása", content);

        //            if (email.KorlatozottFegyelmiList.Count > 0)
        //                fegyelmiUgyFunctions.KonasoftBVFonixContext.EmailErtesitesek.Add(new Entities.JFK.FENY.EmailErtesites()
        //                {
        //                    ErtesitesTipus = "Többletszolgáltatás megvonás értesítés",
        //                    Ertesitettek = email.EmailAddresses,
        //                    FegyelmiUgyszamok = string.Join(";", email.KorlatozottFegyelmiList.Select(x => x.Ugyszam)),
        //                    ErvenyessegKezdete = DateTime.Now,
        //                    KeziRogzitoAdatok = true,
        //                    LetrehozasDatuma = DateTime.Now,
        //                    RogzitoIntezetId = 135,
        //                    RogzitoSzemelyId = 12
        //                });
        //            if (email.EngedelyezettFegyelmiList.Count > 0)
        //                fegyelmiUgyFunctions.KonasoftBVFonixContext.EmailErtesitesek.Add(new Entities.JFK.FENY.EmailErtesites()
        //                {
        //                    ErtesitesTipus = "Többletszolgáltatás engedélyezés értesítés",
        //                    Ertesitettek = email.EmailAddresses,
        //                    FegyelmiUgyszamok = string.Join(";", email.EngedelyezettFegyelmiList.Select(x => x.Ugyszam)),
        //                    ErvenyessegKezdete = DateTime.Now,
        //                    KeziRogzitoAdatok = true,
        //                    LetrehozasDatuma = DateTime.Now,
        //                    RogzitoIntezetId = 135,
        //                    RogzitoSzemelyId = 12
        //                });

        //            fegyelmiUgyFunctions.KonasoftBVFonixContext.BaseSaveChanges();
        //        }

        //        foreach (var email in maganelzarasFofelugyeloEmailData)
        //        {
        //            email.AlkalmazasUrl = url;
        //            var content = Engine.Razor.RunCompile(maganelzarasFofelugyeloFileContent, "maganelzarasFofelugyelo", null, email);
        //            emailFunctions.SendEmailHTML(email.EmailAddresses, "Magánelzárás elrendelés értesítés", content);

        //            fegyelmiUgyFunctions.KonasoftBVFonixContext.EmailErtesitesek.Add(new Entities.JFK.FENY.EmailErtesites()
        //            {
        //                ErtesitesTipus = "Magánelzárás elrendelés értesítés",
        //                Ertesitettek = email.EmailAddresses,
        //                FegyelmiUgyszamok = email.Fegyelmi.Ugyszam,
        //                ErvenyessegKezdete = DateTime.Now,
        //                KeziRogzitoAdatok = true,
        //                LetrehozasDatuma = DateTime.Now,
        //                RogzitoIntezetId = 135,
        //                RogzitoSzemelyId = 12
        //            });
        //            fegyelmiUgyFunctions.KonasoftBVFonixContext.BaseSaveChanges();
        //        }

        //        foreach (var email in vegszallitottFogvatartottEmailDatas)
        //        {
        //            email.AlkalmazasUrl = url;
        //            var content = Engine.Razor.RunCompile(vegszallitottFogvatartottFileContent, "vegszallitott", null, email);
        //            emailFunctions.SendEmailHTML(email.EmailAddresses, "Végszállított fogvatartottak magánelzárásának végrehajtása", content);

        //            fegyelmiUgyFunctions.KonasoftBVFonixContext.EmailErtesitesek.Add(new Entities.JFK.FENY.EmailErtesites()
        //            {
        //                ErtesitesTipus = "Végszállított fogvatartottak magánelzárásának végrehajtása",
        //                Ertesitettek = email.EmailAddresses,
        //                FegyelmiUgyszamok = string.Join(";", email.FegyelmiList.Select(x => x.Ugyszam)),
        //                ErvenyessegKezdete = DateTime.Now,
        //                KeziRogzitoAdatok = true,
        //                LetrehozasDatuma = DateTime.Now,
        //                RogzitoIntezetId = 135,
        //                RogzitoSzemelyId = 12
        //            });

        //            fegyelmiUgyFunctions.KonasoftBVFonixContext.BaseSaveChanges();
        //        }
        //    }
        //}

        //public void UpdateAutomatikusFelfuggesztes(object o)
        //{
        //    Log.Info("AutomatikusFelfuggesztes lock obj. előtt");
        //    lock (this.lockObject)
        //    {
        //        FegyelmiUgyFunctions fegyelmiUgyFunctions = new FegyelmiUgyFunctions();
        //        List<FelfuggesztesEmailData> emailDatas = fegyelmiUgyFunctions.UpdateAutomatikusFelfuggesztesTipusuUgyek(o);
        //        EmailFunctions emailFunctions = new EmailFunctions();

        //        var filename = System.Web.Hosting.HostingEnvironment.MapPath("~/Views/EmailTemplates/UgyfelfuggesztesMegszuntetesTemplate.cshtml");
        //        string FileContent = File.ReadAllText(filename);

        //        string url = ConfigurationManager.AppSettings["AlkalmazasUrl"];

        //        foreach (var email in emailDatas)
        //        {
        //            email.AlkalmazasUrl = url;
        //            var content = Engine.Razor.RunCompile(FileContent, "templateKey", null, email);
        //            try
        //            {
        //                emailFunctions.SendEmailHTML(email.EmailAddresses, "Automatikus fegyelmi eljárás felfüggesztések és megszakítások", content);
        //                Log.Info("Email küldés, címek: " + email.EmailAddresses + "felfüggesztett ügyek: " + email.FelfuggesztettFegyelmiList.Count + "aktívra állított ügyek: " + email.AktivraAllitottFegyelmiList.Count);
        //                if (email.FelfuggesztettFegyelmiList != null && email.FelfuggesztettFegyelmiList.Count > 0)
        //                    fegyelmiUgyFunctions.KonasoftBVFonixContext.EmailErtesitesek.Add(new Entities.JFK.FENY.EmailErtesites()
        //                    {
        //                        ErtesitesTipus = "Fegyelmi ügy felfüggesztés értesítés",
        //                        Ertesitettek = email.EmailAddresses,
        //                        FegyelmiUgyszamok = string.Join(";", email.FelfuggesztettFegyelmiList.Select(x => x.Ugyszam)),
        //                        ErvenyessegKezdete = DateTime.Now,
        //                        KeziRogzitoAdatok = true,
        //                        LetrehozasDatuma = DateTime.Now,
        //                        RogzitoIntezetId = 135,
        //                        RogzitoSzemelyId = 12
        //                    });
        //                if (email.AktivraAllitottFegyelmiList != null && email.AktivraAllitottFegyelmiList.Count > 0)
        //                    fegyelmiUgyFunctions.KonasoftBVFonixContext.EmailErtesitesek.Add(new Entities.JFK.FENY.EmailErtesites()
        //                    {
        //                        ErtesitesTipus = "Fegyelmi ügy felfüggesztés megszüntetés értesítés",
        //                        Ertesitettek = email.EmailAddresses,
        //                        FegyelmiUgyszamok = string.Join(";", email.AktivraAllitottFegyelmiList.Select(x => x.Ugyszam)),
        //                        ErvenyessegKezdete = DateTime.Now,
        //                        KeziRogzitoAdatok = true,
        //                        LetrehozasDatuma = DateTime.Now,
        //                        RogzitoIntezetId = 135,
        //                        RogzitoSzemelyId = 12
        //                    });

        //                fegyelmiUgyFunctions.KonasoftBVFonixContext.BaseSaveChanges();
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Error("Email küldés hiba, címek: " + email.EmailAddresses, ex);
        //            }
        //        }
        //    }
        //}


        private void RegisterAllListTypesModelBinder()
        {
            ModelBinders.Binders.Add(typeof(List<sbyte>), new ListModelBinder());
            ModelBinders.Binders.Add(typeof(List<short>), new ListModelBinder());
            ModelBinders.Binders.Add(typeof(List<int>), new ListModelBinder());
            ModelBinders.Binders.Add(typeof(List<long>), new ListModelBinder());
            ModelBinders.Binders.Add(typeof(List<byte>), new ListModelBinder());
            ModelBinders.Binders.Add(typeof(List<ushort>), new ListModelBinder());
            ModelBinders.Binders.Add(typeof(List<uint>), new ListModelBinder());
            ModelBinders.Binders.Add(typeof(List<ulong>), new ListModelBinder());
            ModelBinders.Binders.Add(typeof(List<char>), new ListModelBinder());
            ModelBinders.Binders.Add(typeof(List<float>), new ListModelBinder());
            ModelBinders.Binders.Add(typeof(List<double>), new ListModelBinder());
            ModelBinders.Binders.Add(typeof(List<decimal>), new ListModelBinder());
            ModelBinders.Binders.Add(typeof(List<bool>), new ListModelBinder());
            ModelBinders.Binders.Add(typeof(List<string>), new ListModelBinder());
        }

        private void NotifyClient()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SystemEventsHub>();
            hubContext.Clients.All.refreshFenyitesekListAfterServerRR();
            Log.Info("lefutottam: " + DateTime.Now.ToLongTimeString());
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Log.Debug("Session start");

            IJogosultsagKezeloFunctions jogosultsagFunctions = InjectionKernel.Instance.GetInstance<IJogosultsagKezeloFunctions>();
            IJogosultsagCacheFunctions jogosultsagCacheFunctions = InjectionKernel.Instance.GetInstance<IJogosultsagCacheFunctions>();
            IIntezetFunctions intezetFunctions = InjectionKernel.Instance.GetInstance<IIntezetFunctions>();

            IAlkalmazasKontextusFunctions appsettingsFunctions = InjectionKernel.Instance.GetInstance<IAlkalmazasKontextusFunctions>();
            var kontextus = new AlkalmazasKontextus();
            kontextus.SessionID = HttpContext.Current.Session.SessionID;
            appsettingsFunctions.Kontextus = kontextus;

            //if (false) //DnsReady
            //{
            //    System.Net.IPHostEntry ip = System.Net.Dns.GetHostEntry(Request.UserHostAddress);

            //    appsettingsFunctions.Kontextus.ClientHostName = ip.HostName;
            //}
            //else
            //{
            //    appsettingsFunctions.Kontextus.ClientHostName = "Dns ellenőrzés kikapcsolva";
            //}
            var windowsIdentity = WindowsIdentity.GetCurrent();

            //IPersonalHelpdeskHelperFunctions personalHelpdeskHelperFunctions = InjectionKernel.Instance.GetInstance<IPersonalHelpdeskHelperFunctions>();
            //appsettingsFunctions.Kontextus.PersonalHelpdeskRSA = personalHelpdeskHelperFunctions.GetLoginRSAData(windowsIdentity, jogosultsagCacheFunctions.JogosultIntezetek?.FirstOrDefault()?.Nev, jogosultsagCacheFunctions.JogosultIntezetek?.FirstOrDefault()?.Azonosito2);
            this.Session["PersonalHelpdeskRSA"] = appsettingsFunctions.Kontextus.PersonalHelpdeskRSA;

//#if DEBUG
//            if (!jogosultsagFunctions.IsDebugPermissions(windowsIdentity.User.Value))
//            {

//                Log.Error($"Debug futtatása jog nélkül: {windowsIdentity.Name} - {windowsIdentity.User.Value}");
//                Server.ClearError();
//                Response.Clear();
//                Response.Redirect("~/Error/NoPermission");
//                return;
//            }
//#endif

            Log.Info(string.Format("Global.asax sid.Name: {0}, sid: {1}", windowsIdentity.Name, windowsIdentity.User.Value));
            jogosultsagFunctions.LehetsegesIntezetekBeallitasaCsakActiveDirectoryOlvasassal(windowsIdentity);
            int id = 0;
            try
            {

                if (jogosultsagCacheFunctions.JogosultIntezetek.Count == 0)
                {
                    jogosultsagFunctions.JogosultsagNelkuliIntezetBeallitas(windowsIdentity);
                    id = jogosultsagCacheFunctions.JogosultIntezetek.First().Id;
                }
                else
                {
                    string cookieName = "fegyelmiIntezetId";

#if DEBUG
                    jogosultsagCacheFunctions.JogosultIntezetek = intezetFunctions.Table.ToList();
                    id = 115;

                    Intezet intezet = null;

                    // ha van intézet cookie beállítva
                    if (Request.Cookies[cookieName] != null)
                    {
                        var intezetId = Request.Cookies[cookieName].Value;
                        intezet = jogosultsagCacheFunctions.JogosultIntezetek.SingleOrDefault(x => x.Id.ToString() == intezetId);
                        // ha még jogosult a cookieban lévő intézethez
                        if (intezet != null)
                        {
                            id = intezet.Id;
                        }
                    }
                    // ha nincs cookie vagy nem jogosult a cookieban tárolt intézethez
                    if (Request.Cookies[cookieName] == null || intezet == null)
                    {
                        var cookie = new HttpCookie(cookieName, id.ToString());
                        Response.SetCookie(cookie);
                    }

#else
                    bool teljesMegtekintoJogotKap = false;

                    var userjogosultsagok = jogosultsagCacheFunctions.UserJogosultsagok;

                    if (jogosultsagCacheFunctions.JogosultIntezetek.Any(x => x.Id == 135))
                    {
                        List<string> fegyelmiJogok = new List<string>() { Jogosultsagok.Fegyelmi_egyeb_szakterulet.ToString().ToLower(),
                                                                        Jogosultsagok.Fegyelmi_jogkor_gyakorloja.ToString().ToLower(),
                                                                        Jogosultsagok.Fegyelmi_reintegracios_tiszt.ToString().ToLower(),
                                                                        Jogosultsagok.Jfk_fegyjutmegtekinto.ToString().ToLower()
                                                                      };

                        // bvop szerkesztő jog kikapcsolás
                        foreach (var jog in fegyelmiJogok)
                        {
                            if (userjogosultsagok.ContainsKey(jog))
                            {
                                if (userjogosultsagok[jog].Contains(135))
                                {
                                    userjogosultsagok[jog] = new HashSet<int>(userjogosultsagok[jog].Where(x => x != 135));
                                    teljesMegtekintoJogotKap = true;
                                }
                            }
                        }
                    }

                    // teljes intézeti megtekintő jog bekapcsolás
                    if (teljesMegtekintoJogotKap)
                    {
                        jogosultsagCacheFunctions.JogosultIntezetek = intezetFunctions.Table.ToList();
                        userjogosultsagok[Jogosultsagok.Jfk_fegyjutmegtekinto.ToString().ToLower()] = new HashSet<int>(jogosultsagCacheFunctions.JogosultIntezetek.Select(x => x.Id).ToList());

                        id = 135;
                    }
                    else
                    {
                        id = jogosultsagCacheFunctions.JogosultIntezetek.First().Id;
                    }

                    jogosultsagCacheFunctions.UserJogosultsagok = userjogosultsagok;

                    Intezet intezet = null;

                    // ha van intézet cookie beállítva
                    if (Request.Cookies[cookieName] != null)
                    {
                        var intezetId = Request.Cookies[cookieName].Value;
                        intezet = jogosultsagCacheFunctions.JogosultIntezetek.SingleOrDefault(x => x.Id.ToString() == intezetId);
                        // ha még jogosult a cookieban lévő intézethez
                        if (intezet != null)
                        {
                            id = intezet.Id;
                        }
                    }

                    var cookie = new HttpCookie(cookieName, id.ToString());
                    Response.SetCookie(cookie);


#endif

                }
                lock (_lockObj)
                {
                    jogosultsagFunctions.BelepesCsakActiveDirectoryOlvasassal(windowsIdentity, string.Empty, id);
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            ISzemelyzetFunctions szemelyzetfunc = InjectionKernel.Instance.GetInstance<ISzemelyzetFunctions>();

            var szemelyzet = szemelyzetfunc.FindById(appsettingsFunctions.Kontextus.SzemelyzetId);

            Log.Info(string.Format("Global.asax szemelyzetfunc name: {0}", szemelyzet.Nev));

            Session["szemelyzetNev"] = szemelyzet.Nev;

            IActiveDirectoryFunctions ActiveDirectoryFunctions = InjectionKernel.Instance.GetInstance<IActiveDirectoryFunctions>();

            this.Session["szemelyzetBeosztas"] = ActiveDirectoryFunctions.KeresBeosztas(szemelyzet.AdSid);

            IActiveDirectoryKezeloFunctions activeDirectoryKezeloFunctions = InjectionKernel.Instance.GetInstance<IActiveDirectoryKezeloFunctions>();

            IActiveDirectoryFunctions activeDirectoryFunctions = InjectionKernel.Instance.GetInstance<IActiveDirectoryFunctions>();


            var noemi2020url = System.Configuration.ConfigurationManager.AppSettings["Noemi2020url"];

            if (string.IsNullOrWhiteSpace(noemi2020url))
            {
                Log.Debug("exotoolbarUrl is missing or empty!");
                return;
            }

#if DEBUG
            Log.Debug("Debug mode, no exotoolbarUrl!");
            return;
#endif

            string sid = windowsIdentity.User.Value;
            string email;
            string adUser;
            string szemelyzetNev;

            try
            {
                email = activeDirectoryKezeloFunctions.KeresEmailcim(windowsIdentity.User.Value);
            }
            catch
            {
                email = "";
            }

            try
            {
                adUser = windowsIdentity.Name;
            }
            catch
            {
                adUser = "";
            }

            try
            {
                szemelyzetNev = activeDirectoryFunctions.KeresDisplayName(sid);
            }
            catch
            {
                szemelyzetNev = "";
            }

            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            if (ConfigurationManager.AppSettings["TrackRequestLog"].ToLower() != "true")
                return;

            try
            {
                MiniProfiler.Start();
            }
            catch (Exception)
            {
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            KonasoftBVFonixContext.DeleteContext();

            if (ConfigurationManager.AppSettings["TrackRequestLog"].ToLower() != "true")
                return;

            try
            {
                var startTime = MiniProfiler.Current.Started.AddHours(1);
                MiniProfiler.Stop();
                Log.Info($"EndRequest - {MiniProfiler.Current.Root.Name} - {(DateTime.Now - startTime).TotalMilliseconds} ms");
            }
            catch (Exception)
            {
            }
        }

    }
}
