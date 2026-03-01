using Network.Http;
using Network.TCP;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Network
{
    public class Server
    {
        public static TCPConnection? TCPConnection = null;
        public static HttpConnection? HttpConnection = null;
        
        public static bool StartTcp()
        {
            if (TCPConnection == null)
            {
                TCPConnection = new TCPConnection(IPAddress.Parse("127.0.0.1"), 8888);
                TCPConnection.StartRequestsListening();
                return true;
            }
            return false;
        }

        public static bool StartHttp()
        {
            if (HttpConnection == null)
            {
                HttpConnection = new HttpConnection(IPAddress.Parse("127.0.0.1"), 7777);
                HttpConnection.StartRequestsListening();
                return true;
            }
            return false;
        }

        public static bool StopHttp()
        {
            if (HttpConnection != null)
            {
                HttpConnection.StopRequestListening();
                HttpConnection = null;
                return true;
            }
            return false;
        }

        public static bool StopTcp()
        {
            if (TCPConnection != null)
            {
                TCPConnection.StopRequestListening();
                TCPConnection = null;
                return true;
            }
            return false;
        }

        public static void Start()
        {
            StartTcp();
            StartHttp();
        }

        public static void Stop()
        {
            StopTcp();
            StopHttp();
        }
    }
}
