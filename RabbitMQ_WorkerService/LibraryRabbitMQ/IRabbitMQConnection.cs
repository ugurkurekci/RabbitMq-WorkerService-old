using RabbitMQ.Client;

namespace LibraryRabbitMQ;

public interface IRabbitMQConnection
{
    IConnection GetConnection();

    IModel GetModel(IConnection connection);

}