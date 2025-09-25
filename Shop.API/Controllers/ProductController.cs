using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.DTO.AllProductDTO;
using Shop.Core.DTO.Shared;
using Shop.Core.Services.ProductServices;
using Shop.Core.DTO.Shared;
namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // /https://localhost:7193/api/Product/GetAll_ByFiltering
        private readonly IProductService productService;



        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }
        [HttpGet("Get_AllProduct")]
        public async Task<IActionResult> Get_AllProduct()
        {
            try
            {
                ResultView<List<ProductDTO>>? resultView = await productService.GetAllAsync();
                if (resultView.IsSuccess ) { return Ok(resultView); }
                else { BadRequest("not found products"); }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }
        [HttpGet("Get_ProductByID/{Id}")]
        public async Task<IActionResult> Get_ProductByID(int Id)
        {
            try
            {
                ResultView<ProductDTO>? resultView = await productService.GetByIdAsync(Id);
                if (resultView.IsSuccess)
                {
                    return Ok(resultView);
                }
                else
                {
                    return BadRequest("product not found");
                }

            }

            catch (Exception ex) { return BadRequest(ex.Message); }

        }
        [HttpPost("Add_Product")]
        public async Task<IActionResult> Add_Product(AddProductDTO entity)
        {
            try
            {
               ResultView<AddProductDTO>? resultView = await productService.AddAsync(entity);
                if (resultView.IsSuccess)
                {
                    return Ok(resultView);

                }
                else { return BadRequest("product is null"); }


            }
            catch (Exception ex) { return BadRequest(ex.Message); }


        }
        [HttpPut("update_ProductBy")]
        public async Task<IActionResult> update_Product(UpdateProductDTO entity)
        {
            try
            {
            ResultView<UpdateProductDTO> resultView = await productService.UpdateAsync(entity);
            if (resultView.IsSuccess)
            {
                return Ok(resultView);

            }
            else { return BadRequest(resultView.Msg); }


            }
            catch (Exception ex) { return BadRequest(ex.Message); }


        }
        [  HttpDelete ("HardDelete_ProductById/{Id}")]
        public async Task<IActionResult> HardDelete_ProductById(int Id)
        {
            try
            {
                ResultView<ProductDTO>? resultView = await productService.HeardDeleteAsync(Id);
                if (resultView.IsSuccess)
                {
                    return Ok(resultView);

                }
                else { return BadRequest(resultView.Msg); }


            }
            catch (Exception ex) { return BadRequest(ex.Message); }


        }

        //
        [HttpDelete("SoftDelete_ProductById/{Id}")]
        public async Task<IActionResult> SoftDelete_ProductById(int Id)
        {
            try
            {
                ResultView<ProductDTO>? resultView = await productService.SoftDeleteAsync(Id);
                if (resultView.IsSuccess)
                {
                    return Ok(resultView);

                }
                else { return BadRequest(resultView.Msg); }


            }
            catch (Exception ex) { return BadRequest(ex.Message); }


        }

        //GetAllByFilteringAsync
        [HttpPost("GetAll_ByFiltering")]

        public async Task<IActionResult> GetAllByFiltering([FromBody]ProductFilter productFilter)
        {
            try
            {
               var resultView = await productService.GetAllByFilteringAsync(productFilter);
                if (resultView.IsSuccess)
                {
                    return Ok(resultView);

                }
                else { return BadRequest(resultView.Msg); }


            }
            catch (Exception ex) { return BadRequest(ex.Message); }


        }



    }
}



