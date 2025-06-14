using FastTechFoods.Orders.Application.Dtos;
using FastTechFoods.Orders.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FastTech.Orders.Controllers
{
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] OrderPostDto payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

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

        [HttpPost("Cancelar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Cancelar([FromBody] ChangeStatusDto payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                throw new NotImplementedException();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send data to the order queue. Error {ex.Message} - {ex.StackTrace} ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("Rejeitar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Rejeitar([FromBody] ChangeStatusDto payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                throw new NotImplementedException();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send data to the order queue. Error {ex.Message} - {ex.StackTrace} ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("Aceitar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Aceitar([FromBody] ChangeStatusDto payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                throw new NotImplementedException();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send data to the order queue. Error {ex.Message} - {ex.StackTrace} ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("Iniciar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Iniciar([FromBody] ChangeStatusDto payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                throw new NotImplementedException();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send data to the order queue. Error {ex.Message} - {ex.StackTrace} ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("Finalizar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Finalizar([FromBody] ChangeStatusDto payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                throw new NotImplementedException();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send data to the order queue. Error {ex.Message} - {ex.StackTrace} ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}