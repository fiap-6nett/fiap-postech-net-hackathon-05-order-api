using AutoMapper;
using FastTechFoods.Orders.Application.Dtos;
using FastTechFoods.Orders.Application.Interfaces;
using FastTechFoods.Orders.Domain.Enums;
using FastTechFoods.Orders.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace FastTechFoods.Orders.Controllers
{
    [Route("api/v1.0/Pedidos")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        private static readonly Counter CriarCounter = Metrics.CreateCounter("order_criar_requests_total", "Total de requisições para Criar");
        private static readonly Counter CancelarCounter = Metrics.CreateCounter("order_cancelar_requests_total", "Total de requisições para Cancelar");
        private static readonly Counter RejeitarCounter = Metrics.CreateCounter("order_rejeitar_requests_total", "Total de requisições para Rejeitar");
        private static readonly Counter AceitarCounter = Metrics.CreateCounter("order_aceitar_requests_total", "Total de requisições para Aceitar");
        private static readonly Counter IniciarCounter = Metrics.CreateCounter("order_iniciar_requests_total", "Total de requisições para Iniciar");
        private static readonly Counter FinalizarCounter = Metrics.CreateCounter("order_finalizar_requests_total", "Total de requisições para Finalizar");
        private static readonly Counter ObterTodosCounter = Metrics.CreateCounter("order_obter_todos_requests_total", "Total de requisições para ObterTodos");
        private static readonly Counter ObterTodosComParametrosCounter = Metrics.CreateCounter("order_obter_todos_com_parametros_requests_total", "Total de requisições para ObterTodosComParametros");

        public OrderController(
                    ILogger<OrderController> logger, 
                    IOrderService orderService,
                    IOrderRepository orderRepository,
                    IMapper mapper)
        {
            _logger = logger;
            _orderService = orderService;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Api responsável por enviar pedidos a fila de criação de pedidos. - teste
        /// </summary>
        /// <remarks>
        /// Use a autenticação via token JWT para acessar os endpoints protegidos.
        /// </remarks>
        /// 
        [Authorize]
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Criar([FromBody] OrderDto payload)
        {
            CriarCounter.Inc();

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

        [Authorize]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Cancelar([FromBody] ChangeStatusDto payload)
        {
            CancelarCounter.Inc();

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

        [Authorize(Roles = "Admin, Employee")]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Rejeitar([FromBody] ChangeStatusDto payload)
        {
            RejeitarCounter.Inc();

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

        [Authorize(Roles = "Admin, Employee")]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Aceitar([FromBody] ChangeStatusDto payload)
        {
            AceitarCounter.Inc();

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

        [Authorize(Roles = "Admin, KitchenStaff")]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Iniciar([FromBody] ChangeStatusDto payload)
        {
            IniciarCounter.Inc();

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

        [Authorize(Roles = "Admin, KitchenStaff")]        
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Finalizar([FromBody] ChangeStatusDto payload)
        {
            FinalizarCounter.Inc();

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

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<ResponseOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterTodos()
        {
            ObterTodosCounter.Inc();

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

        [Authorize]
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<ResponseOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterTodosComParametros(
            [FromQuery] Guid? idStore,
            [FromQuery] Guid? idUser,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            ObterTodosComParametrosCounter.Inc();

            try
            {
                _logger.LogInformation($"Acessou {nameof(ObterTodosComParametros)}.");

                if (startDate.HasValue && endDate.HasValue && startDate > endDate)
                    return BadRequest("A data de início não pode ser maior que a data de fim.");

                var orders = await _orderRepository.GetAll();

                var filtered = orders
                    .Where(o => !idStore.HasValue || o.IdStore == idStore.Value)
                    .Where(o => !idUser.HasValue || o.IdUser == idUser.Value)
                    .Where(o => !startDate.HasValue || o.CreatedAt >= startDate.Value)
                    .Where(o => !endDate.HasValue || o.CreatedAt <= endDate.Value);

                var response = _mapper.Map<IEnumerable<ResponseOrderDto>>(filtered);
                                
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