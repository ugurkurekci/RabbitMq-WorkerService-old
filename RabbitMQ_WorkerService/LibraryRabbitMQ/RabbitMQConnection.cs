using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace LibraryRabbitMQ;

public class RabbitMQConnection : IRabbitMQConnection
{
    private readonly IConfiguration _configuration;

    public RabbitMQConnection(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IConnection GetConnection()
    {
        try
        {
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                TopologyRecoveryEnabled = false
            };

            _configuration.GetSection("RabbitMQConnectionSettings").Bind(connectionFactory);           
            return connectionFactory.CreateConnection();
        }
        catch (Exception)
        {
            return null;
        }

    }

    public IModel GetModel(IConnection connection)
    {
        return connection.CreateModel();
    }
}