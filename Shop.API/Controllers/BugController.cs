using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services.CategoryServices;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class BugController : ControllerBase
    {
        /// <Postman>
        /// https://localhost:7193/api/Bug/not-found
        /// https://localhost:7193/api/Bug/server-error
        /// </summary>
        public readonly ICategoryService categoryService;

        public BugController(ICategoryService _categoryService) {
            categoryService = _categoryService;
        }

        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            var category = await categoryService.GetByIdAsync(1000);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpGet("server-error")]
        public async Task<ActionResult> GetServerError()
        {
            var category = await categoryService.GetByIdAsync(1000);
            category.Name = "";// 
            return Ok(category);
        }

        [HttpGet("bad-request/{Id}")]
        public async Task<ActionResult> GetBadRequest(int id)
        {
            return Ok();
        }

        [HttpGet("bad-request/")]
        public async Task<ActionResult> GetBadRequest()
        {
            return BadRequest();
        }
    }
}






