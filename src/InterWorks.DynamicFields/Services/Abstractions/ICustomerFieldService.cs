using InterWorks.DynamicFields.Constants;
using InterWorks.DynamicFields.Models;

namespace InterWorks.DynamicFields.Services.Abstractions;

public interface ICustomerFieldService
{
    Task CreateFieldAsync(CustomerField customerField);
    Task UpdateAsync(Guid id, string newValue);
    Task<IEnumerable<CustomerField>> GetByCustomerIdAsync(Guid customerId);
}