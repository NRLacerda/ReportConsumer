using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class ReportConsumer
{
    public static async Task Main(string[] args)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync("reports_exchange", ExchangeType.Topic, durable: false, autoDelete: false);

        await channel.QueueDeclareAsync(queue: "reports", durable: true, exclusive: false, autoDelete: false);

        await channel.QueueBindAsync(queue: "reports", exchange: "reports_exchange", routingKey: "reports.bacen");

        Console.WriteLine(" [*] Waiting for reports.");

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received: {message}");
            return Task.CompletedTask;
        };

        await channel.BasicConsumeAsync(queue: "reports", autoAck: true, consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();

    }
}
