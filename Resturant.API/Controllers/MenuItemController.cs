using Microsoft.AspNetCore.Mvc;
using Resturant.BL.Contracts;
using Resturant.BL.Features.MenuItems.Requests;

namespace Resturant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemServices _menuItemService;

        public MenuItemController(IMenuItemServices menuItemService)
        {
            _menuItemService = menuItemService;
        }

        // GET: api/MenuItem
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await _menuItemService.ResetAvailabilityDailyAsync();
            await _menuItemService.UpdateAvailabilityBasedOnOrderCountAsync();

            var result = await _menuItemService.GetAllAsync();
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }

        // GET: api/MenuItem/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _menuItemService.GetByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Data);
        }

        // POST: api/MenuItem
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddMenuItemRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _menuItemService.AddAsync(model);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        // PUT: api/MenuItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMenuItemRequest model)
        {
            if (id != model.Id)
                return BadRequest("ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _menuItemService.UpdateAsync(model);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        }

        // DELETE: api/MenuItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _menuItemService.DeleteAsync(new DeleteMenuItemRequest(id));
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return NoContent();
        }

        // Optional: GET available items
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableItems()
        {
            var result = await _menuItemService.UpdateAvailabilityBasedOnOrderCountAsync();
            return Ok(result);
        }
    }
}
