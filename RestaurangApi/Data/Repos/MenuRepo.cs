using Microsoft.EntityFrameworkCore;
using ResturangDB_API.Data.Repos.IRepos;
using ResturangDB_API.Models;

namespace ResturangDB_API.Data.Repos
{
    public class MenuRepo : IMenuRepo
    {
        private readonly ResturangContext _context;

        public MenuRepo(ResturangContext context)
        {
            _context = context;
        }

        public async Task AddMenuAsync(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            var menuList = await _context.Menus.ToListAsync();
            return menuList;
        }

        public async Task<Menu> GetMenuByIdAsync(int menuID)
        {
            var menu = await _context.Menus.FindAsync(menuID);
            return menu;
        }

        public async Task<Menu> GetMenuWithItemsAsync(int menuID)
        {
            return await _context.Menus
                .Include(m => m.MenuItems)
                .FirstOrDefaultAsync(m => m.MenuID == menuID);
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            _context.Menus.Update(menu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenuAsync(Menu menu)
        {
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
        }
    }
}
