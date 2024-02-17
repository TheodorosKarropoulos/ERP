using InterWorks.DynamicFields.Models;

namespace InterWorks.DynamicFields.Repositories.Abstractions;

public interface ICustomerRepository
{
    Task CreateAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task<Customer?> GetByIdAsync(Guid id);
}