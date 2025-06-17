using Microsoft.AspNetCore.Mvc;
using Resturant.BL.Contracts;
using Resturant.BL.Features.Tables.Requests;

namespace Resturant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        // GET: api/Table
        [HttpGet]
        public async Task<IActionResult> GetAvailableTables()
        {
            var result = await _tableService.GetAvailableTablesAsync();
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }

        // GET: api/Table/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetTableByIdRequest(id);
            var result = await _tableService.GetByIdAsync(request);
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Data);
        }

        // POST: api/Table
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddTableRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tableService.AddAsync(model);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        // PUT: api/Table/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTableRequest model)
        {
            if (id != model.Id)
                return BadRequest("ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tableService.UpdateAsync(model);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        }

        // DELETE: api/Table/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tableService.DeleteAsync(new DeleteTableRequest(id));
            if (!result.IsSuccess)
                return NotFound(result.Error);

            return NoContent();
        }
    }
}
