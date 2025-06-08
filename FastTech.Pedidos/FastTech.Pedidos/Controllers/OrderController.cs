using FastTech.Pedidos.Application.Dtos;
using FastTech.Pedidos.Application.Interfaces;
using FastTech.Pedidos.Domain.Entities;
using FastTech.Pedidos.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FastTech.Pedidos.Controllers;

[Route("api/v1.0/Pedidos")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;

    public OrderController(ILogger<OrderController> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] OrderPostDto payload)
    {
        try
        {
            _logger.LogInformation($"Acess Post - Order. Payload {payload}");
            
           var orderId = await _orderService.SendOrderQueueAsync(payload);

           var returnDto = new ResponseDto
           {
               Id = orderId,
               CreatedAt = DateTime.Now,
           };
           
           _logger.LogInformation($"Order sent to the order queue. Id {orderId}");
           return Ok(returnDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to send data to the order queue. Error {ex.Message} - {ex.StackTrace} ");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
        }
       
    }
}