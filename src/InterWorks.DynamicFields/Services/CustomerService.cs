using InterWorks.DynamicFields.Models;
using InterWorks.DynamicFields.Repositories.Abstractions;
using InterWorks.DynamicFields.Services.Abstractions;

namespace InterWorks.DynamicFields.Services;

public sealed class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task CreateAsync(Customer customer)
    {
        await _customerRepository.CreateAsync(customer);
    }

    public async Task UpdateAsync(Customer customer)
    {
        await _customerRepository.UpdateAsync(customer);
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        try
        {
            return await _customerRepository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            // Logging here
            throw;
        }
    }
}