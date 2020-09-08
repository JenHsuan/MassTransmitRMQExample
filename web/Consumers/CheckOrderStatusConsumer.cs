using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.Contracts;

namespace web.Consumers
{
    public class CheckOrderStatusConsumer : IConsumer<CheckOrderStatus>
    {
        public async Task Consume(ConsumeContext<CheckOrderStatus> context)
        {
            await context.RespondAsync<OrderStatusResult>(new
            {
                context.Message.OrderId
            });
        }
    }
}
