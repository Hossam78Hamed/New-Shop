using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services.CustomerBasketServices;
using Shop.Domain.Redis_Entities; 
using Shop.API.Helper;
namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBasketController : ControllerBase
    {
        private readonly ICustomerBasketService customerBasketService;
        public CustomerBasketController(ICustomerBasketService customerBasket ) {

            customerBasketService = customerBasket;
        }

        [HttpPost("AddOrUpdate_BasketAsync")]
        public async Task <IActionResult> AddOrUpdate_BasketAsync(CustomerBasket customerBasket) {

            var Item= await customerBasketService.AddOrUpdateBasketAsync(customerBasket);
            if(Item!=null)
                
            return Ok(Item);
            return BadRequest(new ResponseAPI(400,"can not add or update"));
        }
        [HttpDelete("Delete_Item_BasketAsync/{Id}")]
        public async Task<IActionResult> Delete_BasketAsync(string Id)
        {

            var boolen =await customerBasketService.DeleteBasketAsync(Id);
            if (boolen == true)

            { return Ok(new ResponseAPI(200, "Done")); }
            return BadRequest(new ResponseAPI(400, "can not Delete"));
        }

        [HttpGet("Get_Item_BasketByIDAsync/{Id}")]
        public async Task<IActionResult> Get_BasketByIDAsync(string Id)
        {

            var Item = await customerBasketService.GetBasketAsync(Id);
            if (Item != null)

            { return Ok(Item);
            
            
            
            }
            return Ok (new CustomerBasket());
        }





        ////-------------
    }
}
