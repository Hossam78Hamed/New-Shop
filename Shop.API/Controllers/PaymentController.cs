using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Helper;
using Shop.Core.DTO.AllOrderDTO;
using Shop.Core.Services.PaymentServices;
using System.Security.Claims;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService _paymentService)
        {
            paymentService = _paymentService;
        }
        [Authorize]
        [HttpPost("CreateOrUpdatePayment")]
        public async Task<IActionResult> CreateOrUpdatePayment(string basketId, int? deliveryId)
        {

            var basket = await paymentService.CreateOrUpdatePaymentAsync( basketId,  deliveryId);
            if (basket != null)
            { return Ok(basket); }
            else { return BadRequest(basket); }
            
            

        }


    }
}
