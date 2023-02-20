using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Edis.Diagnostics;
using Edis.Fenyites.Controllers.Base;
using Edis.Functions.Common;
using Edis.Functions.Fany;
using Edis.IoC.Attributes;
using Edis.ViewModels.Common;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;    

namespace Edis.Fenyites.Hubs
{
    [HubName("SystemEventsHub")]  
    public class SystemEventsHub : Hub
    {

        [Inject]
        public IAlkalmazasKontextusFunctions AlkalmazasKontextusFunctions { get; set; }

        public readonly static ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();

        private readonly Ticker _ticker;

        public SystemEventsHub() :
            this(Ticker.Instance)
        {

        }        
        public SystemEventsHub(Ticker ticker)
        {
            _ticker = ticker;
        }
        
        public override Task OnConnected()
        {
            try
            {                
                var sid = WindowsIdentity.GetCurrent();                                
                var intezetId = Context.Request.Cookies.Where(w => w.Key.Contains("fegyelmiIntezetId")).Select(s => Int32.Parse(s.Value.Value)).SingleOrDefault();
                _connections.Add(sid.User.Value, Context.ConnectionId, intezetId);                                
            }
            catch (Exception e)
            {                
                Log.Error("OnConnected error: " + e);
            }
            return base.OnConnected();
        }
        
        public override Task OnDisconnected(bool stopCalled)
        {
            var sid = WindowsIdentity.GetCurrent();
            try
            {
                _connections.Remove(sid.User.Value, Context.ConnectionId);                
            }
            catch (Exception e)
            {
                Log.Error("OnDisconnected error: " + e);
            }            
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var sid = WindowsIdentity.GetCurrent();
            var intezetId = Context.Request.Cookies.Where(w => w.Key.Contains("fegyelmiIntezetId")).Select(s => Int32.Parse(s.Value.Value)).SingleOrDefault();
            if (!_connections.GetConnectionsWithSid(sid.User.Value).Contains(Context.ConnectionId))
            {
                _connections.Add(sid.User.Value, Context.ConnectionId, intezetId);
            }
            return base.OnReconnected();
        }

        public void Hello()
        {
            Clients.All.cheers("hello bro!");
        }
        public async void GetLegfrissebbFenyitesekByDatum(DateTime disconnectedDate)
        {
           var localDate = disconnectedDate.ToLocalTime();
           var conncetionId = Context.ConnectionId;
           await _ticker.NotifyUserOnFegyelmiUgyValtozas(localDate, conncetionId);
           //await _ticker.NotifyUserOnFegyelmiUgyValtozasFany(localDate, conncetionId);
            
        }
        public void RefreshFenyitesekFany()
        {
            Clients.All.refreshFenyitesekListAfterServerRRFany();
        }
        public void RefreshFenyitesek()
        {
            Clients.All.refreshFenyitesekListAfterServerRR();
        }
        public void SetIntezetIdToUser(string socketConnectionId, int intezetId)
        {
            var sid = WindowsIdentity.GetCurrent();
            _connections.SetIntezetId(sid.User.Value, socketConnectionId, intezetId);
            var connectionIds = ConnectionMapping<string>._connections.Where(w => w.Key == sid.User.Value)
                                .SelectMany(s => s.Value).Select(i => i.ConnectionUserId)
                                .Where(w => w != socketConnectionId).ToList();
            Clients.Clients(connectionIds).refreshAfterInetezetValtas(intezetId);
        }
    }
}