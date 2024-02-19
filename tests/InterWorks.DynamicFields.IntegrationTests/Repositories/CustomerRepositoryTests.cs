using InterWorks.DynamicFields.IntegrationTests.Fixtures;
using InterWorks.DynamicFields.Models;
using InterWorks.DynamicFields.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterWorks.DynamicFields.IntegrationTests.Repositories;

public class CustomerRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _databaseFixture;
    private readonly CustomerRepository _repository;

    public CustomerRepositoryTests(DatabaseFixture databaseFixture)
    {
        _databaseFixture = databaseFixture;
        _repository = new CustomerRepository(_databaseFixture.DbContext);
    }

    [Fact]
    public async Task CreateAsync_AddsNewCustomerToDatabase()
    {
        // Arrange
        var newCustomer = new Customer
        {
            Name = "New Customer",
        };

        // Act
        await _repository.CreateAsync(newCustomer);

        // Assert
        var addedCustomer = await _databaseFixture.DbContext.Customers.FindAsync(newCustomer.Id);
        Assert.NotNull(addedCustomer);
        Assert.Equal(newCustomer.Name, addedCustomer.Name);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingCustomerInDatabase()
    {
        // Arrange
        var existingCustomer = new Customer
        {
            Name = "Existing Customer"
        };

        await _databaseFixture.DbContext.Customers.AddAsync(existingCustomer);
        await _databaseFixture.DbContext.SaveChangesAsync();

        var updatedCustomer = new Customer
        {
            Id = existingCustomer.Id,
            Name = "Updated Customer"
        };
        
        // Detach the existing customer from the DbContext
        _databaseFixture.DbContext.Entry(existingCustomer).State = EntityState.Detached;

        // Act
        await _repository.UpdateAsync(updatedCustomer);

        // Assert
        var findCustomer = await _databaseFixture.DbContext.Customers.FindAsync(existingCustomer.Id);
        Assert.NotNull(findCustomer);
        Assert.Equal(updatedCustomer.Name, findCustomer.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectCustomer()
    {
        // Arrange
        var customer = new Customer
        {
            Name = "Test Customer"
        };

        await _databaseFixture.DbContext.Customers.AddAsync(customer);
        await _databaseFixture.DbContext.SaveChangesAsync();

        // Act
        await _repository.CreateAsync(customer);
        var createdCustomer = await _repository.GetByIdAsync(customer.Id);

        // Assert
        Assert.NotNull(createdCustomer);
        Assert.Equal(customer.Id, createdCustomer.Id);
        Assert.Equal(customer.Name, createdCustomer.Name);
    }
}