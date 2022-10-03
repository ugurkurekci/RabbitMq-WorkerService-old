using LibraryRabbitMQ;
using LibraryRabbitMQ.Business;

namespace Worker;

public static class DependencyInjections
{

    public static void RegisterDependency(this IServiceCollection services)
    {
        services.AddTransient<IRabbitMQConnection, RabbitMQConnection>();
        services.AddTransient<IConsumerService, ConsumerService>();

    }
}