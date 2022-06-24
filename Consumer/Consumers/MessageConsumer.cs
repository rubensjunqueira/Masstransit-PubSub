using MassTransit;
using Microsoft.Extensions.Logging;
using Publisher.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Consumers
{
    public class MessageConsumer : IConsumer<IMessageEvent>
    {
        public async Task Consume(ConsumeContext<IMessageEvent> context)
        {
            Console.WriteLine("Event value {0}", context.Message.Message);
        }
    }
}
