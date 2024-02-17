using InterWorks.DynamicFields.Models;

namespace InterWorks.DynamicFields.Repositories.Abstractions;

public interface ICustomerFieldHistoryRepository
{
    Task InsertAsync(FieldValueHistory fieldValueHistory);
    Task<IEnumerable<FieldValueHistory>> GetByFieldIdAsync(Guid fieldId);
}