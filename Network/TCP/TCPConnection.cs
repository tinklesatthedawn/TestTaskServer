using DependencyInjection;
using Logger;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;

namespace Network.TCP
{
    public class TCPConnection
    {
        private List<Request> _requestsPool;
        private TcpListener _tcpListener;
        private bool _disposed = false;

        public TCPConnection(IPAddress ip, int port)
        {
            _requestsPool = new List<Request>();
            _tcpListener = new TcpListener(ip, port);
            _tcpListener.Start();
        }

        public async void StartRequestsListening()
        {
            await GetLogger().WriteInformationMessage(GetType().ToString(), "TCP server started", "TCP Connection started", CancelAfter(2000));
            while (!_disposed)
            {
                try
                {
                    TcpClient client = await _tcpListener.AcceptTcpClientAsync();
                    var request = new Request(client);
                    request.Notify += DisposeRequest;
                    _requestsPool.Add(request);
                }
                catch (InvalidOperationException e)
                {
                    await GetLogger().WriteWarningMessage(GetType().ToString(), "Invalid operation exception", e.Message, CancelAfter(2000));
                }
                catch (SocketException) { }
                catch (Exception e)
                {
                    await GetLogger().WriteErrorMessage(GetType().ToString(), "CRITICAL ERROR", e.Message, CancelAfter(2000));
                }
            }
        }

        public async void StopRequestListening()
        {
            _disposed = true;
            _tcpListener.Stop();
            await GetLogger().WriteInformationMessage(GetType().ToString(), "TCP server stopped", "TCP Connection stopped", CancelAfter(2000));
        }

        private void DisposeRequest(Request request)
        {
            _requestsPool.Remove(request);
        }

        private CancellationToken CancelAfter(int ms)
        {
            using (CancellationTokenSource source = new CancellationTokenSource())
            {
                source.CancelAfter(ms);
                return source.Token;
            }
        }

        private ILogger GetLogger()
        {
            return DI.Get<ILogger>();
        }
    }
}
