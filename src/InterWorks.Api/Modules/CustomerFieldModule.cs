using AutoMapper;
using InterWorks.Api.Requests;
using InterWorks.DynamicFields.Models;
using InterWorks.DynamicFields.Services.Abstractions;

namespace InterWorks.Api.Modules;

public static class CustomerFieldModule
{
    public static void AddCustomerFieldEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/customerFields/customer/{customerId}",
            async (Guid customerId, ICustomerFieldService service) => await service.GetByCustomerIdAsync(customerId));

        app.MapPost("/customerFields",
            async (CreateCustomerField request, ICustomerFieldService service, IMapper mapper) =>
            {
                var field = mapper.Map<CreateCustomerField, CustomerField>(request);
                await service.CreateFieldAsync(field);
            });

        app.MapPut("/customerFields",
            async (UpdateCustomerField request, ICustomerFieldService service) =>
            {
                await service.UpdateAsync(request.Id, request.Value);
            });
    }
}