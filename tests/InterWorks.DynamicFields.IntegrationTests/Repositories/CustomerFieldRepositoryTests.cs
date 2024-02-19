using InterWorks.DynamicFields.Constants;
using InterWorks.DynamicFields.IntegrationTests.Fixtures;
using InterWorks.DynamicFields.Models;
using InterWorks.DynamicFields.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterWorks.DynamicFields.IntegrationTests.Repositories;

public class CustomerFieldRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _databaseFixture;
    private readonly CustomerFieldRepository _repository;

    public CustomerFieldRepositoryTests(DatabaseFixture databaseFixture)
    {
        _databaseFixture = databaseFixture;
        _repository = new CustomerFieldRepository(_databaseFixture.DbContext);
    }

    [Fact]
    public async Task CreateAsync_AddsNewCustomerFieldToDatabase()
    {
        // Arrange
        var customerField = new CustomerField
        {
            CustomerId = Guid.NewGuid(),
            FieldName = "Test Field",
            Value = "Test Value",
            Type = FieldType.Text
        };

        // Act
        await _repository.CreateAsync(customerField);

        // Assert
        var addedField = await _databaseFixture.DbContext.CustomerFields.FindAsync(customerField.Id);
        Assert.NotNull(addedField);
        Assert.Equal(customerField.CustomerId, addedField.CustomerId);
        Assert.Equal(customerField.FieldName, addedField.FieldName);
        Assert.Equal(customerField.Value, addedField.Value);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingCustomerFieldInDatabase()
    {
        // Arrange
        var existingField = new CustomerField
        {
            CustomerId = Guid.NewGuid(),
            FieldName = "Existing Name",
            Value = "Existing Value",
            Type = FieldType.DropDownList
        };

        await _databaseFixture.DbContext.CustomerFields.AddAsync(existingField);
        await _databaseFixture.DbContext.SaveChangesAsync();

        var updatedField = new CustomerField
        {
            Id = existingField.Id,
            CustomerId = existingField.CustomerId,
            FieldName = "Updated Field",
            Value = "Updated Value",
            Type = FieldType.Text
        };

        // Detach the existing customer from the DbContext
        _databaseFixture.DbContext.Entry(existingField).State = EntityState.Detached;

        // Act
        await _repository.UpdateAsync(updatedField);

        // Assert
        var findField = await _databaseFixture.DbContext.CustomerFields.FindAsync(existingField.Id);
        Assert.NotNull(findField);
        Assert.Equal(updatedField.FieldName, findField.FieldName);
        Assert.Equal(updatedField.Value, findField.Value);
        Assert.Equal(updatedField.Type, findField.Type);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectCustomerField()
    {
        // Arrange
        var expectedField = new CustomerField
        {
            CustomerId = Guid.NewGuid(),
            FieldName = "Test Field",
            Value = "Test Value",
            Type = FieldType.Text
        };

        await _databaseFixture.DbContext.CustomerFields.AddAsync(expectedField);
        await _databaseFixture.DbContext.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(expectedField.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedField.CustomerId, result.CustomerId);
        Assert.Equal(expectedField.FieldName, result.FieldName);
        Assert.Equal(expectedField.Value, result.Value);
    }

    [Fact]
    public async Task GetByCustomerIdAsync_ReturnsCorrectCustomerFields()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var customerFields = new List<CustomerField>
        {
            new() {CustomerId = customerId, FieldName = "Field1", Value = "Value1", Type = FieldType.Text},
            new() {CustomerId = customerId, FieldName = "Field2", Value = "Value2", Type = FieldType.DropDownList}
        };

        await _databaseFixture.DbContext.CustomerFields.AddRangeAsync(customerFields);
        await _databaseFixture.DbContext.SaveChangesAsync();

        // Act
        var result = await _repository.GetByCustomerIdAsync(customerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customerFields.Count, result.Count());
        Assert.Contains(result, x => x is {FieldName: "Field1", Value: "Value1"});
        Assert.Contains(result, x => x is {FieldName: "Field2", Value: "Value2"});
    }
}