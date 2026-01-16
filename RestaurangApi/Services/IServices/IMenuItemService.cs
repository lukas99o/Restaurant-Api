using ResturangDB_API.Models.DTOs.MenuItem;

namespace ResturangDB_API.Services.IServices
{
    public interface IMenuItemService
    {
        Task AddMenuItemAsync(MenuItemCreateDTO menuItem);
        Task<IEnumerable<MenuItemGetDTO>> GetAllMenuItemsAsync();
        Task<MenuItemGetDTO> GetMenuItemByIdAsync(int menuItemID);
        Task<bool> UpdateMenuItemAsync(MenuItemUpdateDTO menuItem);
        Task<bool> DeleteMenuItemAsync(int menuItemID);
    }
}
