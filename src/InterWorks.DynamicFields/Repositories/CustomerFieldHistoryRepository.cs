using InterWorks.DynamicFields.DbContext;
using InterWorks.DynamicFields.Models;
using InterWorks.DynamicFields.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InterWorks.DynamicFields.Repositories;

public sealed class CustomerFieldHistoryRepository : ICustomerFieldHistoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerFieldHistoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task InsertAsync(FieldValueHistory fieldValueHistory)
    {
        fieldValueHistory.Id = Guid.NewGuid();
        fieldValueHistory.DateModified = DateTimeOffset.UtcNow;

        await _dbContext.AddAsync(fieldValueHistory);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<FieldValueHistory>> GetByFieldIdAsync(Guid fieldId)
    {
        return await _dbContext.FieldValueHistories.Where(x => x.CustomerFieldId == fieldId).ToListAsync();
    }
}