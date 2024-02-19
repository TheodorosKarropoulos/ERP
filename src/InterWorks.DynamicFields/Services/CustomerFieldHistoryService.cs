using InterWorks.DynamicFields.Models;
using InterWorks.DynamicFields.Repositories.Abstractions;
using InterWorks.DynamicFields.Services.Abstractions;

namespace InterWorks.DynamicFields.Services;

public sealed class CustomerFieldHistoryService : ICustomerFieldHistoryService
{
    private readonly ICustomerFieldHistoryRepository _repository;

    public CustomerFieldHistoryService(ICustomerFieldHistoryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task InsertAsync(FieldValueHistory fieldValueHistory)
    {
        try
        {
            await _repository.InsertAsync(fieldValueHistory);
        }
        catch (Exception ex)
        {
            // Logging
            throw;
        }
    }

    public async Task<IEnumerable<FieldValueHistory>> GetByFieldIdAsync(Guid fieldId)
    {
        try
        {
            return await _repository.GetByFieldIdAsync(fieldId);
        }
        catch (Exception ex)
        {
            // Logging
            throw;
        }
    }
}