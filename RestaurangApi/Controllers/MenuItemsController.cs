using Microsoft.AspNetCore.Mvc;
using ResturangDB_API.Models.DTOs.MenuItem;
using ResturangDB_API.Services.IServices;

namespace ResturangDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemsController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpPost]
        [Route("CreateMenuItem")]
        public async Task<IActionResult> CreateMenuItem(MenuItemCreateDTO menuItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _menuItemService.AddMenuItemAsync(menuItem);
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemGetDTO>>> GetAllMenuItems()
        {
            var menuItemList = await _menuItemService.GetAllMenuItemsAsync();
            return Ok(menuItemList);
        }

        [HttpGet]
        [Route("GetSpecificMenuItem/{menuItemID}")]
        public async Task<ActionResult<MenuItemGetDTO>> GetSpecificMenuItem(int menuItemID)
        {
            var menuItem = await _menuItemService.GetMenuItemByIdAsync(menuItemID);

            if (menuItem == null)
            {
                return NotFound();
            }

            return Ok(menuItem);
        }

        [HttpPut]
        [Route("UpdateMenuItem/{menuItemID}")]
        public async Task<IActionResult> UpdateSpecificMenuItem(int menuItemID, MenuItemUpdateDTO menuItem)
        {
            if (menuItemID != menuItem.MenuItemID)
            {
                return BadRequest();
            }

            var result = await _menuItemService.UpdateMenuItemAsync(menuItem);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteMenuItem/{menuItemID}")]
        public async Task<IActionResult> DeleteSpecificMenuItem(int menuItemID)
        {
            var result = await _menuItemService.DeleteMenuItemAsync(menuItemID);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
