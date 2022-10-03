using API.Dtos;
using LibraryRabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace API.Services;

public class RabbitMQCarPublisherService : IRabbitMQCarPublisherService
{
    private EventingBasicConsumer consumer;
    private IModel channel;
    private IConnection connection;
    private readonly IConfiguration _configuration;
    private readonly IRabbitMQConnection _rabbitMQConnectionManager;

    public RabbitMQCarPublisherService(IRabbitMQConnection rabbitMQConnectionManager, IConfiguration configuration)
    {
        _rabbitMQConnectionManager = rabbitMQConnectionManager;
        _configuration = configuration;
    }
    public ResultDto PushToQueue(CarDto carDto)
    {
        connection = _rabbitMQConnectionManager.GetConnection();
        channel = _rabbitMQConnectionManager.GetModel(connection);
        consumer = new EventingBasicConsumer(channel);

        string message = JsonConvert.SerializeObject(carDto);

        byte[] body = Encoding.UTF8.GetBytes(message);
        string exchange = "RabbitMQSettings:xxxExchange";
        string routingKey = "RabbitMQSettings:xxxRoutingKey";

        try
        {
            channel.BasicPublish(exchange: _configuration.GetValue<string>(exchange),
                               routingKey: _configuration.GetValue<string>(routingKey),
                               body: body);

            Console.WriteLine(" [x] Sent {0}", message);

        }

        catch (Exception ex)
        {

            Console.WriteLine(ex.Message, true);

            return new ResultDto
            {

                Message = ex.Message,
                Status = false

            };

        }

        return new ResultDto
        {

            Message = message,
            Status = true

        };
    }
}