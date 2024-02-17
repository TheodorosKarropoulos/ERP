using InterWorks.DynamicFields.DbContext;
using InterWorks.DynamicFields.Models;
using InterWorks.DynamicFields.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InterWorks.DynamicFields.Repositories;

public sealed class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Customer customer)
    {
        customer.Id = Guid.NewGuid();
        await _dbContext.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _dbContext.Entry(customer).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
    }
}