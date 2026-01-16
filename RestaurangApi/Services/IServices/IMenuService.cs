using ResturangDB_API.Models;
using ResturangDB_API.Models.DTOs.Menu;
using ResturangDB_API.Models.DTOs.MenuItem;

namespace ResturangDB_API.Services.IServices
{
    public interface IMenuService
    {
        Task AddMenuAsync(MenuCreateDTO menu);
        Task<IEnumerable<MenuGetDTO>> GetAllMenusAsync();
        Task<MenuGetDTO> GetMenuByIdAsync(int menuID);
        Task<IEnumerable<MenuItemGetDTO>> GetMenuItemsAsync(int menuID);
        Task<bool> UpdateMenuAsync(MenuUpdateDTO menu);
        Task<bool> DeleteMenuAsync(int menuID);
    }
}
