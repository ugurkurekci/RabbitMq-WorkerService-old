using LibraryRabbitMQ.Business;

namespace Worker;

public class WorkerRun : BackgroundService
{
    private readonly IConsumerService _consumerService;

    public WorkerRun(IConsumerService consumerService)
    {

        _consumerService = consumerService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Worker started");
        await _consumerService.Start();
        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Worker stopping");
        await base.StopAsync(cancellationToken);
        await _consumerService.Stop();
    }
}