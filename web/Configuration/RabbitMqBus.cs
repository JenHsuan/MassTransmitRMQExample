using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using web.Observers;

namespace web.Configuration
{
    public class RabbitMqBus
    {
        public static IBusControl CreateRabbitMqBus(IRegistrationContext<IServiceProvider> context)
        {
            var rabbitMq = context.Container.GetRequiredService<RabbitMq>();
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(rabbitMq.Host, hostConfigurator =>
                {
                    hostConfigurator.Username(rabbitMq.Username);
                    hostConfigurator.Password(rabbitMq.Password);
                });
                //cfg.UseServiceBusMessageScheduler();
                cfg.UseDelayedExchangeMessageScheduler();
                cfg.ConfigureEndpoints(context);
                //Configure endpoints automatically. Might need to configure them manually, if we want to set up custom retry / redelivery stuff.

                cfg.ConnectBusObserver(context.Container.GetRequiredService<BusObserver>());
            });

            bus.ConnectSendObserver(context.Container.GetRequiredService<SendObserver>());
            bus.ConnectReceiveObserver(context.Container.GetRequiredService<ReceiveObserver>());

            return bus;
        }
    }
}
