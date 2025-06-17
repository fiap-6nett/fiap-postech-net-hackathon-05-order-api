using AutoMapper;
using FastTechFoods.Orders.Application.Dtos;
using FastTechFoods.Orders.Application.Interfaces;
using FastTechFoods.Orders.Domain.Enums;
using FastTechFoods.Orders.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FastTechFoods.Orders.Controllers
{
    [Route("api/v1.0/Pedidos")]
    public class OrderController(
                ILogger<OrderController> logger, 
                IOrderService orderService,
                IOrderRepository orderRepository,
                IMapper mapper): ControllerBase
    {

        private readonly ILogger<OrderController> _logger = logger;
        private readonly IOrderService _orderService = orderService;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMapper _mapper = mapper;
        
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Criar([FromBody] OrderDto payload)
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

        [HttpPost("[action]")]
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
                
                _logger.LogInformation($"Acess cancel - Order. Payload {payload}");

                var chageStatus = new ChangeStatusSendQueueDto()
                {
                    OrderId = payload.OrderId,
                    OrderStatus = OrderStatus.Cancelled,
                    Justification = payload.Justification

                };

                await _orderService.SendOrderChangeStatusAsync(chageStatus);

                var returnDto = new ResponseDto
                {
                    Id = payload.OrderId,
                    CreatedAt = DateTime.Now,
                };

                _logger.LogInformation($"Order sent to the  cancel order queue. Id {payload.OrderId}");

                return Ok(returnDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send data to the order queue. Error {ex.Message} - {ex.StackTrace} ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("[action]")]
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
                
                _logger.LogInformation($"Acess reject - Order. Payload {payload}");

                var chageStatus = new ChangeStatusSendQueueDto()
                {
                    OrderId = payload.OrderId,
                    OrderStatus = OrderStatus.Rejected,
                    Justification = payload.Justification

                };

                await _orderService.SendOrderChangeStatusAsync(chageStatus);

                var returnDto = new ResponseDto
                {
                    Id = payload.OrderId,
                    CreatedAt = DateTime.Now,
                };

                _logger.LogInformation($"Order sent to the  reject order queue. Id {payload.OrderId}");

                return Ok(returnDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send data to the order reject queue. Error {ex.Message} - {ex.StackTrace} ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("[action]")]
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
                
                _logger.LogInformation($"Acess accept - Order. Payload {payload}");

                var chageStatus = new ChangeStatusSendQueueDto()
                {
                    OrderId = payload.OrderId,
                    OrderStatus = OrderStatus.Accepted,
                    Justification = payload.Justification

                };

                await _orderService.SendOrderChangeStatusAsync(chageStatus);

                var returnDto = new ResponseDto
                {
                    Id = payload.OrderId,
                    CreatedAt = DateTime.Now,
                };

                _logger.LogInformation($"Order sent to the  accept order queue. Id {payload.OrderId}");

                return Ok(returnDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send data to the order accept queue. Error {ex.Message} - {ex.StackTrace} ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("[action]")]
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
                
                _logger.LogInformation($"Acess progress - Order. Payload {payload}");

                var chageStatus = new ChangeStatusSendQueueDto()
                {
                    OrderId = payload.OrderId,
                    OrderStatus = OrderStatus.InProgress,
                    Justification = payload.Justification
                };

                await _orderService.SendOrderChangeStatusAsync(chageStatus);

                var returnDto = new ResponseDto
                {
                    Id = payload.OrderId,
                    CreatedAt = DateTime.Now,
                };

                _logger.LogInformation($"Order sent to the progress order queue. Id {payload.OrderId}");

                return Ok(returnDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send data to the order progress queue. Error {ex.Message} - {ex.StackTrace} ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[action]")]
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
                
                _logger.LogInformation($"Acess finished - Order. Payload {payload}");

                var chageStatus = new ChangeStatusSendQueueDto()
                {
                    OrderId = payload.OrderId,
                    OrderStatus = OrderStatus.Finished,
                    Justification = payload.Justification

                };

                await _orderService.SendOrderChangeStatusAsync(chageStatus);

                var returnDto = new ResponseDto
                {
                    Id = payload.OrderId,
                    CreatedAt = DateTime.Now,
                };

                _logger.LogInformation($"Order sent to finished order queue. Id {payload.OrderId}");

                return Ok(returnDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send data to finished progress queue. Error {ex.Message} - {ex.StackTrace} ");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
                
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<ResponseOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                _logger.LogInformation($"Acessou {nameof(ObterTodos)}.");
                var response = _mapper.Map<IEnumerable<ResponseOrderDto>>(await _orderRepository.GetAll());
                                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed API Orders. Erro: {ex}");
                return StatusCode(500, $"Internal server error - {ex}");
            }
        }

    }
}