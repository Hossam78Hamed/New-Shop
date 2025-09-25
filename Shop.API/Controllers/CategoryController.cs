using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Helper;
using Shop.Core.DTO.CategoryDTO;
using Shop.Core.Services.CategoryServices;
using Shop.API.Helper;
namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            // https://localhost:7193/api/Category/GetAll
            //https://localhost:7193/api/Category/GetById/1
            categoryService = _categoryService;
            // /Category/GetAll
            /// /api/Category/GetById/{Id}
        }

        //GetAllAsync
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<GetOneCategoryDTO> categorys = await categoryService.GetAllAsync();
             
                if (categorys != null)
                {
                    return Ok(categorys);

                }
                else
                {
                    return BadRequest(new ResponseAPI(400));

                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }





        }
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {

            try
            {
                GetOneCategoryDTO? CategoryDTO = await categoryService.GetByIdAsync(Id);
                if (CategoryDTO != null)
                {
                    return Ok(CategoryDTO);
                }
                else { return BadRequest( new ResponseAPI(400)); }
            }

            catch (Exception ex) { return BadRequest(ex.Message); }

        }



        [HttpDelete("HeardDeleteAsync/{Id}")]
        public async Task<IActionResult> HeardDelete(int Id)
        {


            try
            {
                GetOneCategoryDTO CategoryDTO = await categoryService.HeardDeleteAsync(Id);
                if (CategoryDTO != null)
                {
                    return Ok(new ResponseAPI(200, "item has been deleted From DB"));
                }
                else { return BadRequest(new ResponseAPI(400)); }
            }

            catch (Exception ex) { return BadRequest(ex.Message); }


        }

        [HttpDelete("SoftDeleteAsync/{Id}")]
        public async Task<IActionResult> SoftDeleteAsync(int Id)
        {
            try
            {
                GetOneCategoryDTO CategoryDTO = await categoryService.SoftDeleteAsync(Id);
                if (CategoryDTO != null)
                {
                    return Ok(new ResponseAPI(200, "item has been deleted But Found in DB "));
                }
                else { return BadRequest(new ResponseAPI(400)); }
            }

            catch (Exception ex) { return BadRequest(ex.Message); }

            
        }
        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(AddCategoryDTO addCategoryDTO)
        {


            try
            {
                var CategoryDTO = await categoryService.AddAsync(addCategoryDTO);
                if (CategoryDTO != null)
                {
                    return Ok( new ResponseAPI(200, "Item has been added"));
                }
                else { return BadRequest(new ResponseAPI(400)); }
            }

            catch (Exception ex) { return BadRequest(ex.Message); }


        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(UpdateCategoryDTO updateCategoryDTO)
        {


            try
            {
                var CategoryDTO = await categoryService.UpdateAsync(updateCategoryDTO);
                if (CategoryDTO != null)
                {
                    return Ok(new ResponseAPI(200, "Item has been updated"));
                }
                else { return BadRequest(new ResponseAPI(400)); }
            }

            catch (Exception ex) { return BadRequest(ex.Message); }


        }
    }
}
