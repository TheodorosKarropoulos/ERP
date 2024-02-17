using InterWorks.DynamicFields.DbContext;
using InterWorks.DynamicFields.Repositories;
using InterWorks.DynamicFields.Repositories.Abstractions;
using InterWorks.DynamicFields.Services;
using InterWorks.DynamicFields.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InterWorks.DynamicFields.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDynamicFieldsDependencies(this IServiceCollection services)
    {
        services
            .AddScoped<ICustomerService, CustomerService>()
            .AddScoped<ICustomerFieldService, CustomerFieldService>()
            .AddScoped<ICustomerFieldHistoryService, CustomerFieldHistoryService>()
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<ICustomerFieldRepository, CustomerFieldRepository>()
            .AddScoped<ICustomerFieldHistoryRepository, CustomerFieldHistoryRepository>();

        services.AddMediatR(options =>
        {
            options.Lifetime = ServiceLifetime.Transient;
            options.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        });

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase(Constants.Database.Name);
        });
        
        return services;
    }
}