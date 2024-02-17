using InterWorks.DynamicFields.Models;

namespace InterWorks.DynamicFields.Repositories.Abstractions;

public interface ICustomerFieldRepository
{
    Task CreateAsync(CustomerField customerField);
    Task UpdateAsync(CustomerField customerField);
    Task<CustomerField?> GetByIdAsync(Guid id);
    Task<IEnumerable<CustomerField>> GetByCustomerIdAsync(Guid customerId);
}