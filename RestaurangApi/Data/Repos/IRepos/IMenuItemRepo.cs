using ResturangDB_API.Models;

namespace ResturangDB_API.Data.Repos.IRepos
{
    public interface IMenuItemRepo
    {
        Task AddMenuItemAsync(MenuItem menuItem);
        Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync();
        Task<MenuItem> GetMenuItemByIDAsync(int menuItemID);
        Task UpdateMenuItemAsync(MenuItem menuItem);
        Task DeleteMenuItemAsync(MenuItem menuItem);
    }
}
