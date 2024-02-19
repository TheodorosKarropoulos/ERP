using InterWorks.DynamicFields.IntegrationTests.Fixtures;
using InterWorks.DynamicFields.Models;
using InterWorks.DynamicFields.Repositories;
using InterWorks.DynamicFields.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace InterWorks.DynamicFields.IntegrationTests.Repositories;

public class CustomerFieldHistoryRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _databaseFixture;
    private readonly CustomerFieldHistoryRepository _repository;

    public CustomerFieldHistoryRepositoryTests(DatabaseFixture databaseFixture)
    {
        _databaseFixture = databaseFixture;
        _repository = new CustomerFieldHistoryRepository(_databaseFixture.DbContext);
    }

    [Fact]
    public async Task InsertAsync_AddsNewFieldValueHistoryToDatabase()
    {
        // Arrange
        var fieldValueHistory = new FieldValueHistory
        {
            CustomerFieldId = Guid.NewGuid(),
            NewValue = "New Value",
            OldValue = "Old Value"
        };
        
        // Act
        await _repository.InsertAsync(fieldValueHistory);
        
        // Assert
        var addedHistory = await _databaseFixture.DbContext.FieldValueHistories.FindAsync(fieldValueHistory.Id);
        Assert.NotNull(addedHistory);
        Assert.Equal(fieldValueHistory.CustomerFieldId, addedHistory.CustomerFieldId);
        Assert.Equal(fieldValueHistory.NewValue, addedHistory.NewValue);
        Assert.Equal(fieldValueHistory.OldValue, addedHistory.OldValue);
        Assert.Equal(fieldValueHistory.DateModified, addedHistory.DateModified);
    }

    [Fact]
    public async Task GetByFieldIdAsync_ReturnsCorrectFieldValueHistories()
    {
        // Arrange
        var fieldId = Guid.NewGuid();
        var fieldValueHistories = new List<FieldValueHistory>
        {
            new() {CustomerFieldId = fieldId, NewValue = "Value1", OldValue = "OldValue"},
            new() {CustomerFieldId = fieldId, NewValue = "Value2", OldValue = "Value1"},
        };

        await _databaseFixture.DbContext.FieldValueHistories.AddRangeAsync(fieldValueHistories);
        await _databaseFixture.DbContext.SaveChangesAsync();
        
        // Act
        var result = await _repository.GetByFieldIdAsync(fieldId);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(fieldValueHistories.Count, result.Count());
        Assert.Contains(result, x => x.OldValue == "Value1");
        Assert.Contains(result, x => x.OldValue == "OldValue");
        Assert.Contains(result, x => x.NewValue == "Value1");
        Assert.Contains(result, x => x.NewValue == "Value2");
    }
}