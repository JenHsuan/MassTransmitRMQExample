using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Observers
{
    public class BusObserver : IBusObserver
    {
        public Task CreateFaulted(Exception exception)
        {
            return Task.CompletedTask;
        }

        public Task PostCreate(IBus bus)
        {
            return Task.CompletedTask;
        }

        public Task PostStart(IBus bus, Task<BusReady> busReady)
        {
            return Task.CompletedTask;
        }

        public Task PostStop(IBus bus)
        {
            return Task.CompletedTask;
        }

        public Task PreStart(IBus bus)
        {
            return Task.CompletedTask;
        }

        public Task PreStop(IBus bus)
        {
            throw new NotImplementedException();
        }

        public Task StartFaulted(IBus bus, Exception exception)
        {
            return Task.CompletedTask;
        }

        public Task StopFaulted(IBus bus, Exception exception)
        {
            return Task.CompletedTask;
        }
    }
}
