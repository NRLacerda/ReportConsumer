using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportConsumer.Models;
using ReportConsumer.Services;
using System.Text;
using System.Text.Json;

public class MyReportConsumer
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
        consumer.ReceivedAsync += async (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                var report = JsonSerializer.Deserialize<ReportModel>(message);
                if (report != null)
                {
                    Console.WriteLine("Successfully deserialized ReportModel:");
                    Console.WriteLine($"StartAt: {report.StartAt}");
                    Console.WriteLine($"EndAt: {report.EndAt}");
                    Console.WriteLine($"UserGuid: {report.UserGuid}");
                    Console.WriteLine($"TipoProcedimento: {report.TipoProcedimento}");
                    var service = new ReportService();
                    service.createReport(report);
                }
                else
                {
                    Console.WriteLine("Failed to deserialize the JSON message into ReportModel.");
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing message: {ex.Message}");
            }

            await Task.CompletedTask;
        };

        await channel.BasicConsumeAsync(queue: "reports", autoAck: true, consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();

    }
}
