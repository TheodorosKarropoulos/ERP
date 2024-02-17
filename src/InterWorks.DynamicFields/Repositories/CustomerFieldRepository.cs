using InterWorks.DynamicFields.DbContext;
using InterWorks.DynamicFields.Models;
using InterWorks.DynamicFields.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InterWorks.DynamicFields.Repositories;

public sealed class CustomerFieldRepository : ICustomerFieldRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerFieldRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(CustomerField customerField)
    {
        customerField.Id = Guid.NewGuid();
        await _dbContext.AddAsync(customerField);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(CustomerField customerField)
    {
        _dbContext.Entry(customerField).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<CustomerField?> GetByIdAsync(Guid id)
    {
        return await _dbContext.CustomerFields.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<CustomerField>> GetByCustomerIdAsync(Guid customerId)
    {
        return await _dbContext.CustomerFields.Where(x => x.CustomerId == customerId).ToListAsync();
    }
}