using Microsoft.EntityFrameworkCore;
using ResturangDB_API.Data.Repos.IRepos;
using ResturangDB_API.Models;

namespace ResturangDB_API.Data.Repos
{
    public class MenuItemRepo : IMenuItemRepo
    { 
        private readonly ResturangContext _context;

        public MenuItemRepo(ResturangContext context)
        {
            _context = context;
        }

        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            var menuItemList = await _context.MenuItems.ToListAsync();
            return menuItemList;
        }

        public async Task<MenuItem> GetMenuItemByIDAsync(int menuItemID)
        {
            var menuItem = await _context.MenuItems.FindAsync(menuItemID);
            return menuItem;
        }

        public async Task UpdateMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
        }
    }
}
