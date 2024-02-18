using InterWorks.DynamicFields.DbContext;
using Microsoft.EntityFrameworkCore;

namespace InterWorks.DynamicFields.IntegrationTests.Fixtures;

public class DatabaseFixture : IDisposable
{
    public ApplicationDbContext DbContext { get; private set; }
    
    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Constants.Database.Name)
            .Options;

        DbContext = new ApplicationDbContext(options);
    }
    
    public void Dispose()
    {
        DbContext.Dispose();
    }
}