using InterWorks.Api.Mappers;

namespace InterWorks.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CreateCustomerProfile), typeof(CreateCustomerFieldProfile));

        return services;
    }
}