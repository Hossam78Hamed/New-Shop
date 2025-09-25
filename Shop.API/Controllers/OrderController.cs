using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Helper;
using Shop.Core.DTO.AllOrderDTO;
using Shop.Core.Services.OrderServices;
using System.Security.Claims;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService _orderService) {
            orderService = _orderService;
        }

        [HttpPost("CreateOrder")]
        public async Task< IActionResult> CreateOrdersAsync(OrderDTO orderDTO) {

            //var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var email = User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.Email)?.Value ;
            
            if (orderDTO == null || email == null) return BadRequest(new ResponseAPI(400, "OrderDTO or Email not found"));
            var order = await orderService.CreateOrdersAsync(orderDTO, email);
            if (order == null) return BadRequest(new ResponseAPI(400, "Problem in creating order"));

            return Ok(order);
        
        }
        //<List<DeliveryMethodDTO>> GetDeliveryMethodAsync();

        [HttpGet("GetAllOrdersForUser")]
        public async Task<IActionResult> GetAllOrdersForUser()
        {

            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if ( email == null) return BadRequest(new ResponseAPI(400, " Email not found"));
           
            var order = await orderService.GetAllOrdersForUserAsync(email);
            if (order == null) return BadRequest(new ResponseAPI(400, "Problem in Get all orders"));

            return Ok(order);

        }
        [HttpGet("GetOrderById/{Id}")]
        public async Task<IActionResult> GetOrderById(int Id)
        {

            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (email == null||Id==null) return BadRequest(new ResponseAPI(400, " Email Or ID not found"));

            var order = await orderService.GetOrderByIdAsync(Id,email);
            if (order == null) return BadRequest(new ResponseAPI(400, "Problem in Get all orders"));

            return Ok(order);

        }

        [HttpGet("GetDeliveryMethodAsync")]
        public async Task<IActionResult> GetDeliveryMethod()
        {

            var deliveries = await orderService.GetDeliveryMethodAsync();
            if (deliveries == null) return BadRequest(new ResponseAPI(400, "Problem in Get all deliveries"));

            return Ok(deliveries);

        }
    }
}
