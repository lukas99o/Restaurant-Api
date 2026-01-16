using Microsoft.AspNetCore.Mvc;
using ResturangDB_API.Models;
using ResturangDB_API.Services.IServices;
using ResturangDB_API.Models.DTOs.Table;
using ResturangDB_API.Data.Repos.IRepos;

namespace ResturangDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpPost]
        [Route("CreateTable")]
        public async Task<IActionResult> CreateTable(TableCreateDTO table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _tableService.AddTableAsync(table);
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableGetDTO>>> GetAllTables()
        {
            var tableList = await _tableService.GetAllTablesAsync();
            return Ok(tableList);
        }

        [HttpGet]
        [Route("GetSpecificTable/{tableID}")]
        public async Task<ActionResult<TableGetDTO>> GetSpecificTable(int tableID)
        {
            var table = await _tableService.GetTableByIdAsync(tableID);

            if (table == null)
            {
                return NotFound();
            }

            return Ok(table);
        }

        [HttpGet]
        [Route("AvailableTables")]
        public async Task<ActionResult<IEnumerable<TableGetDTO>>> AvailableTables(DateTime time, DateTime timeEnd)
        {
            var tableList = await _tableService.GetAvailableTablesAsync(time, timeEnd);
            return Ok(tableList);
        }

        [HttpPut]
        [Route("UpdateTable/{tableID}")]
        public async Task<IActionResult> UpdateSpecificTable(int tableID, TableUpdateDTO table)
        {
            if (tableID != table.TableID)
            {
                return BadRequest();
            }

            var result = await _tableService.UpdateTableAsync(table);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteTable/{tableID}")]
        public async Task<IActionResult> DeleteSpecificTable(int tableID)
        {
            var result = await _tableService.DeleteTableAsync(tableID);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
