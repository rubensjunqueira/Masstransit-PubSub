using MassTransit;
using Publisher.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Consumers
{
    public class CreatedPersonConsumer : IConsumer<ICreatedPersonEvent>
    {
        public async Task Consume(ConsumeContext<ICreatedPersonEvent> context)
        {
            var message = context.Message;
            Console.WriteLine($"Id: {message.Id}\n" +
                              $"FirstName: {message.FirstName}\n" +
                              $"LastName: {message.LastName}\n" +
                              $"Email: {message.Email}");
        }
    }
}
