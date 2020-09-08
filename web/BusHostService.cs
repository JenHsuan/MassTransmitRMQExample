using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace web
{
    // https://github.com/MassTransit/MassTransit/blob/develop/src/Containers/MassTransit.AspNetCoreIntegration/MassTransitHostedService.cs
    public class BusHostService : IHostedService
    {
        private readonly IBusControl _busControl;

        public BusHostService(IBusControl bus)
        {
            _busControl = bus;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _busControl.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _busControl.StopAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
