using Moq;
using ResturangDB_API.Data.Repos.IRepos;
using ResturangDB_API.Models;
using ResturangDB_API.Models.DTOs.MenuItem;
using ResturangDB_API.Services;

namespace RestaurangDB_APITests.Services;

public class MenuItemServiceTests
{
    private readonly Mock<IMenuItemRepo> _repoMock;
    private readonly MenuItemService _service;

    public MenuItemServiceTests()
    {
        _repoMock = new Mock<IMenuItemRepo>(MockBehavior.Strict);
        _service = new MenuItemService(_repoMock.Object);
    }

    [Fact]
    public async Task AddMenuItemAsync_MapsDto_AndCallsRepo()
    {
        var dto = new MenuItemCreateDTO
        {
            MenuID = 10,
            Name = "Pizza",
            Price = 120,
            IsAvailable = true,
            ImgUrl = "img",
            Description = "desc"
        };

        _repoMock
            .Setup(r => r.AddMenuItemAsync(It.Is<MenuItem>(m =>
                m.FK_MenuID == dto.MenuID &&
                m.Name == dto.Name &&
                m.Price == dto.Price &&
                m.IsAvailable == dto.IsAvailable &&
                m.ImgUrl == dto.ImgUrl &&
                m.Description == dto.Description)))
            .Returns(Task.CompletedTask);

        await _service.AddMenuItemAsync(dto);

        _repoMock.VerifyAll();
    }

    [Fact]
    public async Task GetAllMenuItemsAsync_MapsEntities_ToDtos()
    {
        var entities = new List<MenuItem>
        {
            new() { MenuItemID = 1, FK_MenuID = 2, Name = "Pizza", Price = 100, IsAvailable = true, ImgUrl = "a", Description = "da" },
            new() { MenuItemID = 2, FK_MenuID = 2, Name = "Pasta", Price = 80, IsAvailable = false, ImgUrl = "b", Description = "db" },
        };

        _repoMock.Setup(r => r.GetAllMenuItemsAsync()).ReturnsAsync(entities);

        var result = (await _service.GetAllMenuItemsAsync()).ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].MenuItemID);
        Assert.Equal(2, result[0].MenuID);
        Assert.Equal("Pizza", result[0].Name);
        Assert.Equal(100, result[0].Price);
        Assert.True(result[0].IsAvailable);
        Assert.Equal("a", result[0].ImgUrl);
        Assert.Equal("da", result[0].Description);

        _repoMock.Verify(r => r.GetAllMenuItemsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetMenuItemByIdAsync_NotFound_ReturnsNull()
    {
        _repoMock.Setup(r => r.GetMenuItemByIDAsync(123)).ReturnsAsync((MenuItem?)null);

        var result = await _service.GetMenuItemByIdAsync(123);

        Assert.Null(result);
        _repoMock.Verify(r => r.GetMenuItemByIDAsync(123), Times.Once);
    }

    [Fact]
    public async Task GetMenuItemByIdAsync_Found_MapsToDto()
    {
        var entity = new MenuItem
        {
            MenuItemID = 3,
            FK_MenuID = 4,
            Name = "Sallad",
            Price = 55,
            IsAvailable = true,
            ImgUrl = "img",
            Description = "desc"
        };

        _repoMock.Setup(r => r.GetMenuItemByIDAsync(3)).ReturnsAsync(entity);

        var result = await _service.GetMenuItemByIdAsync(3);

        Assert.NotNull(result);
        Assert.Equal(entity.MenuItemID, result!.MenuItemID);
        Assert.Equal(entity.FK_MenuID, result.MenuID);
        Assert.Equal(entity.Name, result.Name);
        Assert.Equal(entity.Price, result.Price);
        Assert.Equal(entity.IsAvailable, result.IsAvailable);
        Assert.Equal(entity.ImgUrl, result.ImgUrl);
        Assert.Equal(entity.Description, result.Description);
    }

    [Fact]
    public async Task UpdateMenuItemAsync_EmptyName_ReturnsFalse_AndDoesNotCallRepo()
    {
        var dto = new MenuItemUpdateDTO
        {
            MenuItemID = 1,
            MenuID = 2,
            Name = "",
            Price = 1,
            IsAvailable = true
        };

        var result = await _service.UpdateMenuItemAsync(dto);

        Assert.False(result);
        _repoMock.Verify(r => r.GetMenuItemByIDAsync(It.IsAny<int>()), Times.Never);
        _repoMock.Verify(r => r.UpdateMenuItemAsync(It.IsAny<MenuItem>()), Times.Never);
    }

    [Fact]
    public async Task UpdateMenuItemAsync_NotFound_ReturnsFalse()
    {
        var dto = new MenuItemUpdateDTO
        {
            MenuItemID = 999,
            MenuID = 2,
            Name = "Ok",
            Price = 1,
            IsAvailable = true
        };

        _repoMock.Setup(r => r.GetMenuItemByIDAsync(999)).ReturnsAsync((MenuItem?)null);

        var result = await _service.UpdateMenuItemAsync(dto);

        Assert.False(result);
        _repoMock.Verify(r => r.UpdateMenuItemAsync(It.IsAny<MenuItem>()), Times.Never);
    }

    [Fact]
    public async Task UpdateMenuItemAsync_Found_UpdatesEntity_AndCallsRepo()
    {
        var existing = new MenuItem { MenuItemID = 1 };
        var dto = new MenuItemUpdateDTO
        {
            MenuItemID = 1,
            MenuID = 7,
            Name = "Updated",
            Price = 77,
            IsAvailable = false,
            ImgUrl = "u",
            Description = "d"
        };

        _repoMock.Setup(r => r.GetMenuItemByIDAsync(1)).ReturnsAsync(existing);
        _repoMock
            .Setup(r => r.UpdateMenuItemAsync(It.Is<MenuItem>(m =>
                ReferenceEquals(m, existing) &&
                m.FK_MenuID == dto.MenuID &&
                m.Name == dto.Name &&
                m.Price == dto.Price &&
                m.IsAvailable == dto.IsAvailable &&
                m.ImgUrl == dto.ImgUrl &&
                m.Description == dto.Description)))
            .Returns(Task.CompletedTask);

        var result = await _service.UpdateMenuItemAsync(dto);

        Assert.True(result);
        _repoMock.VerifyAll();
    }

    [Fact]
    public async Task DeleteMenuItemAsync_NotFound_ReturnsFalse()
    {
        _repoMock.Setup(r => r.GetMenuItemByIDAsync(5)).ReturnsAsync((MenuItem?)null);

        var result = await _service.DeleteMenuItemAsync(5);

        Assert.False(result);
        _repoMock.Verify(r => r.DeleteMenuItemAsync(It.IsAny<MenuItem>()), Times.Never);
    }

    [Fact]
    public async Task DeleteMenuItemAsync_Found_Deletes_AndReturnsTrue()
    {
        var existing = new MenuItem { MenuItemID = 5 };

        _repoMock.Setup(r => r.GetMenuItemByIDAsync(5)).ReturnsAsync(existing);
        _repoMock.Setup(r => r.DeleteMenuItemAsync(existing)).Returns(Task.CompletedTask);

        var result = await _service.DeleteMenuItemAsync(5);

        Assert.True(result);
        _repoMock.VerifyAll();
    }
}
