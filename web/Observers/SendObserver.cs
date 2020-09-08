using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Observers
{
    public class SendObserver : ISendObserver
    {
        public Task PostSend<T>(SendContext<T> context) where T : class
        {
            return Task.CompletedTask;
        }

        public Task PreSend<T>(SendContext<T> context) where T : class
        {
            return Task.CompletedTask;
        }

        public Task SendFault<T>(SendContext<T> context, Exception exception) where T : class
        {
            return Task.CompletedTask;
        }
    }
}
