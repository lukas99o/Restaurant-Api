using Moq;
using ResturangDB_API.Data.Repos.IRepos;
using ResturangDB_API.Models;
using ResturangDB_API.Models.DTOs.Menu;
using ResturangDB_API.Services;

namespace RestaurangDB_APITests.Services;

public class MenuServiceTests
{
    private readonly Mock<IMenuRepo> _repoMock;
    private readonly MenuService _service;

    public MenuServiceTests()
    {
        _repoMock = new Mock<IMenuRepo>(MockBehavior.Strict);
        _service = new MenuService(_repoMock.Object);
    }

    [Fact]
    public async Task AddMenuAsync_MapsDto_AndCallsRepo()
    {
        var dto = new MenuCreateDTO { Name = "Lunch" };

        _repoMock
            .Setup(r => r.AddMenuAsync(It.Is<Menu>(m => m.Name == dto.Name)))
            .Returns(Task.CompletedTask);

        await _service.AddMenuAsync(dto);

        _repoMock.VerifyAll();
    }

    [Fact]
    public async Task GetAllMenusAsync_MapsEntities_ToDtos()
    {
        var entities = new List<Menu>
        {
            new() { MenuID = 1, Name = "A" },
            new() { MenuID = 2, Name = "B" }
        };

        _repoMock.Setup(r => r.GetAllMenusAsync()).ReturnsAsync(entities);

        var result = (await _service.GetAllMenusAsync()).ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].MenuID);
        Assert.Equal("A", result[0].Name);
        _repoMock.Verify(r => r.GetAllMenusAsync(), Times.Once);
    }

    [Fact]
    public async Task GetMenuByIdAsync_NotFound_ReturnsNull()
    {
        _repoMock.Setup(r => r.GetMenuByIdAsync(10)).ReturnsAsync((Menu?)null);

        var result = await _service.GetMenuByIdAsync(10);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetMenuByIdAsync_Found_MapsToDto()
    {
        var entity = new Menu { MenuID = 10, Name = "Dinner" };
        _repoMock.Setup(r => r.GetMenuByIdAsync(10)).ReturnsAsync(entity);

        var result = await _service.GetMenuByIdAsync(10);

        Assert.NotNull(result);
        Assert.Equal(entity.MenuID, result!.MenuID);
        Assert.Equal(entity.Name, result.Name);
    }

    [Fact]
    public async Task GetMenuItemsAsync_MenuOrItemsNull_ReturnsEmptyList()
    {
        _repoMock.Setup(r => r.GetMenuWithItemsAsync(1)).ReturnsAsync((Menu?)null);

        var result = (await _service.GetMenuItemsAsync(1)).ToList();

        Assert.Empty(result);
    }

    [Fact]
    public async Task UpdateMenuAsync_EmptyName_ReturnsFalse_AndDoesNotUpdateRepo()
    {
        var dto = new MenuUpdateDTO { MenuID = 1, Name = "" };

        var result = await _service.UpdateMenuAsync(dto);

        Assert.False(result);
        _repoMock.Verify(r => r.GetMenuByIdAsync(It.IsAny<int>()), Times.Never);
        _repoMock.Verify(r => r.UpdateMenuAsync(It.IsAny<Menu>()), Times.Never);
    }

    [Fact]
    public async Task UpdateMenuAsync_NotFound_ReturnsFalse()
    {
        var dto = new MenuUpdateDTO { MenuID = 2, Name = "X" };
        _repoMock.Setup(r => r.GetMenuByIdAsync(2)).ReturnsAsync((Menu?)null);

        var result = await _service.UpdateMenuAsync(dto);

        Assert.False(result);
        _repoMock.Verify(r => r.UpdateMenuAsync(It.IsAny<Menu>()), Times.Never);
    }

    [Fact]
    public async Task UpdateMenuAsync_Found_UpdatesAndReturnsTrue()
    {
        var existing = new Menu { MenuID = 2, Name = "Old" };
        var dto = new MenuUpdateDTO { MenuID = 2, Name = "New" };

        _repoMock.Setup(r => r.GetMenuByIdAsync(2)).ReturnsAsync(existing);
        _repoMock.Setup(r => r.UpdateMenuAsync(It.Is<Menu>(m => ReferenceEquals(m, existing) && m.Name == dto.Name)))
            .Returns(Task.CompletedTask);

        var result = await _service.UpdateMenuAsync(dto);

        Assert.True(result);
        _repoMock.VerifyAll();
    }

    [Fact]
    public async Task DeleteMenuAsync_NotFound_ReturnsFalse()
    {
        _repoMock.Setup(r => r.GetMenuByIdAsync(9)).ReturnsAsync((Menu?)null);

        var result = await _service.DeleteMenuAsync(9);

        Assert.False(result);
        _repoMock.Verify(r => r.DeleteMenuAsync(It.IsAny<Menu>()), Times.Never);
    }

    [Fact]
    public async Task DeleteMenuAsync_Found_DeletesAndReturnsTrue()
    {
        var existing = new Menu { MenuID = 9, Name = "X" };

        _repoMock.Setup(r => r.GetMenuByIdAsync(9)).ReturnsAsync(existing);
        _repoMock.Setup(r => r.DeleteMenuAsync(existing)).Returns(Task.CompletedTask);

        var result = await _service.DeleteMenuAsync(9);

        Assert.True(result);
        _repoMock.VerifyAll();
    }
}
