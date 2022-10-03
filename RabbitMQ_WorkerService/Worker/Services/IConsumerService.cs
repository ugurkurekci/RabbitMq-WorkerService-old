namespace LibraryRabbitMQ.Business;

public interface IConsumerService : IDisposable
{
    Task Start();
    Task Stop();
}