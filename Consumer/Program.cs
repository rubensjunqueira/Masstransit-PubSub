using Consumer.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(Environment.GetEnvironmentVariable("RABBITMQ_ADDRESS"));
                cfg.ReceiveEndpoint("message-endpoint", e =>
                {
                    e.Consumer<MessageConsumer>();
                });

                cfg.ReceiveEndpoint("created-person-endpoint", e =>
                {
                    e.Consumer<CreatedPersonConsumer>();
                });
            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try
            {
                Console.WriteLine("Press enter to exit");

                await Task.Run(() => Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
