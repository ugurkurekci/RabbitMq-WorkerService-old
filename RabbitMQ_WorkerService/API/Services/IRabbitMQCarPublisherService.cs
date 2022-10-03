using API.Dtos;

namespace API.Services;

public interface IRabbitMQCarPublisherService
{
    ResultDto PushToQueue(CarDto carDto);

}