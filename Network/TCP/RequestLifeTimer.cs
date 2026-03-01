using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;

namespace Network.TCP
{
    internal class RequestLifeTimer
    {
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _token;
        private static readonly int lifespan = 8000;
        private System.Timers.Timer _timer;

        public RequestLifeTimer()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _token = _cancellationTokenSource.Token;

            _timer = new System.Timers.Timer(lifespan);
            _timer.AutoReset = false;
            _timer.Elapsed += OnExpired;
        }

        private void OnExpired(object? sender, ElapsedEventArgs e)
        {
            _timer.Dispose();
            _cancellationTokenSource.Cancel();
        }

        internal void SubscribeOnElapsed(ElapsedEventHandler onExpired)
        {
            _timer.Elapsed += onExpired;
        }

        internal CancellationToken GetToken() 
        {
            return _token;
        }

        internal void Start()
        {
            _timer.Start();
        }

        internal void Stop()
        {
            _timer.Stop();
        }
    }
}
