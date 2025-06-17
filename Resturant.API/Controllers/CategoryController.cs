using Microsoft.AspNetCore.Mvc;
using Resturant.BL.Contracts;
using Resturant.BL.Features.Categories.Requests;

namespace Resturant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryService;

        public CategoryController(ICategoryServices categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Category
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result.Data);
        }

        // GET: api/Category/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery]GetCategoryByIdRequest request)
        {
            //var request = new GetCategoryByIdRequest(id);
            var result = await _categoryService.GetCategoryByIdAsync(request);
            if (!result.IsSuccess)
                return NotFound(result.Error);
            return Ok(result.Data);
        }
        //GET: api/Category/name
        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName([FromQuery]GetCategoryByNameRequest request)
        {
            var result = await _categoryService.GetCategoryByNameAsync(request);
            if (result.IsSuccess)
                return Ok(result.Data);
            return NotFound(result.Error);
        }
        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> Create(AddCategoryRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _categoryService.AddCategoryAsync(model);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryRequest model)
        {
            if (id != model.Id)
                return BadRequest("ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _categoryService.UpdateCategoryAsync(model);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(new DeleteCategoryRequest(id));
            if (!result.IsSuccess)
                return NotFound(result.Error);
            return NoContent();
        }
    }
}
