using System.Collections.Generic;
using System.Linq;

namespace DRproxy.MemeoryStorage
{

    public class ConnectionInfo
    {
        public ConnectionInfo(string clientId, string connectionId)
        {
            this._clientId =clientId;
            this._connectionId=connectionId;
        }
        public string _clientId { get; set; }
        public string _connectionId { get; set; }
    }


    public class ConnectionMapping<T>
    {
        private readonly List<ConnectionInfo> _connections = new List<ConnectionInfo>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(string clientId, string connectionId)
        {
            lock (_connections)
            {
               _connections.Add( new ConnectionInfo("clientId", "connectionId"));
            }
        }

        public String GetClientId(string connectionId)
        {
            string _clientId = "";
            lock (_connections)
            {

                foreach (ConnectionInfo con in _connections)
                {
                    if (con._connectionId.Equals(connectionId)){
                        _clientId = con._clientId;
                        break;
                    }
                }
                return _clientId;
            }
        }

            public String GetConnectiontId(string clientId)
        {
            string _connectionId = "";
            lock (_connections)
            {

                foreach (ConnectionInfo con in _connections)
                {
                    if (con._clientId.Equals(clientId)){
                        _connectionId = con._connectionId;
                        break;
                    }
                }
                return _connectionId;
            }
        }

        public void Remove(string connectionId)
        {
            lock (_connections)
            {
               foreach (ConnectionInfo con in _connections)
               {
                    if (con._connectionId.Equals(connectionId)){
                        _connections.Remove(con);
                        return;
                    }
               }
            }
        }
    }
}