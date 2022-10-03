using API.Dtos;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

public class CarController : BaseController
{
    private readonly IRabbitMQCarPublisherService _rabbitmqCarPublisherService;

    public CarController(IRabbitMQCarPublisherService rabbitmqCarPublisherService)
    {
        _rabbitmqCarPublisherService = rabbitmqCarPublisherService;
    }

    [HttpPost]
    public IActionResult Post([FromBody] CarDto carDto)
    {
        ResultDto resultDto = _rabbitmqCarPublisherService.PushToQueue(carDto);
        if (resultDto.Status)
        {
            return StatusCode((int)HttpStatusCode.Created);

        }

        return BadRequest(new { error = resultDto.Message });
    }

}