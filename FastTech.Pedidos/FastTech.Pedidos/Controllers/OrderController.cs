using FastTech.Pedidos.Application.Dtos;
using FastTech.Pedidos.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FastTech.Pedidos.Controllers;

[Route("api/v1.0/Pedidos")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
    }
    
    
    
    [HttpPost]
    public Task<IActionResult> Post([FromBody] OrderPostDto order)
    {
        _logger.LogInformation($"Acess Post - Order. Payload {order}");
        throw new NotImplementedException();
    }
}