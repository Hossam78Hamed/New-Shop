using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Helper;
namespace Shop.API.Controllers
{
   // [Route("api/[controller]")]
    [Route("Errors/{statusCode}")]
    [ApiController] 
    [ApiExplorerSettings(IgnoreApi =true)]////to hide this controller from swagger 
    public class ErrorsController : ControllerBase
    {
        //statusCode
        [HttpGet]
        public IActionResult Error(int statusCode ) {

            return Ok(new ResponseAPI(statusCode));
        
        }
    }
}
