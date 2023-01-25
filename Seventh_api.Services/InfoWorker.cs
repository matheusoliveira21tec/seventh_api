using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Seventh_api.Services
{
    public class InfoWorker : IHostedService
    {
        private Timer _timer;

        public InfoWorker()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"Process started {nameof(InfoWorker)}");

            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            Console.WriteLine($"{DateTime.Now:o} =>  ");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"Process finished {nameof(InfoWorker)}");

            return Task.CompletedTask;
        }
    }
}