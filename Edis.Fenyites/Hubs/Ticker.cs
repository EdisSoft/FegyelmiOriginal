using Edis.Diagnostics;
using Edis.Entities.Enums;
using Edis.Fenyites.Controllers.Base;
using Edis.Functions.Fany;
using Edis.Functions.JFK;
using Edis.Functions.JFK.FENY;
using Edis.IoC.Attributes;
using Edis.ViewModels.JFK.FENY;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using static Edis.Fenyites.Hubs.SystemEventsHub;

namespace Edis.Fenyites.Hubs
{
    [TrackTimeFilter]
    public class Ticker
    {

        public ConcurrentDictionary<int, FenyitesFegyelmiUgyekValtozasokList> FegyelmiUgyekValtozasokIdsFany = new ConcurrentDictionary<int, FenyitesFegyelmiUgyekValtozasokList>();
        public ConcurrentDictionary<int, FenyitesFegyelmiUgyekValtozasokList> FegyelmiUgyekValtozasokIds = new ConcurrentDictionary<int, FenyitesFegyelmiUgyekValtozasokList>();
       
        private readonly static Lazy<Ticker> _instance = new Lazy<Ticker>(
            () => new Ticker(GlobalHost.ConnectionManager.GetHubContext<SystemEventsHub>().Clients));
        private Ticker(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
            FenyitesDashboardFunctions.OnFegyelmiUgyValtozasFany += OnFegyelmiUgyValtozasFany;
            FegyelmiUgyFunctions.OnFegyelmiUgyValtozas += OnFegyelmiUgyValtozas;
            EsemenyekFunctions.OnEsemenyValtozas += OnFegyelmiUgyValtozas;
            //F3JutalomFunctions.OnJutalomUgyValtozas += OnJutalomUgyValtozas;
        }

        public static Ticker Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        private void OnFegyelmiUgyValtozasFany(List<int> ujUgyIdList, List<int> valtozottUgyIdList, List<int> megszuntUgyIdList)
        {
            var success = NotifyUseresOnFegyelmiUgyValtozasFany(ujUgyIdList, valtozottUgyIdList, megszuntUgyIdList);
        }

        private void OnFegyelmiUgyValtozas(List<int> ujUgyIdList, List<int> valtozottUgyIdList, List<int> megszuntUgyIdList)
        {

            Task.Run(() =>
            {
                NotifyUseresOnFegyelmiUgyValtozas(ujUgyIdList, valtozottUgyIdList, megszuntUgyIdList);
            }).ConfigureAwait(true);

        }

        //private void OnJutalomUgyValtozas(List<int> ujJutalomUgyIdList, List<int> valtozottJutalomUgyIdList, List<int> megszuntJutalomUgyIdList)
        //{

        //    Task.Run(() =>
        //    {
        //        NotifyUseresOnJutalomUgyValtozas(ujJutalomUgyIdList, valtozottJutalomUgyIdList, megszuntJutalomUgyIdList);
        //    }).ConfigureAwait(true);

        //}

        private async Task<bool> NotifyUseresOnFegyelmiUgyValtozasFany(List<int> ujUgyIdList, List<int> valtozottUgyIdList, List<int> megszuntUgyIdList)
        {
            //Log.Info("NotifyUseresOnFegyelmiUgyValtozas started");            
            //Log.Info($"Új fegyelmi ügyek:{ujUgyIdLiSst.Count() }, Fegyelmi ügy változások: { valtozottUgyIdList.Count() }, Megszűnt fegyelmi ügyek: {megszuntUgyIdList.Count()}");
            if (ujUgyIdList.Count > 0 || valtozottUgyIdList.Count > 0)
            {
                List<int> fegyelmiUgyIds = new List<int>();
                fegyelmiUgyIds.AddRange(ujUgyIdList);
                fegyelmiUgyIds.AddRange(valtozottUgyIdList);
                await Clients.All.fegyelmiUgyValtozasFany(fegyelmiUgyIds);

            }
            if (megszuntUgyIdList.Count > 0)
            {
                await Clients.All.fegyelmiUgyMegszunesFany(megszuntUgyIdList);
            }
            lock (FegyelmiUgyekValtozasokIdsFany)
            {
                foreach (var item in ujUgyIdList)
                {
                    FenyitesFegyelmiUgyekValtozasokList ujFegyelmiUgyek = new FenyitesFegyelmiUgyekValtozasokList();
                    ujFegyelmiUgyek.ModositasIdeje = DateTime.Now;
                    ujFegyelmiUgyek.ModositasTipusa = (int)FenyitesFegyelmiUgyekTipusai.UjFegyelmiUgy;
                    FegyelmiUgyekValtozasokIdsFany.AddOrUpdate(item, ujFegyelmiUgyek, (k, v) => ujFegyelmiUgyek);
                }
                foreach (var item in valtozottUgyIdList)
                {
                    FenyitesFegyelmiUgyekValtozasokList valtozottFegyelmiUgyek = new FenyitesFegyelmiUgyekValtozasokList();
                    valtozottFegyelmiUgyek.ModositasIdeje = DateTime.Now;
                    valtozottFegyelmiUgyek.ModositasTipusa = (int)FenyitesFegyelmiUgyekTipusai.MegvaltoztottFegyelmiUgy;
                    FegyelmiUgyekValtozasokIdsFany.AddOrUpdate(item, valtozottFegyelmiUgyek, (k, v) => valtozottFegyelmiUgyek);
                }
                foreach (var item in megszuntUgyIdList)
                {
                    FenyitesFegyelmiUgyekValtozasokList megszuntFegyelmiUgyek = new FenyitesFegyelmiUgyekValtozasokList();
                    megszuntFegyelmiUgyek.ModositasIdeje = DateTime.Now;
                    megszuntFegyelmiUgyek.ModositasTipusa = (int)FenyitesFegyelmiUgyekTipusai.MegszuntFegyelmiUgy;
                    FegyelmiUgyekValtozasokIdsFany.AddOrUpdate(item, megszuntFegyelmiUgyek, (k, v) => megszuntFegyelmiUgyek);
                }

            }
            var elmult30perc = DateTime.Now.AddMinutes(-30);
            List<int> torlendoFegyelmiUgyIds = new List<int>();
            torlendoFegyelmiUgyIds = FegyelmiUgyekValtozasokIdsFany.Where(w => w.Value.ModositasIdeje < elmult30perc).Select(s => s.Key).ToList();
            foreach (var torlendoUgyId in torlendoFegyelmiUgyIds)
            {
                lock (FegyelmiUgyekValtozasokIdsFany)
                {
                    FegyelmiUgyekValtozasokIdsFany.TryRemove(torlendoUgyId, out _);
                }
            }


            //Log.Info("NotifyUseresOnFegyelmiUgyValtozas ended");
            return true;
        }
        private async Task<bool> NotifyUseresOnFegyelmiUgyValtozas(List<int> ujUgyIdList, List<int> valtozottUgyIdList, List<int> megszuntUgyIdList)
        {                     
            try
            {
                
                if (ujUgyIdList.Count > 0 || valtozottUgyIdList.Count > 0)
                {
                    List<int> fegyelmiUgyIds = new List<int>();
                    fegyelmiUgyIds.AddRange(ujUgyIdList);
                    fegyelmiUgyIds.AddRange(valtozottUgyIdList);
                    List<FegyelmiUgyListItemViewModel> fegyelmiUgyek = new List<FegyelmiUgyListItemViewModel>();
                    using (FegyelmiUgyFunctions fegyelmiUgyFunctions = new FegyelmiUgyFunctions())
                    {
                        fegyelmiUgyek = fegyelmiUgyFunctions.GetFegyelmiUgyekByIds(fegyelmiUgyIds);
                    }
                    if(fegyelmiUgyek.Count == 0 || fegyelmiUgyek == null)
                    {
                        StringBuilder builder = new StringBuilder();
                        foreach (var fegyelmiUgy in fegyelmiUgyIds)
                        {
                            builder.Append(fegyelmiUgy).Append(',');
                        }
                        string fegyelmiUgyStrings = builder.ToString();
                        Log.Error($"Értesítés közben a GetFegyelmiUgyekByIds nulla fegyelmi ügyet adott vissza. Változtatásra külödtt fegyelmi ügyek idk: {fegyelmiUgyStrings}");
                    }
                    var users = _connections.GetConnections().SelectMany(s => s.Value).ToList();                    
                    foreach (var user in users)
                    {                        
                        var userFegyelmiUgyei = fegyelmiUgyek.Where(w => w.FegyelmiIntezetId == user.IntezetId || w.FogvatartottAktualisBvIntezet == user.IntezetId || user.IntezetId == 135).ToList();                          
                        var aktivitasFolyamList = NotifyUserAktivitasPanelFrissites(fegyelmiUgyIds);
                        var aktAktivitasFolyamList = aktivitasFolyamList.Where(w => w.FegyelmiIntezetId == user.IntezetId || w.FogvatartottAktualisBvIntezet == user.IntezetId || user.IntezetId == 135).ToList();

                        if (userFegyelmiUgyei.Any())
                        {
                             Clients.Client(user.ConnectionUserId).fegyelmiUgyValtozas(userFegyelmiUgyei);
                        }
                        if (aktAktivitasFolyamList.Any())
                        {
                             Clients.Client(user.ConnectionUserId).aktivitasPanelFrissites(aktAktivitasFolyamList);
                        }
                    }

                }
                if (megszuntUgyIdList.Count > 0)
                {
                    List<int> fegyelmiUgyIds = new List<int>();
                    fegyelmiUgyIds.AddRange(megszuntUgyIdList);
                    List<FegyelmiUgyListItemViewModel> fegyelmiUgyek = new List<FegyelmiUgyListItemViewModel>();
                    using (FegyelmiUgyFunctions fegyelmiUgyFunctions = new FegyelmiUgyFunctions())
                    {
                        fegyelmiUgyek = fegyelmiUgyFunctions.GetFegyelmiUgyekByIds(fegyelmiUgyIds);
                    }
                    List<int> intezetIds = new List<int>();
                    var fegyelmiIntezetId = fegyelmiUgyek.Select(s => s.FegyelmiIntezetId).ToList();
                    var fogvatartottAktualisBvIntezet = fegyelmiUgyek.Select(s => s.FogvatartottAktualisBvIntezet).ToList();
                    intezetIds.AddRange(fegyelmiIntezetId);
                    intezetIds.AddRange(fogvatartottAktualisBvIntezet);
                    intezetIds.Add(135);
                    var intezetDistinct = intezetIds.Distinct().ToList();
                    var users = _connections.GetConnections().SelectMany(s => s.Value).Where(w => intezetDistinct.Contains(w.IntezetId)).ToList();
                    var aktAktivitasFolyamList = NotifyUserAktivitasPanelFrissites(megszuntUgyIdList);
                    foreach (var user in users)
                    {
                        await Clients.Client(user.ConnectionUserId).fegyelmiUgyMegszunes(megszuntUgyIdList);
                        await Clients.Client(user.ConnectionUserId).aktivitasPanelFrissites(aktAktivitasFolyamList);
                    }
                }
                lock (FegyelmiUgyekValtozasokIds)
                {
                    foreach (var item in ujUgyIdList)
                    {
                        FenyitesFegyelmiUgyekValtozasokList ujFegyelmiUgyek = new FenyitesFegyelmiUgyekValtozasokList();
                        ujFegyelmiUgyek.ModositasIdeje = DateTime.Now;
                        ujFegyelmiUgyek.ModositasTipusa = (int)FenyitesFegyelmiUgyekTipusai.UjFegyelmiUgy;
                        FegyelmiUgyekValtozasokIds.AddOrUpdate(item, ujFegyelmiUgyek, (k, v) => ujFegyelmiUgyek);
                    }
                    foreach (var item in valtozottUgyIdList)
                    {
                        FenyitesFegyelmiUgyekValtozasokList valtozottFegyelmiUgyek = new FenyitesFegyelmiUgyekValtozasokList();
                        valtozottFegyelmiUgyek.ModositasIdeje = DateTime.Now;
                        valtozottFegyelmiUgyek.ModositasTipusa = (int)FenyitesFegyelmiUgyekTipusai.MegvaltoztottFegyelmiUgy;
                        FegyelmiUgyekValtozasokIds.AddOrUpdate(item, valtozottFegyelmiUgyek, (k, v) => valtozottFegyelmiUgyek);
                    }
                    foreach (var item in megszuntUgyIdList)
                    {
                        FenyitesFegyelmiUgyekValtozasokList megszuntFegyelmiUgyek = new FenyitesFegyelmiUgyekValtozasokList();
                        megszuntFegyelmiUgyek.ModositasIdeje = DateTime.Now;
                        megszuntFegyelmiUgyek.ModositasTipusa = (int)FenyitesFegyelmiUgyekTipusai.MegszuntFegyelmiUgy;
                        FegyelmiUgyekValtozasokIds.AddOrUpdate(item, megszuntFegyelmiUgyek, (k, v) => megszuntFegyelmiUgyek);
                    }

                }
                var elmult30perc = DateTime.Now.AddMinutes(-30);
                List<int> torlendoFegyelmiUgyIds = new List<int>();
                torlendoFegyelmiUgyIds = FegyelmiUgyekValtozasokIds.Where(w => w.Value.ModositasIdeje < elmult30perc).Select(s => s.Key).ToList();
                foreach (var torlendoUgyId in torlendoFegyelmiUgyIds)
                {
                    lock (FegyelmiUgyekValtozasokIds)
                    {
                        FegyelmiUgyekValtozasokIds.TryRemove(torlendoUgyId, out _);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error($"NotifyUseresOnFegyelmiUgyValtozas hívás közben, hibaüznet {e.Message}");
                return false;
                throw;
            }
                        
            return true;
        }
        private List<AktivitasFolyamModel> NotifyUserAktivitasPanelFrissites(List<int> valtozottUgyIdList)
        {
            try
            {                
                List<AktivitasFolyamModel> getAktivitasList = new List<AktivitasFolyamModel>();
                using (FegyelmiUgyFunctions fegyelmiUgyFunctions = new FegyelmiUgyFunctions())
                {
                    getAktivitasList = fegyelmiUgyFunctions.GetAktivitasFolyamList(null,valtozottUgyIdList);
                }
                if(getAktivitasList.Count == 0 || getAktivitasList == null)
                {
                    var ugyIdListString = string.Join(",", valtozottUgyIdList);
                    Log.Error($"NotifyUserAktivitasPanelFrissites közben hiba lépett fel. A GetAktivitasFolyamList üres listát adott vissza. ÜgyIds: {ugyIdListString} ");                    
                }
                                
                return getAktivitasList;
            }
            catch (Exception e)
            {
                Log.Error("Hiba a NotifyUserAktivitasPanelFrissites fv.-ben, hibaüzenet: ", e);
            }
            
            return new List<AktivitasFolyamModel>();

        }

        //public async Task<bool> NotifyUserOnFegyelmiUgyValtozasFany(DateTime disconnectedDate, string connectionId)
        //{
        //    //Log.Info($"NotifyUserOnFegyelmiUgyValtozas started, timeout dátuma: {disconnectedDate}, ConnectionId: {connectionId}");

        //    var ujFegyelmiUgyek = FegyelmiUgyekValtozasokIdsFany.Where(w => w.Value.ModositasIdeje >= disconnectedDate && w.Value.ModositasTipusa == (int)FenyitesFegyelmiUgyekTipusai.UjFegyelmiUgy).Select(s => s.Key).ToList();
        //    var megvaltozottFegyelmiUgyek = FegyelmiUgyekValtozasokIdsFany.Where(w => w.Value.ModositasIdeje >= disconnectedDate && w.Value.ModositasTipusa == (int)FenyitesFegyelmiUgyekTipusai.MegvaltoztottFegyelmiUgy).Select(s => s.Key).ToList();
        //    var megszuntFegyelmiUgyek = FegyelmiUgyekValtozasokIdsFany.Where(w => w.Value.ModositasIdeje >= disconnectedDate && w.Value.ModositasTipusa == (int)FenyitesFegyelmiUgyekTipusai.MegszuntFegyelmiUgy).Select(s => s.Key).ToList();
        //    //Log.Info($"Új fegyelmi ügyek száma: {ujFegyelmiUgyek.Count}, Megváltozott fegyelmi ügyek száma: {megvaltozottFegyelmiUgyek.Count}, Megszűnt fegyelmi ügyek száma: {megszuntFegyelmiUgyek.Count}");
        //    if (ujFegyelmiUgyek.Any() || megvaltozottFegyelmiUgyek.Any())
        //    {
        //        List<int> fegyelmiUgyIds = new List<int>();
        //        fegyelmiUgyIds.AddRange(ujFegyelmiUgyek);
        //        fegyelmiUgyIds.AddRange(megvaltozottFegyelmiUgyek);

        //        var result = FenyitesDashboardFunctions.GetFenyitesDashboardListByFegyelmiUgyIds(fegyelmiUgyIds);
        //        await Clients.Client(connectionId).modifyFegyelmiUgyOnRecconectFany(result);
        //        //Log.Info($"Új és/vagy megváltozott fegyelmi ügyek kiküldve a {connectionId} kliensnek!");
        //    }

        //    if (megszuntFegyelmiUgyek.Any())
        //    {
        //        var result = FenyitesDashboardFunctions.GetFenyitesDashboardListByFegyelmiUgyIds(megszuntFegyelmiUgyek);
        //        await Clients.Client(connectionId).fegyelmiUgyMegszunesFany(megszuntFegyelmiUgyek);
        //        //Log.Info($"Megszűnt fegyelmi ügyek kiküldve a {connectionId} kliensnek!"); 
        //    }
        //    //Log.Info($"NotifyUserOnFegyelmiUgyValtozas ended!");
        //    return true;
        //}
        public async Task<bool> NotifyUserOnFegyelmiUgyValtozas(DateTime disconnectedDate, string connectionId)
        {

            //Log.Info($"NotifyUserOnFegyelmiUgyValtozas started, timeout dátuma: {disconnectedDate}, ConnectionId: {connectionId}");


            var ujFegyelmiUgyek = FegyelmiUgyekValtozasokIds.Where(w => w.Value.ModositasIdeje >= disconnectedDate && w.Value.ModositasTipusa == (int)FenyitesFegyelmiUgyekTipusai.UjFegyelmiUgy).Select(s => s.Key).ToList();
            var megvaltozottFegyelmiUgyek = FegyelmiUgyekValtozasokIds.Where(w => w.Value.ModositasIdeje >= disconnectedDate && w.Value.ModositasTipusa == (int)FenyitesFegyelmiUgyekTipusai.MegvaltoztottFegyelmiUgy).Select(s => s.Key).ToList();
            var megszuntFegyelmiUgyek = FegyelmiUgyekValtozasokIds.Where(w => w.Value.ModositasIdeje >= disconnectedDate && w.Value.ModositasTipusa == (int)FenyitesFegyelmiUgyekTipusai.MegszuntFegyelmiUgy).Select(s => s.Key).ToList();


            //Log.Info($"Új fegyelmi ügyek száma: {ujFegyelmiUgyek.Count}, Megváltozott fegyelmi ügyek száma: {megvaltozottFegyelmiUgyek.Count}, Megszűnt fegyelmi ügyek száma: {megszuntFegyelmiUgyek.Count}");
            if (ujFegyelmiUgyek.Any() || megvaltozottFegyelmiUgyek.Any())
            {
                List<int> fegyelmiUgyIds = new List<int>();
                fegyelmiUgyIds.AddRange(ujFegyelmiUgyek);
                fegyelmiUgyIds.AddRange(megvaltozottFegyelmiUgyek);
                List<FegyelmiUgyListItemViewModel> result = new List<FegyelmiUgyListItemViewModel>();
                using (FegyelmiUgyFunctions fegyelmiUgyFunctions = new FegyelmiUgyFunctions())
                {
                    result = fegyelmiUgyFunctions.GetFegyelmiUgyekByIds(fegyelmiUgyIds);
                }
                var users = _connections.GetConnections().SelectMany(s => s.Value).ToList();
                var bekuldoUser = users.Where(x => x.ConnectionUserId == connectionId).FirstOrDefault();
                if (bekuldoUser != null)
                {
                    var userFegyelmiUgyei = result.Where(w => w.FegyelmiIntezetId == bekuldoUser.IntezetId || w.FogvatartottAktualisBvIntezet == bekuldoUser.IntezetId || bekuldoUser.IntezetId == 135).ToList();
                    var aktivitasFolyamList = NotifyUserAktivitasPanelFrissites(fegyelmiUgyIds);
                    var aktAktivitasFolyamList = aktivitasFolyamList.Where(w => w.FegyelmiIntezetId == bekuldoUser.IntezetId || w.FogvatartottAktualisBvIntezet == bekuldoUser.IntezetId || bekuldoUser.IntezetId == 135).ToList();

                    if (userFegyelmiUgyei.Any())
                    {
                        await Clients.Client(bekuldoUser.ConnectionUserId).fegyelmiUgyValtozas(userFegyelmiUgyei);
                    }
                    if (aktAktivitasFolyamList.Any())
                    {
                        await Clients.Client(bekuldoUser.ConnectionUserId).aktivitasPanelFrissites(aktAktivitasFolyamList);
                    }
                }
                else
                {
                    await Clients.Client(connectionId).modifyFegyelmiUgyOnRecconect(result);
                }
            }

            if (megszuntFegyelmiUgyek.Any())
            {
                await Clients.Client(connectionId).fegyelmiUgyMegszunes(megszuntFegyelmiUgyek);
            }

            return true;
        }


        //private async Task<bool> NotifyUseresOnJutalomUgyValtozas(List<int> ujJutalomUgyIdList, List<int> valtozottJutalomUgyIdList, List<int> megszuntJutalomUgyIdList)
        //{
        //    try
        //    {
        //        if (ujJutalomUgyIdList.Count > 0 || valtozottJutalomUgyIdList.Count > 0)
        //        {
        //            List<int> jutalomUgyIds = new List<int>();
        //            jutalomUgyIds.AddRange(ujJutalomUgyIdList);
        //            jutalomUgyIds.AddRange(valtozottJutalomUgyIdList);
        //            List<JutalomListItemModel> jutalomUgyek = new List<JutalomListItemModel>();
        //            using (F3JutalomFunctions jutalomUgyFunctions = new F3JutalomFunctions())
        //            {
        //                jutalomUgyek = jutalomUgyFunctions.GetJutalomUgyekByIds(jutalomUgyIds);
        //            }
        //            if (jutalomUgyek.Count == 0 || jutalomUgyek == null)
        //            {
        //                StringBuilder builder = new StringBuilder();
        //                foreach (var jutalomUgy in jutalomUgyIds)
        //                {
        //                    builder.Append(jutalomUgy).Append(',');
        //                }
        //                string jutalomUgyStrings = builder.ToString();
        //                Log.Error($"Értesítés közben a GetJutalomUgyekByIds nulla jutalom ügyet adott vissza. Változtatásra külödtt jutalom ügyek idk: {jutalomUgyStrings}");
        //            }
        //            var users = _connections.GetConnections().SelectMany(s => s.Value).ToList();
        //            F3IntezetFunctions f3IntezetFunctions = new F3IntezetFunctions();
        //            foreach (var user in users)
        //            {
        //                var userIntezet = f3IntezetFunctions.GetIntezetIdByF2IntezetId(user.IntezetId);
        //                var userJutalomUgyei = jutalomUgyek.Where(w => w.IntezetId == userIntezet || w.FogvatartottAktualisBvIntezetId == userIntezet).ToList();
                        
        //                if (userJutalomUgyei.Any())
        //                {
        //                    Clients.Client(user.ConnectionUserId).jutalomUgyValtozas(userJutalomUgyei);
        //                }
        //            }

        //        }
        //        if (megszuntJutalomUgyIdList.Count > 0)
        //        {
        //            List<int> jutalomUgyIds = new List<int>();
        //            jutalomUgyIds.AddRange(megszuntJutalomUgyIdList);
        //            List<JutalomListItemModel> jutalomUgyek = new List<JutalomListItemModel>();
        //            using (F3JutalomFunctions jutalomUgyFunctions = new F3JutalomFunctions())
        //            {
        //                jutalomUgyek = jutalomUgyFunctions.GetJutalomUgyekByIds(jutalomUgyIds);
        //            }
        //            List<int> intezetIds = new List<int>();
        //            var jutalomIntezetId = jutalomUgyek.Select(s => s.IntezetId).ToList();
        //            var fogvatartottAktualisBvIntezet = jutalomUgyek.Select(s => s.FogvatartottAktualisBvIntezetId).ToList();
        //            intezetIds.AddRange(jutalomIntezetId);
        //            intezetIds.AddRange(fogvatartottAktualisBvIntezet);
        //            intezetIds.Add(135);
        //            var intezetDistinct = intezetIds.Distinct().ToList();
        //            var users = _connections.GetConnections().SelectMany(s => s.Value).Where(w => intezetDistinct.Contains(w.IntezetId)).ToList();
        //            foreach (var user in users)
        //            {
        //                await Clients.Client(user.ConnectionUserId).jutalomUgyMegszunes(megszuntJutalomUgyIdList);
        //            }
        //        }
        //        lock (JutalomUgyekValtozasokIds)
        //        {
        //            foreach (var item in ujJutalomUgyIdList)
        //            {
        //                JutalomUgyekValtozasList ujJutalomUgyek = new JutalomUgyekValtozasList();
        //                ujJutalomUgyek.ModositasIdeje = DateTime.Now;
        //                ujJutalomUgyek.ModositasTipusa = (int)JutalomUgyekValtozasTipusai.UjJutalomUgy;
        //                JutalomUgyekValtozasokIds.AddOrUpdate(item, ujJutalomUgyek, (k, v) => ujJutalomUgyek);
        //            }
        //            foreach (var item in valtozottJutalomUgyIdList)
        //            {
        //                JutalomUgyekValtozasList valtozottJutalomUgyek = new JutalomUgyekValtozasList();
        //                valtozottJutalomUgyek.ModositasIdeje = DateTime.Now;
        //                valtozottJutalomUgyek.ModositasTipusa = (int)JutalomUgyekValtozasTipusai.MegvaltoztottJutalomUgy;
        //                JutalomUgyekValtozasokIds.AddOrUpdate(item, valtozottJutalomUgyek, (k, v) => valtozottJutalomUgyek);
        //            }
        //            foreach (var item in megszuntJutalomUgyIdList)
        //            {
        //                JutalomUgyekValtozasList megszuntJutalomUgyek = new JutalomUgyekValtozasList();
        //                megszuntJutalomUgyek.ModositasIdeje = DateTime.Now;
        //                megszuntJutalomUgyek.ModositasTipusa = (int)JutalomUgyekValtozasTipusai.MegszuntJutalomUgy;
        //                JutalomUgyekValtozasokIds.AddOrUpdate(item, megszuntJutalomUgyek, (k, v) => megszuntJutalomUgyek);
        //            }

        //        }
        //        var elmult30perc = DateTime.Now.AddMinutes(-30);
        //        List<int> torlendoJutalomUgyIds = new List<int>();
        //        torlendoJutalomUgyIds = JutalomUgyekValtozasokIds.Where(w => w.Value.ModositasIdeje < elmult30perc).Select(s => s.Key).ToList();
        //        foreach (var torlendoUgyId in torlendoJutalomUgyIds)
        //        {
        //            lock (JutalomUgyekValtozasokIds)
        //            {
        //                JutalomUgyekValtozasokIds.TryRemove(torlendoUgyId, out _);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error($"NotifyUseresOnJutalomUgyValtozas hívás közben, hibaüznet {e.Message}");
        //        return false;
        //        throw;
        //    }

        //    return true;
        //}

        //public async Task<bool> NotifyUserOnJutalomUgyValtozas(DateTime disconnectedDate, string connectionId)
        //{
        //    var ujJutalomUgyek = JutalomUgyekValtozasokIds.Where(w => w.Value.ModositasIdeje >= disconnectedDate && w.Value.ModositasTipusa == (int)JutalomUgyekValtozasTipusai.UjJutalomUgy).Select(s => s.Key).ToList();
        //    var megvaltozottJutalomUgyek = JutalomUgyekValtozasokIds.Where(w => w.Value.ModositasIdeje >= disconnectedDate && w.Value.ModositasTipusa == (int)JutalomUgyekValtozasTipusai.MegvaltoztottJutalomUgy).Select(s => s.Key).ToList();
        //    var megszuntJutalomUgyek = JutalomUgyekValtozasokIds.Where(w => w.Value.ModositasIdeje >= disconnectedDate && w.Value.ModositasTipusa == (int)JutalomUgyekValtozasTipusai.MegszuntJutalomUgy).Select(s => s.Key).ToList();


        //    //Log.Info($"Új fegyelmi ügyek száma: {ujJutalomUgyek.Count}, Megváltozott fegyelmi ügyek száma: {megvaltozottJutalomUgyek.Count}, Megszűnt fegyelmi ügyek száma: {megszuntJutalomUgyek.Count}");
        //    if (ujJutalomUgyek.Any() || megvaltozottJutalomUgyek.Any())
        //    {
        //        List<int> jutalomUgyIds = new List<int>();
        //        jutalomUgyIds.AddRange(ujJutalomUgyek);
        //        jutalomUgyIds.AddRange(megvaltozottJutalomUgyek);
        //        List<JutalomListItemModel> result = new List<JutalomListItemModel>();
        //        using (F3JutalomFunctions jutalomUgyFunctions = new F3JutalomFunctions())
        //        {
        //            result = jutalomUgyFunctions.GetJutalomUgyekByIds(jutalomUgyIds);
        //        }
        //        F3IntezetFunctions f3IntezetFunctions = new F3IntezetFunctions();
        //        var users = _connections.GetConnections().SelectMany(s => s.Value).ToList();
        //        var bekuldoUser = users.Where(x => x.ConnectionUserId == connectionId).FirstOrDefault();
        //        var bekuldoUserIntezet = f3IntezetFunctions.GetIntezetIdByF2IntezetId(bekuldoUser.IntezetId);
        //        if (bekuldoUser != null)
        //        {
        //            var userJutalomUgyei = result.Where(w => w.IntezetId == bekuldoUserIntezet || w.FogvatartottAktualisBvIntezetId == bekuldoUserIntezet).ToList();
        //            if (userJutalomUgyei.Any())
        //            {
        //                await Clients.Client(bekuldoUser.ConnectionUserId).jutalomUgyValtozas(userJutalomUgyei);
        //            }
        //        }
        //        else
        //        {
        //            await Clients.Client(connectionId).modifyJutalomUgyOnRecconect(result);
        //        }
        //    }

        //    if (megszuntJutalomUgyek.Any())
        //    {
        //        await Clients.Client(connectionId).jutalomUgyMegszunes(megszuntJutalomUgyek);
        //    }

        //    return true;
        //}
    }
}