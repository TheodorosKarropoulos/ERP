using InterWorks.DynamicFields.Models;

namespace InterWorks.DynamicFields.Services.Abstractions;

public interface ICustomerService
{
    Task CreateAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task<Customer?> GetByIdAsync(Guid id);
}