using System;

namespace RabbitMQ
{
    internal class ConnectionFactory
    {
        public object Endpoint { get; set; }
        public string Password { get; set; }
        public int RequestedHeartbeat { get; set; }
        public string UserName { get; set; }

        internal IDisposable CreateConnection()
        {
            throw new NotImplementedException();
        }
    }
}