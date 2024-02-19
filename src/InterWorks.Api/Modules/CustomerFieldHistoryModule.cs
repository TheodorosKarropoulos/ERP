using InterWorks.DynamicFields.Services.Abstractions;

namespace InterWorks.Api.Modules;

public static class CustomerFieldHistoryModule
{
    public static void AddCustomerFieldHistoryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/customerFieldHistory/{fieldId}",
            async (Guid fieldId, ICustomerFieldHistoryService service) => await service.GetByFieldIdAsync(fieldId));
    }
}