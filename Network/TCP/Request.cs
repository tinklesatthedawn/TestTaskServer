using DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Text.Unicode;
using System.Timers;

namespace Network.TCP
{
    internal class Request
    {
        private RequestLifeTimer _lifeTimer;
        private TcpClient _client;
        private Task _task;

        public delegate void OnDisposed(Request sender);
        public event OnDisposed? Notify;


        public Request(TcpClient client)
        {
            _lifeTimer = new RequestLifeTimer();
            _lifeTimer.SubscribeOnElapsed(OnExpired);
            _lifeTimer.Start();

            _client = client;
            _task = new Task(async () => await Run(), _lifeTimer.GetToken());
            _task.Start();
        }

        private void OnFinished()
        {
            Notify?.Invoke(this);
            _lifeTimer.Stop();
        }

        private void OnExpired(object? sender, ElapsedEventArgs e)
        {
            Notify?.Invoke(this);
            Console.WriteLine("Connection disposed");
        }

        private async Task Run()
        {
            if (!GetToken().IsCancellationRequested)
            {
                try
                {
                    using var stream = _client.GetStream();
                    string request = await ReadRequest(stream, GetToken());
                    string response = await TCPController.Get(request).Handle(GetToken());
                    Console.WriteLine($" Request: {request}");
                    Console.WriteLine($" Response: {response}");
                    await WriteResponse(stream, response, GetToken());
                    stream.Close();
                    OnFinished();
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e.ToString());
                }
                catch (InvalidDataException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        //Клиент отправляет запрос пакетами по 512 байт ровно. Если в конце пакета нет <EOM>, тогда ожидается еще пакет
        private async Task<string> ReadRequest(NetworkStream stream, CancellationToken cancellationToken)
        {
            string request = string.Empty;
            byte[] buffer = new byte[512];
            string requestBuffer = string.Empty;
            bool isMessageEnded = false;

            try
            {
                do
                {
                    await stream.ReadExactlyAsync(buffer, cancellationToken);
                    buffer = buffer.Where(b => b != 0).ToArray();
                    requestBuffer = UTF8Encoding.UTF8.GetString(buffer);
                    isMessageEnded = requestBuffer.EndsWith("<EOM>");
                    request += requestBuffer;
                }
                while (!isMessageEnded && !cancellationToken.IsCancellationRequested);
            } 
            catch { }
            
            return request.TrimEnd("<EOM>").ToString().TrimEnd();
        }

        private async Task WriteResponse(NetworkStream stream, string response, CancellationToken cancellationToken)
        {
            try
            {
                var byteResponse = UTF8Encoding.UTF8.GetBytes(response + "<EOM>");
                var packets = GetPackets(byteResponse);

                foreach (var packet in packets)
                {
                    await stream.WriteAsync(packet, cancellationToken);
                }
            } 
            catch { }
        }

        private List<byte[]> GetPackets(byte[] byteResponse)
        {
            return Utils.SplitArray(byteResponse, 512);
        }

        private CancellationToken GetToken()
        {
            return _lifeTimer.GetToken();
        }
    }
}
