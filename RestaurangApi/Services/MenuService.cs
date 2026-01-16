using ResturangDB_API.Data.Repos.IRepos;
using ResturangDB_API.Models;
using ResturangDB_API.Services.IServices;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Net.WebSockets;
using ResturangDB_API.Models.DTOs.Menu;
using ResturangDB_API.Models.DTOs.MenuItem;

namespace ResturangDB_API.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepo _menuRepo;

        public MenuService(IMenuRepo menuRepo)
        {
            _menuRepo = menuRepo;
        }

        public async Task AddMenuAsync(MenuCreateDTO menuCreate)
        {
            var menu = new Menu
            {
                Name = menuCreate.Name
            };

            await _menuRepo.AddMenuAsync(menu);
        }

        public async Task<IEnumerable<MenuGetDTO>> GetAllMenusAsync()
        {
            var menus = await _menuRepo.GetAllMenusAsync();

            var menuList = menus.Select(menu => new MenuGetDTO
            {
                MenuID = menu.MenuID,
                Name = menu.Name
            }).ToList();

            return menuList;
        }

        public async Task<MenuGetDTO> GetMenuByIdAsync(int menuID)
        {
            var menuFound = await _menuRepo.GetMenuByIdAsync(menuID);

            if (menuFound != null)
            {
                var menu = new MenuGetDTO
                {
                    MenuID = menuFound.MenuID,
                    Name = menuFound.Name
                };

                return menu;
            }

            return null;
        }

        public async Task<IEnumerable<MenuItemGetDTO>> GetMenuItemsAsync(int menuID)
        {
            var menuFound = await _menuRepo.GetMenuWithItemsAsync(menuID);
            
            if (menuFound == null || menuFound.MenuItems == null)
            {
                return new List<MenuItemGetDTO>();
            }

            var menuItemDtos = menuFound.MenuItems.Select(menuItem => new MenuItemGetDTO
            {
                MenuItemID = menuItem.MenuItemID,
                Name = menuItem.Name,
                Price = menuItem.Price,
                IsAvailable = menuItem.IsAvailable,
                MenuID = menuItem.FK_MenuID,
                ImgUrl = menuItem.ImgUrl
            }).ToList();

            return menuItemDtos;
        }

        public async Task<bool> UpdateMenuAsync(MenuUpdateDTO menu)
        {
            if (string.IsNullOrEmpty(menu.Name))
            {
                return false;
            }

            var updatedMenu = await _menuRepo.GetMenuByIdAsync(menu.MenuID);

            if (updatedMenu != null)
            {
                updatedMenu.Name = menu.Name;
                await _menuRepo.UpdateMenuAsync(updatedMenu);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteMenuAsync(int menuID)
        {
            var foundMenu = await _menuRepo.GetMenuByIdAsync(menuID);

            if (foundMenu != null)
            {
                await _menuRepo.DeleteMenuAsync(foundMenu);
                return true;
            }

            return false;
        }
    }
}
