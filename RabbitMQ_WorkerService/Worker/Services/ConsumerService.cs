using API.Dtos;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace LibraryRabbitMQ.Business;

public class ConsumerService : IConsumerService
{
    private EventingBasicConsumer consumer;
    private IConnection connection;
    private IModel _channel;
    private readonly IRabbitMQConnection _rabbitMQService;
    private readonly IConfiguration _configuration;

    public ConsumerService(IRabbitMQConnection rabbitMQService, IConfiguration configuration)
    {
        _rabbitMQService = rabbitMQService;

        _configuration = configuration;
    }

    public Task Start()
    {
        IConnection connection = _rabbitMQService.GetConnection();
        _channel = _rabbitMQService.GetModel(connection);

        EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);

        consumer.Received += ConsumerMessageReceivedAsync;

        string queueName = "RabbitMQSettings:xxxQueue";
        string queuenameBasic = _configuration.GetSection(queueName).Value;
        if (queuenameBasic == null)
        {
            return Task.CompletedTask;
        }
        _channel.BasicConsume(queuenameBasic, false, consumer);
        return Task.CompletedTask;
    }

    public Task Stop()
    {
        Console.WriteLine("Worker Stopped !");
        Dispose();
        connection.Close();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel.Dispose();

    }

    private void ConsumerMessageReceivedAsync(object? sender, BasicDeliverEventArgs e)
    {
        byte[] byteArr = e.Body.ToArray();
        string bodyStr = Encoding.UTF8.GetString(byteArr);
        try
        {
            Console.WriteLine("\nBody: " + bodyStr + "\n");

            CarDto queueMessage = JsonConvert.DeserializeObject<CarDto>(bodyStr);

            // TODO

            /// Business example : sql data added

            _channel.BasicAck(e.DeliveryTag, true);

        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

}