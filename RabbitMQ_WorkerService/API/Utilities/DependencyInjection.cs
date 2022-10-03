using API.Services;
using LibraryRabbitMQ;

namespace API.Utilities;

public static class DependencyInjections
{

    public static void RegisterDependency(this IServiceCollection services)
    {
        services.AddScoped<IRabbitMQConnection, RabbitMQConnection>();
        services.AddScoped<IRabbitMQCarPublisherService, RabbitMQCarPublisherService>();

    }
}