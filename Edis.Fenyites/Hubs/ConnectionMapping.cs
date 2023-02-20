using Edis.Diagnostics;
using Edis.Fenyites.Controllers.Base;
using Edis.ViewModels.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edis.Fenyites.Hubs
{
    public class ConnectionMapping<T>
    {                
        public static readonly ConcurrentDictionary<string,List<SocketAuthenticationViewModel>> _connections = new ConcurrentDictionary<string, List<SocketAuthenticationViewModel>>();
        private static ConcurrentDictionary<string, string> _helperConnections = new ConcurrentDictionary<string, string>();
        public int CountSids
        {
            get
            {
                return _connections.Values.Count;
            }
        }
        
        public int CountConnections
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(string sid, string connectionId, int intezetId)
        {            
            lock (_connections)
            {
                List<SocketAuthenticationViewModel> connectionModel;
                if(!_connections.TryGetValue(sid, out connectionModel))
                {                    
                    connectionModel = new List<SocketAuthenticationViewModel>();                   
                    _connections.TryAdd(sid, connectionModel);
                }
                lock (connectionModel)
                {
                    if(!connectionModel.Any(a => a.ConnectionUserId == connectionId))
                    {
                        connectionModel.Add(new SocketAuthenticationViewModel
                        {
                            ConnectionUserId = connectionId,
                            Sid = sid,
                            IdentityName = "",
                            IntezetId = intezetId,
                        });
                    }
                    
                }
                lock (_helperConnections)
                {
                    _helperConnections.TryAdd(connectionId, sid);
                }
            }
                
        }
        
        public IEnumerable<string> GetConnectionsWithSid(string sid)
        {

            List<SocketAuthenticationViewModel> connections;

            if(_connections.TryGetValue(sid, out connections))
            {
                var connectionIds = connections.Select(s => s.ConnectionUserId).ToList();
                return connectionIds;
            }
            return Enumerable.Empty<string>();
        }

        public void Remove(string sid, string connectionId)
        {
            
            lock (_helperConnections)
            {
                string sidValue;
                if(!_helperConnections.TryGetValue(connectionId, out sidValue))
                {
                    return;
                }
                lock (_connections)
                {
                    List<SocketAuthenticationViewModel> connectionDetails = null;
                    if(!_connections.TryGetValue(sidValue, out connectionDetails))
                    {
                        return;
                    }
                    var connection = connectionDetails.Single(s => s.ConnectionUserId == connectionId);

                    _connections[sidValue].Remove(connection);
                    _helperConnections.TryRemove(connectionId, out _);
                }
            }
        }
        public bool SetIntezetId(string sid, string connectionId, int intezetId)
        {

            try
            {
                lock (_connections)
                {
                    _connections.Where(w => w.Key == sid).SelectMany(s => s.Value).Select(i => {
                        i.IntezetId = intezetId; return i;
                    }).ToList();
                }                             
                return true;
            }
            catch (Exception e)
            {
                Log.Info(e.Message);
                return false;
            }

        }
        public ConcurrentDictionary<string, List<SocketAuthenticationViewModel>> GetConnections()
        {
            return _connections;
        }


    }
}