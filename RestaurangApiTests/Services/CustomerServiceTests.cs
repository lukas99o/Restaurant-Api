using Moq;
using ResturangDB_API.Data.Repos.IRepos;
using ResturangDB_API.Models;
using ResturangDB_API.Models.DTOs.Customer;
using ResturangDB_API.Services;

namespace RestaurangDB_APITests.Services;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepo> _repoMock;
    private readonly CustomerService _service;

    public CustomerServiceTests()
    {
        _repoMock = new Mock<ICustomerRepo>(MockBehavior.Strict);
        _service = new CustomerService(_repoMock.Object);
    }

    [Fact]
    public async Task AddCustomerAsync_MapsDto_AndCallsRepo()
    {
        var dto = new CustomerCreateDTO { Name = "A", Email = "a@a.se", PhoneNumber = "123" };

        _repoMock
            .Setup(r => r.AddCustomerAsync(It.Is<Customer>(c =>
                c.Name == dto.Name &&
                c.Email == dto.Email &&
                c.PhoneNumber == dto.PhoneNumber)))
            .Returns(Task.CompletedTask);

        await _service.AddCustomerAsync(dto);

        _repoMock.VerifyAll();
    }

    [Fact]
    public async Task GetAllCustomersAsync_MapsEntities_ToDtos()
    {
        var entities = new List<Customer>
        {
            new() { CustomerID = 1, Name = "A", Email = "a@a.se", PhoneNumber = "1", Password = "p" },
            new() { CustomerID = 2, Name = "B", Email = "b@b.se", PhoneNumber = "2", Password = "q" },
        };

        _repoMock.Setup(r => r.GetAllCustomersAsync()).ReturnsAsync(entities);

        var result = (await _service.GetAllCustomersAsync()).ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal("p", result[0].Password);
        _repoMock.Verify(r => r.GetAllCustomersAsync(), Times.Once);
    }

    [Fact]
    public async Task GetCustomerByIdAsync_NotFound_ReturnsNull()
    {
        _repoMock.Setup(r => r.GetCustomerByIDAsync(1)).ReturnsAsync((Customer?)null);

        var result = await _service.GetCustomerByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateCustomerAsync_EmptyName_ReturnsFalse_AndDoesNotCallRepo()
    {
        var dto = new CustomerUpdateDTO { CustomerID = 1, Name = "" };

        var result = await _service.UpdateCustomerAsync(dto);

        Assert.False(result);
        _repoMock.Verify(r => r.GetCustomerByIDAsync(It.IsAny<int>()), Times.Never);
        _repoMock.Verify(r => r.UpdateCustomerAsync(It.IsAny<Customer>()), Times.Never);
    }

    [Fact]
    public async Task UpdateCustomerAsync_NotFound_ReturnsFalse()
    {
        var dto = new CustomerUpdateDTO { CustomerID = 1, Name = "A", Email = "a@a.se", PhoneNumber = "1" };
        _repoMock.Setup(r => r.GetCustomerByIDAsync(1)).ReturnsAsync((Customer?)null);

        var result = await _service.UpdateCustomerAsync(dto);

        Assert.False(result);
        _repoMock.Verify(r => r.UpdateCustomerAsync(It.IsAny<Customer>()), Times.Never);
    }

    [Fact]
    public async Task UpdateCustomerAsync_Found_UpdatesAndReturnsTrue()
    {
        var existing = new Customer { CustomerID = 1, Name = "Old" };
        var dto = new CustomerUpdateDTO { CustomerID = 1, Name = "New", Email = "e", PhoneNumber = "p" };

        _repoMock.Setup(r => r.GetCustomerByIDAsync(1)).ReturnsAsync(existing);
        _repoMock.Setup(r => r.UpdateCustomerAsync(It.Is<Customer>(c =>
            ReferenceEquals(c, existing) &&
            c.Name == dto.Name &&
            c.Email == dto.Email &&
            c.PhoneNumber == dto.PhoneNumber))).Returns(Task.CompletedTask);

        var result = await _service.UpdateCustomerAsync(dto);

        Assert.True(result);
        _repoMock.VerifyAll();
    }

    [Fact]
    public async Task DeleteCustomerAsync_NotFound_ReturnsFalse()
    {
        _repoMock.Setup(r => r.GetCustomerByIDAsync(1)).ReturnsAsync((Customer?)null);

        var result = await _service.DeleteCustomerAsync(1);

        Assert.False(result);
        _repoMock.Verify(r => r.DeleteCustomerAsync(It.IsAny<Customer>()), Times.Never);
    }

    [Fact]
    public async Task DeleteCustomerAsync_Found_DeletesAndReturnsTrue()
    {
        var existing = new Customer { CustomerID = 1 };

        _repoMock.Setup(r => r.GetCustomerByIDAsync(1)).ReturnsAsync(existing);
        _repoMock.Setup(r => r.DeleteCustomerAsync(existing)).Returns(Task.CompletedTask);

        var result = await _service.DeleteCustomerAsync(1);

        Assert.True(result);
        _repoMock.VerifyAll();
    }
}
