using Microsoft.AspNetCore.Mvc;
using ResturangDB_API.Models;
using ResturangDB_API.Models.DTOs.Menu;
using ResturangDB_API.Models.DTOs.MenuItem;
using ResturangDB_API.Services.IServices;

namespace ResturangDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost]
        [Route("CreateMenu")]
        public async Task<IActionResult> CreateMenu(MenuCreateDTO menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _menuService.AddMenuAsync(menu);
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuGetDTO>>> GetAllMenus()
        {
            var menuList = await _menuService.GetAllMenusAsync();
            return Ok(menuList);
        }

        [HttpGet]
        [Route("GetSpecificMenu/{menuID}")]
        public async Task<ActionResult<MenuGetDTO>> GetSpecificMenu(int menuID)
        {
            var menu = await _menuService.GetMenuByIdAsync(menuID);

            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }

        [HttpGet]
        [Route("GetMenuItemsFromMenu/{menuID}")]
        public async Task<ActionResult<IEnumerable<MenuItemGetDTO>>> GetMenuItemsFromMenu(int menuID)
        {
            var menuItems = await _menuService.GetMenuItemsAsync(menuID);

            if (menuItems == null || !menuItems.Any())
            {
                return NotFound();
            }

            return Ok(menuItems);
        }

        [HttpPut]
        [Route("UpdateMenu/{menuID}")]
        public async Task<IActionResult> UpdateSpecificMenu(int menuID, MenuUpdateDTO menu)
        {
            if (menuID != menu.MenuID)
            {
                return BadRequest();
            }

            var result = await _menuService.UpdateMenuAsync(menu);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteMenu/{menuID}")]
        public async Task<IActionResult> DeleteSpecificMenu(int menuID)
        {
            var result = await _menuService.DeleteMenuAsync(menuID);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

    }
}
