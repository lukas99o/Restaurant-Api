using ResturangDB_API.Models;

namespace ResturangDB_API.Data.Repos.IRepos
{
    public interface IMenuRepo
    {
        Task AddMenuAsync(Menu menu);
        Task<IEnumerable<Menu>> GetAllMenusAsync();
        Task<Menu> GetMenuByIdAsync(int menuID);
        Task<Menu> GetMenuWithItemsAsync(int menuID);
        Task UpdateMenuAsync(Menu menu);
        Task DeleteMenuAsync(Menu menu);
    }
}
